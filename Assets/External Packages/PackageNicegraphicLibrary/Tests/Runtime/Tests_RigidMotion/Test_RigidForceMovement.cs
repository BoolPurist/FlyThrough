using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Component.Movement;

namespace NiceGraphicLibrary.Tests.Runtime.Tests_RigidMotion
{
  [TestFixture]
  public class Test_RigidForceMovement
  {
    private RigidForceMovement _componentToTest;
    private static readonly ForceMode[] _allForceModes = (ForceMode[])Enum.GetValues(typeof(ForceMode));

    [SetUp]
    public void ConstructObject()
    {
      _componentToTest = TestBase_RigidMotion.SetUp<RigidForceMovement>();
    }

    [TearDown]
    public void DeConstructObject()
    {
      TestBase_RigidMotion.TearDown(_componentToTest.gameObject);
    }

    [Test]
    public void Test_MovementGlobal()
    {
      foreach (ForceMode forceMode in _allForceModes)
      {
        _componentToTest.ForceMode = forceMode;
        TestBase_RigidMotion.TestRun_HasMovedGlobal(_componentToTest);
      }      
    }

    [Test]
    public void Test_MovementLocal()
    {
      foreach (ForceMode forceMode in _allForceModes)
      {
        _componentToTest.ForceMode = forceMode;
        TestBase_RigidMotion.TestRun_HasMovedLocal(_componentToTest);
      }
    }
  }
}