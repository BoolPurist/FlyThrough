using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using NiceGraphicLibrary.Extensions;

namespace NiceGraphicLibrary.Tests.Editor.Stubs
{
  public class FakeRandomGenerator : IRandomGenerator
  {
#pragma warning disable IDE0090 // Remove unread private members
    private readonly Queue<float> _randomValues = new Queue<float>();
    private readonly Queue<int> _randomRangeValues = new Queue<int>();
#pragma warning restore IDE0090 // Remove unread private members

    public float FakeValue { get; set; } = 0.5f;
    public int FakeRangeReturnValue { get; set; } = 0;

    public void AddRandomValues(params float[] newRandomValues)
      => _randomValues.EnqueueRange(newRandomValues);

    public void AddRandomRangeValues(params int[] newRandomValues)
      => _randomRangeValues.EnqueueRange(newRandomValues);

    public float Value => ReturnValue(_randomValues, FakeValue);

    public int Range(int minInclusive, int maxInclusive) => ReturnValue(_randomRangeValues, FakeRangeReturnValue);

    private TElement ReturnValue<TElement>(Queue<TElement> queue, TElement defaultElement)
    {
      if (queue.Count == 0)
      {
        return defaultElement;
      }
      else
      {
        return queue.Dequeue();
      }
    }
  }
}