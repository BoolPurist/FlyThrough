using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Component.Movement;


namespace NiceGraphicLibrary.Tests.Runtime.Tests_RigidMotion
{
  [TestFixture]
  public class Test_RigidStaticMovement
  {

    
    private RigidStaticMovment _componentToTest;

    [SetUp]
    public void ConstructObject()
    {
      _componentToTest = TestBase_RigidMotion.SetUp<RigidStaticMovment>();
    }

    [TearDown]
    public void DeConstructObject()
    {
      TestBase_RigidMotion.TearDown(_componentToTest.gameObject);
    }

    [Test]
    public void Test_Movement()
    {      
      TestBase_RigidMotion.TestRun_HasMovedForStaticMovementGlobal(_componentToTest);
    }

    [Test]
    public void Test_MovementLocal()
      => TestBase_RigidMotion.TestRun_HasMovedForStaticMovementLocal(_componentToTest);
  }
}