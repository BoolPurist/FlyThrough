using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



using NiceGraphicLibrary.Utility.Cooldown;
using NiceGraphicLibrary.Tests.Editor.Stubs;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_CoolDownDeltaTimer
  {
    private CooldownDeltaTimer _timer;

    private const float FAKE_DELTA_TIME_FACTOR = 1f;

    [SetUp]
    public void SetUp()
    {
      _timer = new CooldownDeltaTimer();
      _timer.SetDeltaTimeProvider(new FakeDeltaTimeProvider(FAKE_DELTA_TIME_FACTOR));
    }

    [Test]
    public void Test_UpdateAndPassedTimeFactor()
    {
      const float TIME_TO_PASS = 5f;
      
      _timer.SecondsToPass = TIME_TO_PASS;

      float previousPassedTimeFactor = 0f;
      

      Assert.AreEqual(0f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be zero.");
      Assert.IsFalse(_timer.WornOff, $"Timer should not have worn off yet !");


      for (
        float currentPassedTime = 0f, endTime = TIME_TO_PASS - FAKE_DELTA_TIME_FACTOR; 
        currentPassedTime < endTime;
        currentPassedTime += FAKE_DELTA_TIME_FACTOR
        )
      {
        Assert.AreEqual(currentPassedTime, _timer.PassedSeconds, $"Passed time from time is not correct.");

        _timer.Update();
        Assert.Greater(
          _timer.PassedTimeRatio, 
          previousPassedTimeFactor, 
          $"{nameof(_timer.PassedTimeRatio)} should have been greater than the previous passed time factor."
          );

        previousPassedTimeFactor = _timer.PassedTimeRatio;
        Assert.IsFalse(_timer.WornOff, $"Timer should not have worn off yet !");
      }

      // Last Update leading to end moment
      _timer.Update();
      Assert.AreEqual(1f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be one.");
      Assert.IsTrue(_timer.WornOff, $"Timer should have worn off !");

      _timer.Update();
      Assert.AreEqual(1f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be one still.");
    }
  }
}