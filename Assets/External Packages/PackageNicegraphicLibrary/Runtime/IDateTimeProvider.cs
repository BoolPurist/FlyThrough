using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary
{
  public interface IDateTimeProvider
  {
    /// <summary>
    /// Changes the way how the current DateTime is determined.
    /// </summary>
    /// <remarks>
    /// Useful to provide an object to control the DateTime during unit tests
    /// </remarks>
    DateTime GetNowDateTime();
  }
}