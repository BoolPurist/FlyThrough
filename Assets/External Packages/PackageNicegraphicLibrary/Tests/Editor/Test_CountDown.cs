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
  public class Test_CountDown
  {
    [Test]
    public void TestCountingDown([Values(0, 5, -5)] int secondsToCountDown)
    {      
      CountDown countDown = SetUpCountDown(secondsToCountDown, out FixedDateTimeProvider fakeDateTimeProvider);

      CountDownInterval(countDown, fakeDateTimeProvider, secondsToCountDown);

      AssertForFinishedCountDown(countDown);
    }
    
    [TestCaseSource(nameof(TestCases_Test_CountingDownWithStopping))]
    public void Test_CountingDownWithStopping(
      int secondsToCountDown,
      int momentToStop,
      int durationOfStop
      )
    {
      CountDown countDown = SetUpCountDown(secondsToCountDown, out FixedDateTimeProvider fakeDateTimeProvider);

      CountDownInterval(countDown, fakeDateTimeProvider, secondsToCountDown, momentToStop);

      countDown.Stop();

      int secondsAfterStop = countDown.PassedSeconds;
      for (int i = durationOfStop; i > 0; i--)
      {
        fakeDateTimeProvider.PlusOneSecond();
        Assert.AreEqual(
          secondsAfterStop, 
          countDown.PassedSeconds, 
          $"Count down should not have be lowered"
          );

        Assert.IsTrue(countDown.IsStopped, $"Count down should have been stopped !");
      }

      
      // Assert if count down works after resuming after the period of being stopped
      countDown.Resume();

      CountDownInterval(countDown, fakeDateTimeProvider, momentToStop);

      AssertForFinishedCountDown(countDown);

    }

    [Test]
    public void Test_Reset([Values(2, 5, 8)]int secondsForCooldownBeforeReset)
    {
      const int COOLDOWN_DURATION = 10;
      CountDown countDown = SetUpCountDown(COOLDOWN_DURATION, out FixedDateTimeProvider fakeDateTimeProvider);
      float expectedRemainingSeconds = COOLDOWN_DURATION - secondsForCooldownBeforeReset;
      fakeDateTimeProvider.ChangeBySeconds(secondsForCooldownBeforeReset);

      Assert.AreEqual(expectedRemainingSeconds, countDown.PassedSeconds, $"Cool down did count down to {expectedRemainingSeconds}");

      countDown.Reset();
      Assert.AreEqual(COOLDOWN_DURATION, countDown.PassedSeconds, $"Count did not reset !");
      Assert.IsTrue(countDown.IsStopped, $"Count down is not stopped after reset !");
      Assert.IsFalse(countDown.WornOff, $"Count down should not be done after reset!");

      fakeDateTimeProvider.ChangeBySeconds(secondsForCooldownBeforeReset);
      countDown.ResetAndStart();
      Assert.IsFalse(countDown.IsStopped, $"Count down should be started after reset with argument startAfterReset being true!");
      Assert.IsFalse(countDown.WornOff, $"Count down should not be done after reset!");

      fakeDateTimeProvider.PlusOneSecond();
      Assert.AreEqual(COOLDOWN_DURATION - 1, countDown.PassedSeconds, $"Cool down should have been decreased by one after reset and start.");
    }

    private static CountDown SetUpCountDown(int secondsToCountDown, out FixedDateTimeProvider provider)
    {
      provider = new FixedDateTimeProvider()
      {
        FixedTimeStamp = new DateTime(0)
      };
      var countDown = new CountDown(secondsToCountDown);
      countDown.SetDateTimeProvider(provider);
      countDown.Resume();
      return countDown;
    }

    private static void CountDownInterval(
      CountDown countDown,
      FixedDateTimeProvider dateTimeProvider,
      int secondsToCountDown,
      int secondToAbort = 0
      )
    {
      for (int i = Mathf.Abs(secondsToCountDown); i > secondToAbort; i--)
      {
        int remaininSeconds = countDown.PassedSeconds;
        Assert.AreEqual(i, remaininSeconds, $"Count down did not go down by one second !");
        Assert.IsFalse(countDown.WornOff, $"Count down should not be done already, Remaining seconds [{remaininSeconds}]");
        Assert.IsFalse(countDown.IsStopped, $"Count down should not have been stopped !");
        dateTimeProvider.PlusOneSecond();
      }
    }

    private static void AssertForFinishedCountDown(CountDown countDown)
    {
      Assert.IsTrue(
        countDown.WornOff,
        $"Count down has not counted down completely, Remaining count is {countDown.PassedSeconds} as seconds"
        );
      Assert.AreEqual(
        0, countDown.PassedSeconds, $"Remaining seconds should be zero if count down is done."
        );
    }

    /// <summary>
    /// 1. Parameter as int: Seconds to count down.
    /// 2. Parameter as int: At which moment in seconds the count is stopped.
    /// 3. Parameter as int: How long the count down is stopped in seconds.
    /// </summary>
    public static object[] TestCases_Test_CountingDownWithStopping
      => new object[]
      {
        new object[] 
        { 
          2, 1, 0
        },
        new object[]
        {
          5, 2, 2
        },
        new object[]
        {
          8, 4, 1
        },
      };




  }
}