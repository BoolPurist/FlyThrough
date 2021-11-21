using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Object to produce pseudo random numbers
  /// </summary>
  /// <remarks>
  /// It not suitable to produce random numbers for security sakes
  /// </remarks>
  public interface IRandomGenerator
  {
    /// <summary>
    /// Returns a random value between 0 and 1.
    /// </summary>
    float Value { get; }

    /// <param name="minInclusive">
    /// Minimal value which can be returned
    /// </param>
    /// <param name="maxInclusive">
    /// Maximum value which can be returned
    /// </param>
    /// <returns>
    /// Returns a whole number between minInclusive and maxInclusive
    /// </returns>
    int Range(int minInclusive, int maxInclusive);
  }
}