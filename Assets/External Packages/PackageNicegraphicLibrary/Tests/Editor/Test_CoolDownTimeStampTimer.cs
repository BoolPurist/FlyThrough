using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Tests.Editor.Stubs;
using NiceGraphicLibrary.Utility.Cooldown;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_CoolDownTimeStampTimer
  {
    private const float TIME_TO_PASS = 5f;
    private const float FAKE_DELTA_TIME_FACTOR = 1f;

    private CooldownTimeStampTimer _timer;
#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly FixedDateTimeProvider _fakeDateTimeProvider = new FixedDateTimeProvider();
#pragma warning restore IDE0090 // Use 'new(...)'

    [SetUp]
    public void SetUp()
    {
      _timer = new CooldownTimeStampTimer();
      _fakeDateTimeProvider.FixedTimeStamp = new DateTime(0);
      _timer.SetDateTimeProvider(_fakeDateTimeProvider);
      _timer.SecondsToPass = TIME_TO_PASS;
      _timer.Reset();
    }

    [Test]
    public void Test_PassedTime()
    {
      Assert.AreEqual(0f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be zero.");
      Assert.IsFalse(_timer.WornOff, $"Timer should not have worn off yet !");
      AssertIsStopped(true);

      float previousPassedTimeFactor = 0f;
      _timer.ResetAndStart();

      for (
        float currentPassedTime = 0f, endTime = TIME_TO_PASS - FAKE_DELTA_TIME_FACTOR;
        currentPassedTime < endTime;
        currentPassedTime += FAKE_DELTA_TIME_FACTOR
        )
      {
        AssertPassedTime(currentPassedTime);

        _fakeDateTimeProvider.PlusOneSecond();

        Assert.Greater(
          _timer.PassedTimeRatio,
          previousPassedTimeFactor,
          $"{nameof(_timer.PassedTimeRatio)} should have been greater than the previous passed time factor."
          );

        previousPassedTimeFactor = _timer.PassedTimeRatio;

        Assert.IsFalse(_timer.WornOff, $"Timer should not have worn off yet !");
        AssertIsStopped(false);
      }

      // Last Update leading to end moment
      _fakeDateTimeProvider.PlusOneSecond();
      Assert.AreEqual(1f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be one.");
      Assert.IsTrue(_timer.WornOff, $"Timer should have worn off !");
      AssertIsStopped(false);

      _fakeDateTimeProvider.PlusOneSecond();
      Assert.AreEqual(1f, _timer.PassedTimeRatio, $"{nameof(_timer.PassedTimeRatio)} should be one still.");
      AssertIsStopped(false);
    }

    [Test]
    public void Test_StoppingAndResuming()
    {
      SetUpCoolDownTo(2);

      // Stop the timer
      _timer.Stop();
      // Assert if timer behaves like being stopped.
      AssertIsStopped(true);
      // Forwarding time, should change count of timer while being stopped.
      _fakeDateTimeProvider.PlusOneSecond();
      AssertIsStopped(true);      
      AssertPassedTime(2f);

      // Resume timer
      _timer.Resume();

      // Assert if timer continues to count and with state before the pause.
      AssertIsStopped(false);
      _fakeDateTimeProvider.ChangeBySeconds(2);

      AssertPassedTime(4f);
    }

    [Test]
    public void Test_Resting()
    {
      SetUpCoolDownTo(2);

      _timer.Reset();

      AssertIsStopped(true);
      AssertPassedTime(0f);

      SetUpCoolDownTo(2);

      _timer.ResetAndStart();
      AssertIsStopped(false);
      _fakeDateTimeProvider.PlusOneSecond();
      AssertPassedTime(1f);
    }

    private void SetUpCoolDownTo(int secondsToForward)
    {
      _timer.ResetAndStart();

      // Let the timer count for a while
      _fakeDateTimeProvider.ChangeBySeconds(secondsToForward);
      AssertPassedTime(secondsToForward);
    }

    private void AssertPassedTime(float currentPassedTime)
      => Assert.AreEqual(currentPassedTime, _timer.PassedSeconds, $"Passed time from time is not correct.");

    private void AssertIsStopped(bool shouldBeStopped)
    {
      string errorMessage = shouldBeStopped ? "Timer should be stopped !" : "Timer should not be stopped !";
      Assert.AreEqual(shouldBeStopped, _timer.IsStopped, errorMessage);
    }
  }
}