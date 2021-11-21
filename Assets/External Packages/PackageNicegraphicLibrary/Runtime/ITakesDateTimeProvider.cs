using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary
{
  public interface ITakesDateTimeProvider
  {
    /// <summary>
    /// Changes the way how the datetime is determined
    /// </summary>
    void SetDateTimeProvider(IDateTimeProvider provider);
  }
}