using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  public class UtcDateTimeProvider : IDateTimeProvider
  {
    public DateTime GetNowDateTime()
      => DateTime.UtcNow;
  }
}