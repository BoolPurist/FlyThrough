using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Returns Time.deltaTime. Used during runtime of the game.
  /// </summary>
  public class UnityDeltaTimeProvider : IDeltaTimeProvider
  {
    public float GetDelatTime() => Time.deltaTime;
  }
}