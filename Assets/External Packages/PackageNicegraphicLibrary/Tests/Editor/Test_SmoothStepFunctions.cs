using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_SmoothStepFunctions
  {
    private const float FLOAT_ERROR_TOLERANCE = 0.01f;

    // A Test behaves as an ordinary method
    [TestCaseSource(nameof(TestCases_PositiveValues))]  
    public void Test_SmoothStepGrowingGraph(float endValue, float incrementSteps)
      => Test_IfGraphIsGrowing(Interpolation.SmoothStep, endValue, incrementSteps);
        
    [TestCaseSource(nameof(TestCases_SmoothStepEdgeValues))]
    public void Test_SmoothStepEdgeValues(float endValue)
      => Test_GraphEdges(Interpolation.SmoothStep, endValue);
        
    [TestCaseSource(nameof(TestCases_NegativeValues))]
    public void Test_SmoothStepShrinkingGraph(float endValue, float incrementSteps)
     => Test_IfShrinkingGraph(Interpolation.SmoothStep, endValue, incrementSteps);
    
    [TestCaseSource(nameof(TestCases_PositiveValues))]
    public void Test_SmootherStepGrowingGraph(float endValue, float incrementSteps)    
      => Test_IfGraphIsGrowing(Interpolation.SmootherStep, endValue, incrementSteps);
    
    [TestCaseSource(nameof(TestCases_SmoothStepEdgeValues))]
    public void Test_SmootherStepEdgeValues(float endValue)
      => Test_GraphEdges(Interpolation.SmootherStep, endValue);
    
    [TestCaseSource(nameof(TestCases_NegativeValues))]
    public void Test_SmootherStepShrinkingGraph(float endValue, float incrementSteps)
      => Test_IfShrinkingGraph(Interpolation.SmootherStep, endValue, incrementSteps);
    
    [TestCaseSource(nameof(TestCases_SmoothStepEdgeValues))]
    public void Test_InverseSmoothStepEdgeValues(float endValue)
      => Test_UpsideDownGraphEdges(Interpolation.InverseSmoothStep, endValue);

    [TestCaseSource(nameof(TestCases_PositiveValues))]
    public void Test_InverseSmoothShrinkingGraph(float endValue, float incrementSteps)
      => Test_IfShrinkingGraph(Interpolation.InverseSmoothStep, endValue, incrementSteps);

    [TestCaseSource(nameof(TestCases_SmoothStepEdgeValues))]
    public void Test_InverseSmootherStepEdgeValues(float endValue)
      => Test_UpsideDownGraphEdges(Interpolation.InverseSmootherStep, endValue);

    [TestCaseSource(nameof(TestCases_PositiveValues))]
    public void Test_InverseSmootherShrinkingGraph(float endValue, float incrementSteps)
      => Test_IfShrinkingGraph(Interpolation.InverseSmootherStep, endValue, incrementSteps);

    private void Test_IfShrinkingGraph(
      Func<float, float, float> interpolationFunction,
      float endValue,
      float incrementStep = 0.1f
      )
    {
      float previousValue = interpolationFunction(endValue, 0f);

      for (float currentFactor = incrementStep; currentFactor <= 1f; currentFactor += incrementStep)
      {
        float currentValue = interpolationFunction(endValue, currentFactor);
        Assert.Less(currentValue, previousValue,
          $"{nameof(currentValue)} = {currentValue} should be smaller than {nameof(previousValue)} = {previousValue}"
          );
        previousValue = currentValue;
      }
    }



    private void Test_IfGraphIsGrowing(
      Func<float, float, float> interpolationFunction, 
      float endValue,
      float incrementStep = 0.1f
      )
    {
      float previousValue = interpolationFunction(endValue, 0f);

      for (float currentFactor = incrementStep; currentFactor <= 1f; currentFactor += incrementStep)
      {
        float currentValue = interpolationFunction(endValue, currentFactor);
        Assert.Greater(currentValue, previousValue, 
          $"{nameof(currentValue)} = {currentValue} should be greater than {nameof(previousValue)} = {previousValue}"
          );
        previousValue = currentValue;
      }
    }

    private void Test_GraphEdges(Func<float, float, float> interpolationFunction, float endValue)
    {
      // Act
      float shouldBeZero = interpolationFunction(endValue, 0f);
      float shouldBeEndValue = interpolationFunction(endValue, 1f);
      float shouldBeZeroWithInvalid = interpolationFunction(endValue, -1f);
      float shouldBeEndValueWithInvalid = interpolationFunction(endValue, 2f);

      // Assert
      Assert.IsTrue(
        Utils.AreFloatsEqual(0f, shouldBeZero, FLOAT_ERROR_TOLERANCE), 
        $"result from factor 0 should be 0 but was {shouldBeZero}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(endValue, shouldBeEndValue, FLOAT_ERROR_TOLERANCE), 
        $"result from factor 1 should be {endValue} but was {shouldBeEndValue}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(0f, shouldBeZeroWithInvalid, FLOAT_ERROR_TOLERANCE),
        $"result from factor -1f should be 0 but was {shouldBeZeroWithInvalid}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(endValue, shouldBeEndValueWithInvalid, FLOAT_ERROR_TOLERANCE),
        $"result from factor 2f should be {endValue} but was {shouldBeEndValueWithInvalid}"
        );
    }

    private void Test_UpsideDownGraphEdges(Func<float, float, float> interpolationFunction, float endValue)
    {
      // Act 
      float shouldBeEndValue = interpolationFunction(endValue, 0f);
      float shouldBeZero = interpolationFunction(endValue, 1f);
      float shouldBeEndValueWithInvalid = interpolationFunction(endValue, -1f);
      float shouldBeZeroWithInvalid = interpolationFunction(endValue, 2f);
      
      // Assert
      Assert.IsTrue(
        Utils.AreFloatsEqual(0f, shouldBeZero, FLOAT_ERROR_TOLERANCE),
        $"result from factor 1 should be 0 but was {shouldBeZero}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(endValue, shouldBeEndValue, FLOAT_ERROR_TOLERANCE),
        $"result from factor 0 should be {endValue} but was {shouldBeEndValue}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(0f, shouldBeZeroWithInvalid, FLOAT_ERROR_TOLERANCE),
        $"result from factor 2f should be 0 but was {shouldBeZeroWithInvalid}"
        );
      Assert.IsTrue(
        Utils.AreFloatsEqual(endValue, shouldBeEndValueWithInvalid, FLOAT_ERROR_TOLERANCE),
        $"result from factor -1f should be {endValue} but was {shouldBeEndValueWithInvalid}"
        );
    }


    public static object[] TestCases_SmoothStepEdgeValues
      => new object[]
      {
        2f, 1f, 5f, -5f
      };

    public static object[] TestCases_NegativeValues
      => new object[]
      {
        new object[]{ -2f, 0.05f },
        new object[]{ -1f, 0.1f },
        new object[]{ -5f, 0.2f }
      };

    public static object[] TestCases_PositiveValues
      => new object[]
      {
        new object[]{ 2f, 0.05f },
        new object[]{ 1f, 0.1f },
        new object[]{ 5f, 0.2f }
      };


    
  }
}

