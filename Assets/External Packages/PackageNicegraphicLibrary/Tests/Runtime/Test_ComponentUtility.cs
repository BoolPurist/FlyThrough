using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Tests.Runtime
{
  [TestFixture]
  public class Test_ComponentUtility
  {
    public GameObject _objectToTestOn;

    [SetUp]
    public void SetUpForTest()
    {
      _objectToTestOn = new GameObject(nameof(_objectToTestOn));
    }

    [TearDown]
    public void TearDownForTest()
    {
      GameObject.Destroy(_objectToTestOn);
    }

    [Test]
    public void Test_HasComponentOn()
    {
      // Test on game object without specific component

      Assert.IsFalse(
        ComponentUtility.HasComponentOn<DummyComponent>(_objectToTestOn),
        $"Game object has not the specific component attached !"
        );

      _objectToTestOn.AddComponent<DummyComponent>();

      Assert.IsTrue(
        ComponentUtility.HasComponentOn<DummyComponent>(_objectToTestOn),
        $"Game object has the specific component attached !"
        );
      
    }

    [Test]
    public void Test_EnsureComponentOn()
    {
      var ensuredComponent = ComponentUtility.EnsureComponentOn<DummyComponent>(_objectToTestOn);
      var fetchedComponent = _objectToTestOn.GetComponent<DummyComponent>();

      Assert.IsFalse(ensuredComponent == null, $"Return value is null !");
      Assert.IsFalse(fetchedComponent == null, $"Game object has not the attached component !");             
    }

    public class DummyComponent : MonoBehaviour { }
  }


}