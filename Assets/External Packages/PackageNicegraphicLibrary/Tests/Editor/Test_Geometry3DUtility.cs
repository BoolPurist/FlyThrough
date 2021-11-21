using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_Geometry3DUtility
  {

    [TestCaseSource(nameof(TestCase_IsPointInDirection_ForTrue))]    
    public static void Test_IsPointInDirection_ForTrue(Vector3 direction, Vector3 startPosition)
    {
      Vector3 halfDirectionPositive = GetPointInDirection(0.5f);
      Vector3 directionPositive = GetPointInDirection(1f);
      Vector3 doubleDirectionPositive = GetPointInDirection(2f);
      Vector3 halfDirectionNegative = GetPointInDirection(-0.5f);
      Vector3 directionNegative = GetPointInDirection(-1f);
      Vector3 doubleDirectionNegative = GetPointInDirection(-2f);

      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(halfDirectionPositive, direction, startPosition));
      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(directionPositive, direction, startPosition));
      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(doubleDirectionPositive, direction, startPosition));
      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(halfDirectionNegative, direction, startPosition));
      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(directionNegative, direction, startPosition));
      Assert.IsTrue(Geometry3DUtility.IsPointInDirection(doubleDirectionNegative, direction, startPosition));

      Vector3 GetPointInDirection(float scaleFactor)
        => startPosition + (direction * scaleFactor);
    }

    [TestCaseSource(nameof(TestCase_IsPointInDirection_ForFalse))]
    public static void Test_IsPointInDirection_ForFalse(Vector3 direction, Vector3 pointNotInDirection, Vector3 startPosition)
      => Assert.IsFalse(Geometry3DUtility.IsPointInDirection(pointNotInDirection, direction, startPosition));
    

    // 1. parameter direction vector as Vector3.
    // 2. parameter start position to start with direction line as Vector3.
    public static object[] TestCase_IsPointInDirection_ForTrue
      => new object[]
      {
        new object[] 
        { 
          new Vector3(2f, 4f, 3f),
          Vector3.zero
        },
        new object[]
        {
          new Vector3(-1f, 2f, -8f),
          Vector3.zero
        },
        new object[]
        {
          new Vector3(4.5f, -2.28f, 9.2f),
          Vector3.zero
        },
        new object[]
        {
          new Vector3(1f, 2f, 1f),
          new Vector3(1f, 1f, 1f)
        },
        new object[]
        {
          new Vector3(0f, -1.5f, 3f),
          new Vector3(0f, 2f, 2f)
        },
        new object[]
        {
          new Vector3(2f, -1.5f, 0f),
          new Vector3(2f, 2f, 0f)
        },
        new object[]
        {
          new Vector3(2f, 0f, 3f),
          new Vector3(2f, 0f, 3f)
        },
        new object[]
        {
          new Vector3(0f, 0f, 3f),
          new Vector3(0f, 0f, 3f)
        },
        new object[]
        {
          new Vector3(0f, -1f, 0f),
          new Vector3(0f, -1f, 0f)
        },
        new object[]
        {
          new Vector3(-1f, 0f, 0f),
          new Vector3(-1f, 0f, 0f)
        }
      };

    // 1. parameter: direction vector as Vector3.
    // 2. parameter: point which is not on line of the direction.
    // 3. parameter: start position to start with direction line as Vector3. 
    public static object[] TestCase_IsPointInDirection_ForFalse
      => new object[]
      {
        new object[]
        {
          new Vector3(2f, 4f, 3f),
          new Vector3(3f, 4f, 3f),
          Vector3.zero
        },
        new object[]
        {
          new Vector3(-1f, 2f, -8f),
          new Vector3(1f, 2f, 8f),
          Vector3.zero
        },
        new object[]
        {
          new Vector3(1f, 2f, 8f),
          new Vector3(2f, 4f, 16f),
          new Vector3(3f, 3f, 3f)
        },
        new object[]
        {
          new Vector3(3f, -3f, 4f),
          new Vector3(0f, 0f, 0f),
          new Vector3(1.5f, 1.5f, 1.5f)
        },
        new object[]
        {
          new Vector3(0f, 0f, 0f),
          new Vector3(1f, 1f, 1f),
          new Vector3(0f, 0f, 0f)
        },
        new object[]
        {
          new Vector3(1f, 0f, 0f),
          new Vector3(1f, 1f, 1f),
          new Vector3(0f, 0f, 0f)
        },
        new object[]
        {
          new Vector3(0f, 1f, 0f),
          new Vector3(1f, 1f, 1f),
          new Vector3(0f, 0f, 0f)
        },
        new object[]
        {
          new Vector3(0f, 0f, 1f),
          new Vector3(1f, 1f, 1f),
          new Vector3(0f, 0f, 0f)
        }
      };
  }
}