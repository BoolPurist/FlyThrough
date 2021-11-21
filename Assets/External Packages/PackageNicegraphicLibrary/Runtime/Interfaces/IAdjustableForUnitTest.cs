using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Provides API to set object to switch inner logic to work for unit tests
  /// </summary>
  public interface IAdjustableForUnitTest
  {
    /// <summary>
    /// If true the object behaves differently in certain parts of the logic to work for unit tests
    /// Usually not relying on input or certain physics functionalities of unity to work.
    /// </summary>
    bool IsSetForUnitTest { get; set; } 
  } 
}