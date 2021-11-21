using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Extensions;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_ICustomEnumerableExtension
  {
    [TestCaseSource(nameof(TestCases_GetValuesAsString))]
    public void Test_GetValuesAsString(int[] input, int elementsPerLine, string expectedResult)
    {
      string actualResult = input.GetValuesAsString(elementsPerLine);
      Assert.AreEqual(expectedResult, actualResult, $"Input array did not get converted to expected string");
    }

    
    public static object[] TestCases_GetValuesAsString
      => new object[]
      {
        new object[]
        {
          Enumerable.Range(1, 10).ToArray(),
          10,
          "[ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ]"
        },
        new object[]
        {
          Enumerable.Range(1, 5).ToArray(),
          10,
          "[ 1, 2, 3, 4, 5 ]"
        },
        new object[]
        {
          new int[] { },
          10,
          "[ ]"
        },
        new object[]
        {
          Enumerable.Range(1, 10).ToArray(),
          5,
          $"[ 1, 2, 3, 4, 5, {Environment.NewLine}6, 7, 8, 9, 10 ]"
        },
        new object[]
        {
          Enumerable.Range(1, 10).ToArray(),
          -5,
          $"[ 1, 2, 3, 4, 5, {Environment.NewLine}6, 7, 8, 9, 10 ]"
        }
      };
  }
}