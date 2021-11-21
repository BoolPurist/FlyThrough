using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Utility;
using NiceGraphicLibrary.Tests.Editor.Stubs;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_NoRepeatedRandomPicker
  {
    private FakeRandomGenerator _fakefRandomGenerator;
    [SetUp]
    public void SetUp()
    {      
      _fakefRandomGenerator = new FakeRandomGenerator();
      RandomUtility.SetRandomGenerator(_fakefRandomGenerator);
    }

    [TearDown]
    public void TearDown()
    {
      RandomUtility.SetRandomGenerator(new UnityRandomGenerator());
    }

    [Test]
    public void Test_Next()
    {
      // Set up
      var inputValue = Enumerable.Range(1, 10).ToArray();
      int arrayLength = inputValue.Length;
      var randomPicker = new NoRepeatedRandomPicker<int>(inputValue);
      _fakefRandomGenerator.FakeRangeReturnValue = 0;

      int returnedValue = -1;

      HashSet<int> foundValues = AssertOnePickCycle(inputValue, randomPicker);

      returnedValue = randomPicker.Next;
      Assert.IsTrue(
        foundValues.Contains(returnedValue), 
        $"All elements of the sequence were returned before. So this returned element should be known.");

    }
   
    [Test]
    public void Test_NextWithReset()
    {
      // Set up
      var inputValue = Enumerable.Range(1, 10).ToArray();
      int arrayLength = inputValue.Length;
      var randomPicker = new NoRepeatedRandomPicker<int>(inputValue);
      _fakefRandomGenerator.FakeRangeReturnValue = 0;

      int returnedValue = -1;
      
      for (int i = 0; i < arrayLength / 2; i++)
      {
        returnedValue = randomPicker.Next;
      }

      // Reset all elements should be returned after using Next property as many times as
      // the length of inputValue of the given sequence.
      randomPicker.Reset();
      AssertOnePickCycle(inputValue, randomPicker);
      

    }

    private HashSet<int> AssertOnePickCycle(int[] returnedValues, NoRepeatedRandomPicker<int> randomPicker)
    {
      var foundValues = new HashSet<int>();
      int returnedValue = -1;
      for (int i = 0; i < returnedValues.Length; i++)
      {
        returnedValue = randomPicker.Next;

        Assert.IsFalse(foundValues.Contains(returnedValue), $"Not all returned values are unique !");
        foundValues.Add(returnedValue);
      }

      return foundValues;
    }

    [Test]
    public void Test_NextWithEmptyAndNull_ReturnsDefault()
    {
      var randomPicker = new NoRepeatedRandomPicker<int>(null);
      int returnedValue = randomPicker.Next;

      int expectedDefaultValue = default(int);

      Assert.AreEqual(expectedDefaultValue, returnedValue);

      randomPicker = new NoRepeatedRandomPicker<int>(new int[] { } );
      returnedValue = randomPicker.Next;
      Assert.AreEqual(expectedDefaultValue, returnedValue);
    }
  }
}