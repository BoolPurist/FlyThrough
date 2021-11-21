using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NiceGraphicLibrary.Tests.Editor.Stubs
{
  /// <summary>
  /// Used for unit testing. Fakes return value for Time.deltaTime to test without waiting for frames
  /// </summary>
  public class FakeDeltaTimeProvider : IDeltaTimeProvider
  {
    private float _deltaTimeFactor = 1f;

    /// <summary>
    /// Fixed value as return value for GetDelatTime
    /// Given negative value for setter will be converted to positive value with same amount.
    /// </summary>
    public float DeltaTimeFactor 
    { 
      get => _deltaTimeFactor;
      set
      {
        _deltaTimeFactor = Mathf.Abs(value);
      }
    }

    public FakeDeltaTimeProvider() : this(1f) { }
    public FakeDeltaTimeProvider(float deltaTimeFactor)
    {
      DeltaTimeFactor = deltaTimeFactor;
    }

    public float GetDelatTime()
    {
      return DeltaTimeFactor;
    }
  }
}
