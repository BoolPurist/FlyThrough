using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Provides functions to interpolate values.
  /// </summary>
  public static class Interpolation
  {

    /// <summary>
    /// Coroutine to return factor from 0 to 1 to a given delegate over a certain duration after each frame 
    /// until the duration has passed    
    /// </summary>
    /// <param name="duration">
    /// Time until the coroutine ends. Negative values will be converted to absolute/positive values.
    /// </param>
    /// <param name="interpolateReceiver">
    /// Is called after each frame. Its argument is from 0 to 1 for the amount of passed duration.
    /// Should not be null.
    /// </param>
    public static IEnumerator InterpolateOverTime(float duration, Action<float> interpolateReceiver)
    {
      if (!IsValidReceiver(interpolateReceiver, nameof(interpolateReceiver)))
      {
        yield break;
      }

      duration = Mathf.Abs(duration);
      float passedTime = 0f;
      do {
        passedTime = Mathf.Min(passedTime + Time.deltaTime, duration);
        float passedTimeRatio = passedTime / duration;
        interpolateReceiver(passedTimeRatio);
        yield return null;
      } while (passedTime < duration);

      yield break;
    }

    /// <summary>
    /// Coroutine to return interpolated speed from [startFloat] to [endFloat]
    /// over the given duration. Defaults to 0.
    /// </summary>
    /// <param name="duration">
    /// Time until the interpolation is complete. Negative values will be converted to absolute/positive values.
    /// </param>
    /// <param name="endFloat">
    /// End float value at the end of the interpolation
    /// </param>
    /// <param name="floatReceiver">
    /// Is called after each frame. Its argument is between float [startFloat] to [endFloat] depending on the given duration.
    /// Should not be null.
    /// </param>
    public static IEnumerator InterpolationFloatOverTime(float duration, float endFloat, Action<float> floatReceiver)
    {
      if (!IsValidReceiver(floatReceiver, nameof(floatReceiver)))
      {
        yield break;
      }

      yield return InterpolateOverTime(duration, passedTimeRatio => floatReceiver( endFloat * passedTimeRatio ));
    }

    /// <summary>
    /// Coroutine to return interpolated color between [startColor] to [targetColor]
    /// over the given duration
    /// </summary>
    /// <param name="duration">
    /// Time until the coroutine ends. Negative values will be converted to absolute/positive values.
    /// </param>
    /// <param name="startColor">
    /// Color at the start of the duration.
    /// </param>
    /// <param name="targetColor">
    /// Color at the end of the interpolation
    /// </param>
    /// <param name="colorReceiver">
    /// Is called after each frame. Its argument is between color the [startColor] and the [targetColor] depending on the given duration.
    /// Should not be null.
    /// </param>
    public static IEnumerator InterpolateColorOverTime(float duration, Color startColor, Color targetColor, Action<Color> colorReceiver)
    {
      if (!IsValidReceiver(colorReceiver, nameof(colorReceiver)))
      {
        yield break;
      }

      yield return InterpolateOverTime(duration, passedTimeRatio => colorReceiver(Color.Lerp(startColor, targetColor, passedTimeRatio)));
    }

    /// <summary>
    /// Coroutine to return interpolated 3d Vector from [startColor] to [targetColor]
    /// over the given duration
    /// </summary>
    /// <param name="duration">
    /// Time until the coroutine ends. Negative values will be converted to absolute/positive values.
    /// </param>
    /// <param name="startVector">
    /// 3d vector at the start of the duration.
    /// </param>
    /// <param name="endVector">
    /// 3d vector at the end of the interpolation
    /// </param>
    /// <param name="vectorReceiver">
    /// Is called after each frame. Its argument is between 3d vector the [startVector] and the [endVector] depending on the given duration.
    /// Should not be null.
    /// </param>
    public static IEnumerator InterpolateVector3OverTime(float duration, Vector3 startVector, Vector3 endVector, Action<Vector3> vectorReceiver)
    {
      if (!IsValidReceiver(vectorReceiver, nameof(vectorReceiver)))
      {
        yield break;
      }

      yield return InterpolateOverTime(duration, passedTimeRatio => vectorReceiver(Vector3.Lerp(startVector, endVector, passedTimeRatio)));
    }

    /// <summary>
    /// Coroutine to return interpolated 2d Vector from [startColor] to [targetColor]
    /// over the given duration
    /// </summary>
    /// <param name="duration">
    /// Time until the coroutine ends. Negative values will be converted to absolute/positive values.
    /// </param>
    /// <param name="startVector">
    /// 2d vector at the start of the duration.
    /// </param>
    /// <param name="endVector">
    /// 2d vector at the end of the interpolation
    /// </param>
    /// <param name="vectorReceiver">
    /// Is called after each frame. Its argument is between 2d vector the [startVector] and the [endVector] depending on the given duration.
    /// Should not be null.
    /// </param>
    public static IEnumerator InterpolateVector2OverTime(float duration, Vector2 startVector, Vector2 endVector, Action<Vector2> vectorReceiver)
    {
      if (!IsValidReceiver(vectorReceiver, nameof(vectorReceiver)))
      {
        yield break;
      }

      yield return InterpolateOverTime(duration, passedTimeRatio => vectorReceiver(Vector2.Lerp(startVector, endVector, passedTimeRatio)));
    } 

    // Used to check if receiver as callback function in a coroutine can  be executed, in not null. 
    private static bool IsValidReceiver<TAction>(Action<TAction> coroutineToCheck, string functionName)    
    {
      if (coroutineToCheck == null)
      {
        Debug.LogWarning($"{functionName} was null. Coroutine can not be executed");
        return false;
      }

      return true;
    }

    /// <summary>
    /// Returns plotted value of the smooth step function. Has lower acceleration near 0 and 1 factor.
    /// </summary>
    /// <param name="numberValue">
    /// Value to be plotted
    /// </param>
    /// <param name="factor">
    /// Factor to plot parameter [value] according to the smooth step function
    /// factor will be clamped between 0 and 1. 
    /// </param>
    /// <returns>
    /// Returns a number from 0 to given parameter [value] depending on the parameter [factor]
    /// </returns>
    /// <remarks>
    /// Implementation comes from wiki <seealso href="https://en.wikipedia.org/wiki/Smoothstep"/>
    /// </remarks>
    public static float SmoothStep(float numberValue, float factor)
    {
      factor = Mathf.Clamp(factor, 0f, 1f);
      // 3x^2 - 2x^3
      factor = (factor * factor) * (3 - (2 * factor));
      return factor * numberValue;
    }

    /// <summary>
    /// Smooth step variation suggested by Ken Perlin better approximation but requires more calculation than <see cref="SmoothStep(float, float)"/>
    /// </summary>
    /// <param name="numberValue">
    /// Value to be plotted
    /// </param>
    /// <param name="factor">
    /// Factor to plot parameter [value] according to the smooth step function
    /// factor will be clamped between 0 and 1. 
    /// </param>
    /// <returns>
    /// Returns a number from 0 to the given parameter [value] depending on the parameter [factor]
    /// </returns>
    /// <remarks>
    /// Implementation comes from wiki <seealso href="https://en.wikipedia.org/wiki/Smoothstep"/>
    /// </remarks>
    public static float SmootherStep(float numberValue, float factor)
    {
      factor = Mathf.Clamp(factor, 0f, 1f);
      // 6x^5 - 15x^4 + 10x^3
      factor = factor * factor * factor * ( factor * ( (factor * 6) - 15 ) + 10 );
      return factor * numberValue;
    }

    /// <summary>
    /// Turns the output of smooth step upside down. Can be used for deceleration after acceleration by <see cref="SmoothStep(float, float)"/>
    /// </summary>
    /// <param name="numberValue">
    /// Value to be plotted
    /// </param>
    /// <param name="factor">
    /// Factor to plot parameter [value] according to the smooth step function
    /// factor will be clamped between 0 and 1. 
    /// </param>
    /// <returns>
    /// Returns from parameter [value] to 0 depending on the parameter [factor]
    /// </returns>
    public static float InverseSmoothStep(float numberValue, float factor)
      => (-1f * SmoothStep(numberValue, factor)) + numberValue; // Graph is mirrored on the x-axis and then moved up by the amount of the parameter [value]
    
    /// <summary>
    /// Turns the output of smoother step upside down. Can be used for deceleration after acceleration by <see cref="SmootherStep(float, float)"/>
    /// </summary>
    /// <param name="numberValue">
    /// Value to be plotted
    /// </param>
    /// <param name="factor">
    /// Factor to plot parameter [value] according to the smooth step function
    /// factor will be clamped between 0 and 1. 
    /// </param>
    /// <returns>
    /// Returns from parameter [value] to 0 depending on the parameter [factor]
    /// </returns>
    public static float InverseSmootherStep(float numberValue, float factor)
      => (-1f * SmootherStep(numberValue, factor)) + numberValue; // Graph is mirrored on the x-axis and then moved up by the amount of the parameter [value]

  } 
}