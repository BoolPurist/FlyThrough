using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Utility;
using NiceGraphicLibrary.Extensions;
using NiceGraphicLibrary.Tests.Editor.Stubs;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_RandomUtility
  {
    private FakeRandomGenerator _fakeRandomGenerator;

    [SetUp]
    public void SetUp()
    {
      RandomUtility.SetRandomGenerator(new UnityRandomGenerator());
      _fakeRandomGenerator = new FakeRandomGenerator();
    }   

    [TestCaseSource(nameof(TestCases_NullOrEmpty))]
    public void Test_PickRandomFrom_ShouldReturnDefault(int [] input)
    {
      int actualReturnValue = RandomUtility.PickRandomFrom(input);
      int expectedReturnValue = default;

      Assert.AreEqual(
        actualReturnValue, 
        expectedReturnValue, 
        $"For null or empty array the default value should have been returned {expectedReturnValue}"
        );      
    }

    [TestCaseSource(nameof(TestCases_SomeArrays))]
    public void Test_PickRandomFrom_ShouldReturnRandomValue(int[] input, int numberOfInvocations)
    {
      // Set up      
      int[] cachedSortedInputArry = CreateSortedCopy(input);

      // Act
      int[] expectedRandomValues = CreateExpectedRandomArray(out int[] indexesToFake);
      RandomUtility.SetRandomGenerator(_fakeRandomGenerator);
      int[] actualRandomValues = CreateActualRandomArray(indexesToFake);

      AssertIfActualMatchesExpected(expectedRandomValues, actualRandomValues);

      AssertIfContentOfSequenceWasNotChanged(input, cachedSortedInputArry);

      int[] CreateExpectedRandomArray(out int[] randomIndexes)
      {
        var expectedValues = new List<int>();
        randomIndexes = new int[numberOfInvocations];

        for (int i = 0; i < numberOfInvocations; i++)
        {
          int randomIndex = UnityEngine.Random.Range(0, input.Length);
          randomIndexes[i] = randomIndex;
          int randomValue = input[randomIndex];
          expectedValues.Add(randomValue);
        }
        
        return expectedValues.ToArray();
      }

      int[] CreateActualRandomArray(int[] indexesToFake)
      {
        var actualValues = new List<int>();

        for (int i = 0; i < numberOfInvocations; i++)
        {
          _fakeRandomGenerator.FakeRangeReturnValue = indexesToFake[i];
          actualValues.Add(RandomUtility.PickRandomFrom(input));
        }
        
        return actualValues.ToArray();

      }
    }

    [TestCaseSource(nameof(TestCases_DoesChanceOccure))]
    public void Test_DoesChanceOccure(
      float acutalGivenProbability,
      float actualGivenTotal,
      float actualFakeRandomValue,
      bool expectedReturnValue
      )
    {
      RandomUtility.SetRandomGenerator(_fakeRandomGenerator);
      _fakeRandomGenerator.FakeValue = actualFakeRandomValue;
      bool actualReturnValue = RandomUtility.DoesChanceOccur(acutalGivenProbability, actualGivenTotal);
      Assert.AreEqual(
        expectedReturnValue, 
        actualReturnValue, 
        $"Actual given probability {acutalGivenProbability} " +
        $"Random fake value {actualFakeRandomValue}"
        );
    }

    [TestCaseSource(nameof(TestCases_Shuffel))]
    public void Test_Shuffle(int[] randomRangeValues, int[] actualArray, int[] expectedArray)
    {
      var cachedActualArrayInSorted = CreateSortedCopy(actualArray);
      _fakeRandomGenerator.AddRandomRangeValues(randomRangeValues);
      RandomUtility.SetRandomGenerator(_fakeRandomGenerator);
      RandomUtility.Shuffle(actualArray);

      AssertIfActualMatchesExpected(expectedArray, actualArray);
      AssertIfContentOfSequenceWasNotChanged(actualArray, cachedActualArrayInSorted);
    }

    [Test]
    public void Test_Shuffle_WithNull()
    {
      int[] arrayAsNull = null;
      RandomUtility.Shuffle(arrayAsNull);
      Assert.IsNull(arrayAsNull, $"array with value null should remain null.");
    }

    [TestCaseSource(nameof(TestCases_ChooseByChances))]
    public void Test_ChooseByChances(float[] chances, float randomValues, int expectedIndex)
    {
      RandomUtility.SetRandomGenerator(_fakeRandomGenerator);
      _fakeRandomGenerator.FakeValue = randomValues;

      int actualReturnedIndex = RandomUtility.ChooseByChances(chances);

      Assert.AreEqual(
        expectedIndex,
        actualReturnedIndex,
        $"The expected index was not returned."
        );
    }


    #region Test routines
    private TElement[] CreateSortedCopy<TElement>(IEnumerable<TElement> sequenceToCopy)
    {
      // Set up
      TElement[] cachedSortedInputArray = sequenceToCopy.ToArray();
      Array.Sort(cachedSortedInputArray);
      return cachedSortedInputArray;
    }

    private void AssertIfContentOfSequenceWasNotChanged(int[] sequnceToCheck, int[] originalSorted)
    {
      Array.Sort(sequnceToCheck);
      Assert.AreEqual(
        originalSorted,
        sequnceToCheck,
        $"Content of some element was changed" +
        $"Actual result: {sequnceToCheck.GetValuesAsString()}"
        );
    }

    private void AssertIfActualMatchesExpected(int[] expectedSequence, int[] actualSequence)
    {
      // Assert
      Assert.AreEqual(
        expectedSequence,
        actualSequence,
        $"Random value are not as expected." +
        $"Expected result: {expectedSequence.GetValuesAsString()}" +
        $"Actual result: {actualSequence.GetValuesAsString()}"
        );
    }
    #endregion

    #region Test cases

    public static object[] TestCases_ChooseByChances
      => new object[]
      {
        new object[]
        {
          new float[] { 0.1f, 0.15f, 0.25f, 0.5f },
          0.6f,
          3
        },
        new object[]
        {
          new float[] { 0.1f, 0.15f, 0.25f, 0.5f },
          1f,
          3
        },
        new object[]
        {
          new float[] { 0.1f, 0.15f, 0.25f, 0.5f },
          0.05f,
          0
        },
        new object[]
        {
          new float[] { 0.1f, 0.15f, -0.25f, 0.5f },
          0.12f,
          1
        },
        new object[]
        {
          new float[] { 0.5f, 0.10f, 0.20f },
          0.22f,
          1
        }
      };

    public static object[] TestCases_Shuffel
      => new object[]
      {
        new object[]
        {
          new int[] { 1, 1 },
          Enumerable.Range(1, 2).ToArray(),
          new int[] { 2, 1 },
        },
        new object[]
        {
          new int[] { 0 },
          new int[] { 20 },
          new int[] { 20 },
        },
        new object[]
        {
          new int[] { 0 },
          new int[] {  },
          new int[] {  },
        },
        new object[]
        {
          new int[] { 1, 3, 4, 1, 4},
          Enumerable.Range(1, 5).ToArray(),
          new int[] { 2, 1, 5, 4, 3 },
        }
      };

    public static object[] TestCases_DoesChanceOccure
      => new object[]
      {
        new object[]
        {
          // actual given probability
          0.4f,
          // actual given total
          1f,
          // actual fake random value
          0.5f,
          // expected return value
          false
        },
        new object[]
        {
          // actual given probability
          0.8f,
          // actual given total
          1f,
          // actual fake random value
          0.2f,
          // expected return value
          true
        },
        new object[]
        {
          // actual given probability
          1.4f,
          // actual given total
          2f,
          // actual fake random value
          0.5f,
          // expected return value
          true
        },
        new object[]
        {
          // actual given probability
          0.8f,
          // actual given total
          2f,
          // actual fake random value
          0.5f,
          // expected return value
          false
        },
        // Negative values should be treated as their absolute values
        new object[]
        {
          // actual given probability
          -0.8f,
          // actual given total
          -2f,
          // actual fake random value
          0.5f,
          // expected return value
          false
        }
      };



    public static object[] TestCases_SomeArrays
      => new object[]
      {
        new object[] 
        {
          Enumerable.Range(1, 20).ToArray(),
          10
        },
        new object[]
        {
          Enumerable.Range(1, 2).ToArray(),
          20
        },
        new object[]
        {
          new int[] { 2, -8, 9, 78, 456 },
          3
        }
      };

    public static object[] TestCases_NullOrEmpty
      => new object[] { null, new int[] { } };

    #endregion
  }
}