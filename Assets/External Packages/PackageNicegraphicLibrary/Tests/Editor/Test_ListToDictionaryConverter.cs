using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_ListToDictionaryConverter
  {
    private class Person
    {
      public string Name;
      public int Age;
    }

    private class InvalidPerson
    {
      public string Name { get; set; } = default;
      public string Age { get; set; } = default;
    }

    private class PersonWithOnlyName
    {

      public string Name { get; set; } = default;
    }



    #region Test data
    private class EmptyPerson { }

#pragma warning disable IDE0090 // Use 'new(...)'
    private ListToDictionaryConverter<string, int, Person> ConverterForTests
      => new ListToDictionaryConverter<string, int, Person>(nameof(Person.Name), nameof(Person.Age));
#pragma warning restore IDE0090 // Use 'new(...)'
    
    #endregion

#pragma warning disable IDE0090 // Use 'new(...)'
    private List<Person> ListForTets
      => new List<Person>()
#pragma warning restore IDE0090 // Use 'new(...)'
        {
          new Person()
          {
            Name = "Max",
            Age = 22
          },
          new Person()
          {
            Name = "Muster",
            Age = 24
          }
        };

    #region Tests
    [Test]
    public void Test_ShouldThrowIfClassHasNoAllProperties()
    {
      Assert.Throws<ArgumentException>(
        ShouldThrowForInvalidPerson,
        $"Should have thrown because {nameof(InvalidPerson)} has for the age property not the type int but string"
        );
      Assert.Throws<ArgumentException>(
        ShouldThrowForOnePropertyWithWrongType,
        $"Should have thrown because {nameof(InvalidPerson)} has for the name property not the type int but string"
        );
      Assert.Throws<ArgumentException>(
        ShouldThrowForOnePropertyWithWrongType,
        $"Should have thrown because {nameof(EmptyPerson)} has no field called Age"
        );
    }



    [Test]
    public void Test_CreateDictionaryFromList()
    {
      // Set up
      var converter = ConverterForTests;
      var list = ListForTets;
      
      // Act 
      IDictionary<string, int> actualDictionary = converter.CreateDictionaryFrom(list);


      // Assert
      var expectedDictionary = new Dictionary<string, int>()
      {
        { "Max", 22 },
        { "Muster", 24 }
      };
      foreach (KeyValuePair<string, int> actualKeyValue in actualDictionary)
      {
        string currentKey = actualKeyValue.Key;
        int currentValue = actualKeyValue.Value;

        Assert.IsTrue(
          expectedDictionary.ContainsKey(currentKey),
          $"{nameof(actualDictionary)} has a key [{currentKey}] which not in {nameof(expectedDictionary)}"
          );
        Assert.AreEqual(
          expectedDictionary[currentKey],
          actualKeyValue.Value,
          $"Value [{currentValue}] in {nameof(actualDictionary)} is not equal to [{expectedDictionary[currentKey]}] in {nameof(expectedDictionary)}"
          );
      }
    }

    [Test]
    public void Test_CreateListFrom()
    {
      // Set up 
      var converter = ConverterForTests;
      var actualDictionary = new Dictionary<string, int>()
      {
        { "Max", 22 },
        { "Muster", 24 }
      };
      var expectedList = ListForTets;

      // Act
      List<Person> actualList = converter.CreateListFrom(actualDictionary);

      // Assert
      int expectedCount = expectedList.Count;
      int actualCount = actualList.Count;
      Assert.AreEqual(
        expectedCount, 
        actualCount, 
        $"{nameof(actualList)} has not the same count [{actualCount}] like {nameof(expectedList)} with the count [{expectedCount}]"
        );

      for (int i = 0; i < actualList.Count; i++)
      {
        Person actualPerson = actualList[i];
        Person expectedPerson = expectedList[i];
        string expectedName = expectedPerson.Name;
        int expectedAge = expectedPerson.Age;
        string actualName = expectedPerson.Name;
        int actualAge = actualPerson.Age;

        Assert.AreEqual(
          expectedName,
          actualName,
          $"In {nameof(actualList)} name [{actualName}] of one element is not equal to [{expectedName}] in {nameof(expectedList)} ."
          );
        Assert.AreEqual(
          expectedPerson.Age,
          actualPerson.Age,
          $"In {nameof(actualList)} name [{actualAge}] of one element is not equal to [{expectedAge}] in {nameof(expectedList)} ."
          );
      }
    }
    #endregion

    #region function which should throw an exception
    private void ShouldThrowForInvalidPerson()
      => new ListToDictionaryConverter<string, int, InvalidPerson>(nameof(Person.Name), nameof(Person.Age));
    

    private void ShouldThrowForOnePropertyWithWrongType()
      => new ListToDictionaryConverter<int, int, InvalidPerson>(nameof(Person.Name), nameof(Person.Age));
    

    private void ShouldThrowForPersonWithOnlyName()
      => new ListToDictionaryConverter<string, int, EmptyPerson>(nameof(PersonWithOnlyName.Name), "Age");
    
    #endregion


  }
}
