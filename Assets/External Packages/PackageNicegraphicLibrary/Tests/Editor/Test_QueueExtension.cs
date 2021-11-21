using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;


using NiceGraphicLibrary.Extensions;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_QueueExtension
  {
    [TestCaseSource(nameof(TestCase_EnqueueRange))]
    public static void Test_EnqueueRange(int[] elementsToAdd, Queue<int> givenQueue, int[] expectedResult)
    {
      givenQueue.EnqueueRange(elementsToAdd);
      
      int[] actualResult = givenQueue.ToArray();
      Assert.AreEqual(
        expectedResult, 
        actualResult,
        $"Queue was not extended correctly" + 
        $"Expected sequence {expectedResult.GetValuesAsString()}",
        $"Actual sequence {actualResult.GetValuesAsString()}"
        );
    }

    public static object[] TestCase_EnqueueRange
      => new object[]
      {
        new object[]
        {
          new int[] { 2, 3, 10 },
          new Queue<int>(new int[] { 2, 2, 2 }),
          new int[] { 2, 2, 2, 2, 3, 10 }
        },
        new object[]
        {
          new int[] {  },
          new Queue<int>(new int[] { 2, 2, 2 }),
          new int[] { 2, 2, 2 }
        },
        new object[]
        {
          null,
          new Queue<int>(new int[] { 2, 2, 2 }),
          new int[] { 2, 2, 2 }
        }
      };
  }
}