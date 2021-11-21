using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  public class UnityRandomGenerator : IRandomGenerator
  {
    public float Value => UnityEngine.Random.value;

    public int Range(int minInclusive, int maxInclusive)
      => UnityEngine.Random.Range(minInclusive, maxInclusive);
  }
}