using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  public static class ErrorMessages
  {
    public static string NotAccountedValueInSwitch<T>(T notAccountedForEnumValue)
      => $"For the type {notAccountedForEnumValue.GetType().Name}, the value [{notAccountedForEnumValue}] is not accounted for !";
  } 
}