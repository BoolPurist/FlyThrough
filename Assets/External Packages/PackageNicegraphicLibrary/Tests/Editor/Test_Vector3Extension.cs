using NUnit.Framework;
using UnityEngine;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Extensions;

namespace NiceGraphicLibrary.Tests.Editor
{
  public class Test_Vector3Extension
  {
    [TestCaseSource(nameof(ScaleAddCases))]
    public void Test_AddScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.AddScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} added to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleMinusCases))]
    public void Test_SubScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.SubScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} subtracted to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleXPlusCases))]
    public void Test_AddXScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.AddXScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} added to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleYPlusCase))]
    public void Test_AddYScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.AddYScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} added to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleZPlusCase))]
    public void Test_AddZScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.AddZScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} added to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleXMinusCase))]
    public void Test_SubXScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.SubXScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} subtracted to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleYMinusCase))]
    public void Test_SubYScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.SubYScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} subtracted to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleZMinusCase))]
    public void Test_SubZScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.SubZScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} subtracted to {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleXMultiplByCases))]
    public void Test_MultiplByXScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.MultiplyWithXScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} multiplied with {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleYMultiplByCases))]
    public void Test_MultiplByYScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.MultiplyWithYScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} multiplied with {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(ScaleZMultiplByCases))]
    public void Test_MultiplByZScale(float givenScale, Vector3 givenVector, Vector3 expectedVector)
    {
      // Set up
      Vector3 actualVector = givenVector;

      // execute
      actualVector = actualVector.MultiplyWithZScale(givenScale);

      // Assert
      Assert.True(expectedVector == actualVector, $"Scale {givenScale} multiplied with {givenVector} should result into {expectedVector}, Actual vector is {actualVector}");
    }

    [TestCaseSource(nameof(VectorDivisionCases))]
    public void Test_DivideByVector(Vector3 numerator, Vector3 denominator, Vector3 expectedResult)
    {
      // Execute
      Vector3 returnedVector = numerator.DivideByVector(denominator);

      // Assert
      Assert.IsTrue(returnedVector == expectedResult, $"{numerator} divided by {denominator} should result in {expectedResult}, actual result {returnedVector}");
    }

    [TestCaseSource(nameof(VectorDivisionXCases))]
    public void Test_DivideByXScale(float denominator, Vector3 numerator, Vector3 expectedResult)
    {
      // Execute
      Vector3 returnedVector = numerator.DivideByXScale(denominator);

      // Assert
      Assert.IsTrue(returnedVector == expectedResult, $"{numerator} divided by {denominator} as x scale should result in {expectedResult}, actual result {returnedVector}");
    }

    [TestCaseSource(nameof(VectorDivisionYCases))]
    public void Test_DivideByYScale(float denominator, Vector3 numerator, Vector3 expectedResult)
    {
      // Execute
      Vector3 returnedVector = numerator.DivideByYScale(denominator);

      // Assert
      Assert.IsTrue(returnedVector == expectedResult, $"{numerator} divided by {denominator} as y scale should result in {expectedResult}, actual result {returnedVector}");
    }

    [TestCaseSource(nameof(VectorDivisionZCases))]
    public void Test_DivideByZScale(float denominator, Vector3 numerator, Vector3 expectedResult)
    {
      // Execute
      Vector3 returnedVector = numerator.DivideByZScale(denominator);

      // Assert
      Assert.IsTrue(returnedVector == expectedResult, $"{numerator} divided by {denominator} as z scale should result in {expectedResult}, actual result {returnedVector}");
    }


    #region TestCases
    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleAddCases =
    {
      new object[] {1f, Vector3.zero, Vector3.one},      
      new object[] {3f, Vector3.one, new Vector3(4f, 4f, 4f)},      
      new object[] {-2f, new Vector3(4f, 5f, 3f), new Vector3(2f, 3f, 1f)},      
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.3f, 1.3f, 1.3f)},      
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleMinusCases =
    {
      new object[] {1f, Vector3.zero, -Vector3.one},
      new object[] {3f, Vector3.one, new Vector3(-2f, -2f, -2f)},
      new object[] {2f, new Vector3(4f, 5f, 3f), new Vector3(2f, 3f, 1f)},
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(4f, 4f, 4f)},
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(0.9f, 0.9f, 0.9f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleXPlusCases =
    {
      new object[] {1f, Vector3.zero, Vector3.right},
      new object[] {3f, Vector3.one, new Vector3(4f, 1f, 1f)},
      new object[] {2f, new Vector3(4f, 5f, 3f), new Vector3(6f, 5f, 3f) },
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(2f, 3f, 3f)},
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.3f, 1.1f, 1.1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleYPlusCase =
    {
      new object[] {1f, Vector3.zero, Vector3.up},
      new object[] {5f, Vector3.one, new Vector3(1f, 6f, 1f)},
      new object[] {9f, new Vector3(4f, 5f, 3f), new Vector3(4f, 14f, 3f) },
      new object[] {-3f, new Vector3(3f, 3f, 3f), new Vector3(3f, 0f, 3f)},
      new object[] {0.4f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.1f, 1.5f, 1.1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleZPlusCase =
    {
      new object[] {1f, Vector3.zero, Vector3.forward},
      new object[] {8f, Vector3.one, new Vector3(1f, 1f, 9f)},
      new object[] {1f, new Vector3(4f, 5f, 3f), new Vector3(4f, 5f, 4f) },
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(3f, 3f, 2f) },
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.1f, 1.1f, 1.3f) },
    };


    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleXMinusCase =
    {
      new object[] {1f, Vector3.zero, Vector3.left},
      new object[] {8f, Vector3.one, new Vector3(-7f, 1f, 1f)},
      new object[] {1f, new Vector3(4f, 5f, 3f), new Vector3(3f, 5f, 3f) },
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(4f, 3f, 3f) },
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(0.9f, 1.1f, 1.1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleYMinusCase =
    {
      new object[] {1f, Vector3.zero, Vector3.down},
      new object[] {8f, Vector3.one, new Vector3(1f, -7f, 1f)},
      new object[] {1f, new Vector3(4f, 5f, 3f), new Vector3(4f, 4f, 3f) },
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(3f, 4f, 3f) },
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.1f, 0.9f, 1.1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to add to given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleZMinusCase =
    {
      new object[] {1f, Vector3.zero, Vector3.back},
      new object[] {8f, Vector3.one, new Vector3(1f, 1f, -7f)},
      new object[] {1f, new Vector3(4f, 5f, 3f), new Vector3(4f, 5f, 2f) },
      new object[] {-1f, new Vector3(3f, 3f, 3f), new Vector3(3f, 3f, 4f) },
      new object[] {0.2f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.1f, 1.1f, 0.9f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to be multiplied with given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleXMultiplByCases =
    {
      new object[] {2f, Vector3.one, new Vector3(2f, 1f, 1f)},
      new object[] {-4f, Vector3.one, new Vector3(-4f, 1f, 1f) },
      new object[] {1f, Vector3.one, Vector3.one},
      new object[] {1f, Vector3.zero, Vector3.zero},
      new object[] {0f, Vector3.one, new Vector3(0f, 1f, 1f)},
      new object[] {-1.5f, new Vector3(3f, 3f, 3f), new Vector3(-4.5f, 3f, 3f) },
      new object[] { 0.2f, Vector3.one, new Vector3(0.2f, 1f, 1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to be multiplied with given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleYMultiplByCases =
    {
      new object[] {2f, Vector3.one, new Vector3(1f, 2f, 1f)},
      new object[] {-4f, Vector3.one, new Vector3(1f, -4f, 1f) },
      new object[] {1f, Vector3.one, Vector3.one},
      new object[] {1f, Vector3.zero, Vector3.zero},
      new object[] {0f, Vector3.one, new Vector3(1f, 0f, 1f)},
      new object[] {-1.5f, new Vector3(3f, 3f, 3f), new Vector3(3f, -4.5f, 3f) },
      new object[] { 0.2f, Vector3.one, new Vector3(1f, 0.2f, 1f) },
    };

    /// <summary>
    /// 1. parameter as float given scale to be multiplied with given vector
    /// 2. parameter as vector3, vector to add with the given scale
    /// 3. parameter as vector3, vector which should be the end result
    /// </summary>
    private readonly static object[] ScaleZMultiplByCases =
    {
      new object[] {2f, Vector3.one, new Vector3(1f, 1f, 2f) },
      new object[] {-4f, Vector3.one, new Vector3(1f, 1f, -4f) },
      new object[] {1f, Vector3.one, Vector3.one},
      new object[] {1f, Vector3.zero, Vector3.zero},
      new object[] {0f, Vector3.one, new Vector3(1f, 1f, 0f) },
      new object[] {-1.5f, new Vector3(3f, 3f, 3f), new Vector3(3f, 3f, -4.5f) },
      new object[] { 0.2f, Vector3.one, new Vector3(1f, 1f, 0.2f) },
    };

    /// <summary>
    /// 1. parameter as vector3, vector as numerator
    /// 2. parameter as vector3, vector as denominator
    /// 3. parameter as vector3, vector as expected result.
    /// </summary>
    private readonly static object[] VectorDivisionCases =
    {
      new object[] { new Vector3(2f, 2f, 2f), new Vector3(2f, 2f, 2f), Vector3.one },
      new object[] { new Vector3(4f, 4f, 4f), new Vector3(2f, 2f, 2f), new Vector3(2f, 2f, 2f) },
      new object[] { new Vector3(2f, 2f, 2f), new Vector3(3f, 4f, 5f), new Vector3(2f/3f, 2f/4f, 2f/5f) },
      new object[] { new Vector3(10f, 3.2f, -8f), new Vector3(3f, 4f, 5f), new Vector3(10f/3f, 3.2f/4f, -8f/5f) },
    };

    /// <summary>
    /// 1. parameter as float, x scale as denominator
    /// 2. parameter as vector3, vector as numerator
    /// 3. parameter as vector3, vector as expected result.
    /// </summary>
    private readonly static object[] VectorDivisionXCases =
    {
      new object[] { 2f, new Vector3(2f, 2f, 2f), new Vector3(1f, 2f, 2f) },
      new object[] { 4f, new Vector3(4f, 4f, 4f), new Vector3(1f, 4f, 4f) },
      new object[] { 3f, new Vector3(2f, 4f, 5f), new Vector3(2f/3f, 4f, 5f) },
      new object[] { 3.2f, new Vector3(5f, 2f, 3f),  new Vector3(5f/3.2f, 2f, 3f)},
    };

    /// <summary>
    /// 1. parameter as float, y scale as denominator
    /// 2. parameter as vector3, vector as numerator
    /// 3. parameter as vector3, vector as expected result.
    /// </summary>
    private readonly static object[] VectorDivisionYCases =
    {
      new object[] { 2f, new Vector3(2f, 2f, 2f), new Vector3(2f, 1f, 2f) },
      new object[] { 4f, new Vector3(4f, 4f, 4f), new Vector3(4f, 1f, 4f) },
      new object[] { 3f, new Vector3(2f, 4f, 5f), new Vector3(2f, 4f/3f, 5f) },
      new object[] { 3.2f, new Vector3(5f, 2f, 3f),  new Vector3(5f, 2f/3.2f, 3f)},
    };

    /// <summary>
    /// 1. parameter as float, z scale as denominator
    /// 2. parameter as vector3, vector as numerator
    /// 3. parameter as vector3, vector as expected result.
    /// </summary>
    private readonly static object[] VectorDivisionZCases =
    {
      new object[] { 2f, new Vector3(2f, 2f, 2f), new Vector3(2f, 2f, 1f) },
      new object[] { 4f, new Vector3(4f, 4f, 4f), new Vector3(4f, 4f, 1f) },
      new object[] { 3f, new Vector3(2f, 4f, 5f), new Vector3(2f, 4f, 5f / 3f) },
      new object[] { 3.2f, new Vector3(5f, 2f, 3f),  new Vector3(5f, 2f, 3f / 3.2f) },
    };

    #endregion
  
  }
}
