using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Component.Movement
{
  public abstract class RigidInterpolatedMotion : RigidGeometryMotion
  {
    private const float MIN_VALUE_FOR_DURATIONS = 1E-5f;

    [SerializeField]
    protected float _Duration = 1f;
    [SerializeField]
    protected float _CounterDuration = 0.5f;
    [SerializeField]
    protected float _SlowingDuration = 1f;
    [SerializeField]
    protected InterpolationKind _AccelerationKind = InterpolationKind.Linear;

    public InterpolationKind AccelerationKind
    {
      get => _AccelerationKind;
      set
      {
        _AccelerationKind = value;

        switch (value)
        {
          case InterpolationKind.Linear:
            _currentInterpolation = LinearInterpolation;
            _currentInverseInterpolation = InverseLinearInterpolation;
            break;
          case InterpolationKind.SmoothStep:
            _currentInterpolation = SmoothStepInterpolation;
            _currentInverseInterpolation = InverseSmoothStepInterpolation;
            break;
          case InterpolationKind.SmootherStep:
            _currentInterpolation = SmootherStepInterpolation;
            _currentInverseInterpolation = InverseSmootherStepInterpolation;
            break;
          default:
            Debug.LogError(ErrorMessages.NotAccountedValueInSwitch(value), this.gameObject);
            break;
        }
      }
    }

    protected virtual void OnValidate()
    {
      AccelerationKind = _AccelerationKind;
      Duration = _Duration;
      SlowingDuration = _SlowingDuration;
      CounterDuration = _CounterDuration;
    }

    public float Duration
    {
      get => _Duration;
      set => _Duration = MinDuration(value);     
    }

    public float SlowingDuration
    {
      get => _SlowingDuration;
      set => _SlowingDuration = MinDuration(value);
    }

    public float CounterDuration
    {
      get => _CounterDuration;
      set => _CounterDuration = MinDuration(value);
    }

    private float MinDuration(in float floatValue) 
      => Mathf.Max(MIN_VALUE_FOR_DURATIONS, Mathf.Abs(floatValue));

    protected float _currentDurationX = 0f;
    protected float _currentSlowingDurationX = 0f;
    protected float _currentCounterDurationX = 0f;
    protected float _currentSpeedX = 0f;
    protected float _currentDurationY = 0f;
    protected float _currentSlowingDurationY = 0f;
    protected float _currentCounterDurationY = 0f;
    protected float _currentSpeedY = 0f;
    protected float _currentDurationZ = 0f;
    protected float _currentSlowingDurationZ = 0f;
    protected float _currentCounterDurationZ = 0f;
    protected float _currentSpeedZ = 0f;

    protected MovingState _currentMovingStateX = MovingState.Standing;
    protected MovingState _currentMovingStateY = MovingState.Standing;
    protected MovingState _currentMovingStateZ = MovingState.Standing;

    private Func<float, float, float> _currentInterpolation = LinearInterpolation;
    private Func<float, float, float> _currentInverseInterpolation = InverseLinearInterpolation;

    public MovingState CurrentMovingStateX => _currentMovingStateX;
    public MovingState CurrentMovingStateY => _currentMovingStateY;
    public MovingState CurrentMovingStateZ => _currentMovingStateZ;

    public override void ForceStand()
    {
      base.ForceStand();

      _currentDurationX = 0f;
      _currentSlowingDurationX = 0f;
      _currentCounterDurationX = 0f;
      _currentSpeedX = 0f;
      _currentDurationY = 0f;
      _currentSlowingDurationY = 0f;
      _currentCounterDurationY = 0f;
      _currentSpeedY = 0f;
      _currentDurationZ = 0f;
      _currentSlowingDurationZ = 0f;
      _currentCounterDurationZ = 0f;
      _currentSpeedZ = 0f;

      _currentMovingStateX = MovingState.Standing;
      _currentMovingStateY = MovingState.Standing;
      _currentMovingStateZ = MovingState.Standing;
    }

    protected void CalculateMotionCycleX(
      )
    {
      CalculateMotionCycle(
        ref _currentMovingStateX,
        base._movement.x,
        ref _currentDurationX,
        ref _currentSlowingDurationX,
        ref _currentCounterDurationX,
        ref _currentSpeedX
        );
    }

    protected void CalculateMotionCycleY(
      )
    {
      CalculateMotionCycle(
        ref _currentMovingStateY,
        base._movement.y,
        ref _currentDurationY,
        ref _currentSlowingDurationY,
        ref _currentCounterDurationY,
        ref _currentSpeedY
        );
    }

    protected void CalculateMotionCycleZ(
      )
    {
      CalculateMotionCycle(
        ref _currentMovingStateZ,
        base._movement.z,
        ref _currentDurationZ,
        ref _currentSlowingDurationZ,
        ref _currentCounterDurationZ,
        ref _currentSpeedZ
        );
    }

    protected void CalculateMotionCycle(
        ref MovingState movingState,
        in float axisDirection,
        ref float duration,
        ref float slowingDuration,
        ref float counteDuration,
        ref float speed
      )
    {
      bool stateHasChanged;

      do
      {

        stateHasChanged = false;

        if (movingState == MovingState.Standing)
        {
          // Possible change to other state
          if (IsControlledToRight(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpFront);
          }
          else if (IsControlledToLeft(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpBack);
          }
          else
          {
            duration = 0f;
            slowingDuration = 0f;
            counteDuration = 0f;
            speed = 0f;
          }
        }
        else if (movingState == MovingState.SpeedingUpFront)
        {
          if (IsNotControlled(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SlowingDownFront);
            ConvertDurationInverse(ref slowingDuration, duration, _Duration, _SlowingDuration);
          }
          else if (IsControlledToLeft(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.CounterSpeedingUpBack);
            ConvertDurationInverse(ref counteDuration, duration, _Duration, _CounterDuration);
          }
          else
          {
            counteDuration = 0f;
            slowingDuration = 0f;
            duration = Mathf.Min(duration + _timeDelta.GetDelatTime(), _Duration);
            speed = _currentInterpolation(Speed, duration / _Duration);
          }
        }
        else if (movingState == MovingState.SpeedingUpBack)
        {
          if (IsNotControlled(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SlowingDownBack);
            ConvertDurationInverse(ref slowingDuration, duration, _Duration, _SlowingDuration);
          }
          else if (IsControlledToRight(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.CounterSpeedingUpFront);
            ConvertDurationInverse(ref counteDuration, duration, _Duration, _CounterDuration);
          }
          else
          {
            counteDuration = 0f;
            slowingDuration = 0f;
            duration = Mathf.Min(duration + _timeDelta.GetDelatTime(), _Duration);
            speed = -_currentInterpolation(Speed, duration / _Duration);
          }
        }
        else if (movingState == MovingState.SlowingDownFront)
        {
          if (IsNotMoving(axisDirection, speed))
          {

            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.Standing);
          }
          else if (IsControlledToRight(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpFront);
            ConvertDurationInverse(ref duration, slowingDuration, _SlowingDuration, _Duration);
          }
          else if (IsControlledToLeft(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.CounterSpeedingUpBack);
            ConvertDuration(ref counteDuration, slowingDuration, _SlowingDuration, _CounterDuration);
          }
          else
          {
            duration = 0f;
            counteDuration = 0f;
            slowingDuration = Mathf.Min(slowingDuration + _timeDelta.GetDelatTime(), _SlowingDuration);
            speed = _currentInverseInterpolation(Speed, slowingDuration / _SlowingDuration);
          }
        }
        else if (movingState == MovingState.SlowingDownBack)
        {
          if (IsNotMoving(axisDirection, speed))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.Standing);
          }
          else if (IsControlledToLeft(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpFront);
            ConvertDurationInverse(ref duration, slowingDuration, _SlowingDuration, _Duration);
          }
          else if (IsControlledToRight(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.CounterSpeedingUpFront);
            ConvertDuration(ref counteDuration, slowingDuration, _SlowingDuration, _CounterDuration);
          }
          else
          {
            duration = 0f;
            counteDuration = 0f;
            slowingDuration = Mathf.Min(slowingDuration + _timeDelta.GetDelatTime(), _SlowingDuration);
            speed = -_currentInverseInterpolation(Speed, slowingDuration / _SlowingDuration);
          }
        }
        else if (movingState == MovingState.CounterSpeedingUpFront)
        {
          if (HasNoSpeed(speed))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.Standing);
          }
          else if (IsControlledToLeft(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpBack);
            ConvertDurationInverse(ref duration, counteDuration, _CounterDuration, _Duration);
          }
          else if (IsNotControlled(axisDirection) && !HasNoSpeed(speed))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SlowingDownBack);
          }
          else
          {
            duration = 0f;
            slowingDuration = 0f;
            counteDuration = Mathf.Min(counteDuration + _timeDelta.GetDelatTime(), _CounterDuration);
            speed = -_currentInverseInterpolation(Speed, counteDuration / _CounterDuration);
          }
        }
        else if (movingState == MovingState.CounterSpeedingUpBack)
        {
          // MovingState.CounterSpeedingUpBack
          if (HasNoSpeed(speed))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.Standing);
          }
          else if (IsControlledToRight(axisDirection))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SpeedingUpFront);
            ConvertDurationInverse(ref duration, counteDuration, _CounterDuration, _Duration);
          }
          else if (IsNotControlled(axisDirection) && !HasNoSpeed(speed))
          {
            ChangeToAnotherState(ref stateHasChanged, ref movingState, MovingState.SlowingDownFront);
          }
          else
          {
            duration = 0f;
            slowingDuration = 0f;
            counteDuration = Mathf.Min(counteDuration + _timeDelta.GetDelatTime(), _CounterDuration);
            speed = _currentInverseInterpolation(Speed, counteDuration / _CounterDuration);
          }
        }

      } while (stateHasChanged);

      bool IsControlledToRight(in float checkedAxis) => checkedAxis > 0f;

      bool IsControlledToLeft(in float checkedAxis) => checkedAxis < 0f;

      bool IsNotControlled(in float chechedAxis) => chechedAxis == 0f;

      bool HasNoSpeed(in float checkedSpeed) => checkedSpeed == 0f;

      bool IsNotMoving(in float checkedAxis, in float checkedSpeed) => IsNotControlled(checkedAxis) && HasNoSpeed(checkedSpeed);

      void ConvertDurationInverse(
        ref float durationToBeConverted,
        in float durationToConvertFrom,
        in float referenceDurationFrom,
        in float referenceDurationTo
        ) => durationToBeConverted = (1f - (durationToConvertFrom / referenceDurationFrom)) * referenceDurationTo;

      void ConvertDuration(
        ref float durationToBeConverted,
        in float durationToConvertFrom,
        in float referenceDurationFrom,
        in float referenceDurationTo
        ) => durationToBeConverted = (durationToConvertFrom / referenceDurationFrom) * referenceDurationTo;

      void ChangeToAnotherState(
        ref bool stateHasChanged,
        ref MovingState oldMovingState,
        in MovingState newMovingState
        )
      {
        stateHasChanged = true;
        oldMovingState = newMovingState;
      }

    }

    private static float LinearInterpolation(float speed, float durationRatio)
      => Mathf.Lerp(0f, speed, durationRatio);

    private static float InverseLinearInterpolation(float speed, float durationRatio)
      => Mathf.Lerp(0f, speed, 1f - durationRatio);

    private static float SmoothStepInterpolation(float speed, float durationRatio)
      => Interpolation.SmootherStep(speed, durationRatio);

    private static float SmootherStepInterpolation(float speed, float durationRatio)
      => Interpolation.SmootherStep(speed, durationRatio);

    private static float InverseSmoothStepInterpolation(float speed, float durationRatio)
      => Interpolation.InverseSmoothStep(speed, durationRatio);

    private static float InverseSmootherStepInterpolation(float speed, float durationRatio)
      => Interpolation.InverseSmootherStep(speed, durationRatio);
  }
}