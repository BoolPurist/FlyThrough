using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary.Tests.Editor.Stubs
{
  public class FixedDateTimeProvider : IDateTimeProvider
  {
  
    public DateTime FixedTimeStamp { get; set; } = DateTime.UtcNow;

    public void ChangeBySeconds(int seconds) => FixedTimeStamp = FixedTimeStamp.AddSeconds(seconds);

    public void PlusOneSecond() => ChangeBySeconds(1);
    public void MinusOneSecond() => ChangeBySeconds(-1);

    public DateTime GetNowDateTime()
      => FixedTimeStamp;
  }
}