using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Component;
using NiceGraphicLibrary.Component.Movement;

namespace NiceGraphicLibrary.Tests.Runtime.Tests_RigidMotion
{
  [TestFixture]
  public class Test_RigidInterpolatedMovement
  {
    private RigidInterpolatedMovement _componentToTest;

    [SetUp]
    public void ConstructObject()
      => _componentToTest = TestBase_RigidMotion.SetUp<RigidInterpolatedMovement>();

    [TearDown]
    public void DeConstructObject()
      => TestBase_RigidMotion.TearDown(_componentToTest.gameObject);

    [Test]
    public void Test_MovementGlobal()
      => TestBase_RigidMotion.TestRun_HasMovedGlobal(_componentToTest);

    [Test]
    public void Test_MovementLocal()
      => TestBase_RigidMotion.TestRun_HasMovedLocal(_componentToTest);

    private const float TEST_DURATION_GROWING_SPEED = 10f;
    private const float TEST_DELTA_STEP_GROWING_SPEED = 1f;

    [Test]
    public void Test_GrowingMovementGobal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionGrowing, MovementAxisLevel.Global);

    [Test]
    public void Test_GrowingMovementLocal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionGrowing, MovementAxisLevel.Local);

    [Test]
    public void Test_SlowingMovementGobal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionSlowing, MovementAxisLevel.Global);
       
    [Test]
    public void Test_SlowingMovementLocal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionSlowing, MovementAxisLevel.Local);

    [Test]
    public void Test_CounterMovementGobal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionCounter, MovementAxisLevel.Global);

    [Test]
    public void Test_CounterMovementLocal()
      => Test_OneInterpolatedRun(TestBase_RigidMotion.TestRun_ForInterpolatedMotionCounter, MovementAxisLevel.Local);

    private void Test_OneInterpolatedRun(
      Action<RigidInterpolatedMotion, MovementAxisLevel, float, float> testRunFunction,
      MovementAxisLevel locaOrGlobal
      )
    {
      foreach (InterpolationKind interpolation in Enum.GetValues(typeof(InterpolationKind)))
      {
        _componentToTest.AccelerationKind = interpolation;

        testRunFunction(
          _componentToTest,
          locaOrGlobal,
          TEST_DURATION_GROWING_SPEED,
          TEST_DELTA_STEP_GROWING_SPEED
          );
      }
    }
  }
}