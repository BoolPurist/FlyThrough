using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Tests.Editor.Stubs;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_FakeKeyButtonProvider
  {
    public FakeGameInputProvider _fakeProvider;

    [SetUp]
    public void SetUp()
    {
      _fakeProvider = new FakeGameInputProvider();
    }

    [Test]
    public void Test_ReturningFalseIfNotInputSupplied()
      => AssertForAllInputsFalse("Fire", KeyCode.K);

    private void AssertForAllInputsFalse(string inputName, KeyCode code)
    {      
      Assert.IsFalse(_fakeProvider.GetButton(inputName));
      Assert.IsFalse(_fakeProvider.GetKey(inputName));
      Assert.IsFalse(_fakeProvider.GetKey(code));

      Assert.IsFalse(_fakeProvider.GetButtonDown(inputName));
      Assert.IsFalse(_fakeProvider.GetKeyDown(inputName));
      Assert.IsFalse(_fakeProvider.GetKeyDown(code));

      Assert.IsFalse(_fakeProvider.GetButtonUp(inputName));
      Assert.IsFalse(_fakeProvider.GetKeyUp(inputName));
      Assert.IsFalse(_fakeProvider.GetKeyUp(code));

      Assert.AreEqual(0f, _fakeProvider.GetAxis(inputName));
    }

    [Test]
    public void Test_ReturningTrueAfterSet_ResetInputs()
    {
      const string FIRE_NAME = "fire";
      const string AXIS_NAME = "Axis";
      const KeyCode CODE = KeyCode.K;
      InpuType[] inputTypes = (InpuType[])(Enum.GetValues(typeof(InpuType)));

      const float AXIS_VALUE_TO_SET = 1f;
      _fakeProvider.UpdateAxis(AXIS_NAME, AXIS_VALUE_TO_SET);
      Assert.AreEqual(AXIS_VALUE_TO_SET, _fakeProvider.GetAxis(AXIS_NAME));

      foreach (InpuType oneInput in inputTypes)
      {
        _fakeProvider.UpdateButton(oneInput, FIRE_NAME, true);
        _fakeProvider.UpdateKeyCode(oneInput, CODE, true);
        _fakeProvider.UpdateKeyName(oneInput, FIRE_NAME, true);
      }

      Assert.IsTrue(_fakeProvider.GetButton(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetButtonUp(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetButtonDown(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetKey(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetKey(CODE));
      Assert.IsTrue(_fakeProvider.GetKeyUp(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetKeyUp(CODE));
      Assert.IsTrue(_fakeProvider.GetKeyDown(FIRE_NAME));
      Assert.IsTrue(_fakeProvider.GetKeyDown(CODE));

      AssertForAllInputsFalse("Unkown", KeyCode.A);

      _fakeProvider.ResteInputs();

      AssertForAllInputsFalse(FIRE_NAME, CODE);

    }
  }
}