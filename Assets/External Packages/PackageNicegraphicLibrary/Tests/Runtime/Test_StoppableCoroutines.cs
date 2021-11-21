using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Utility.Coroutines;

namespace NiceGraphicLibrary.Tests.Runtime
{ 
  [TestFixture]
  public class Test_StoppableCoroutines
  {
    private const float TIME_SCALE_SLOW = 100f;
    private const float DELAY_TIME = 2f;
    private const float INTERVAL_TIME = 2f;
    // Used to simulate execution time for code in executed in loop.
    // Loop means that code is started again after done.
    private const float LOOP_TIME = 1f;

    private GameObject _object;
    private MonoBehaviour _component;

    [SetUp]
    public void ConstructObject()
    {
      _object = new GameObject();
      _component = _object.AddComponent<DummyComponent>();
      _testCounter = 0;
      Time.timeScale = TIME_SCALE_SLOW;
    }

    [TearDown]
    public void DestroyNeededGameObject()
    {
      GameObject.Destroy(_object);
      Time.timeScale = 1f;
    }

    #region tests to run

    [UnityTest]
    public IEnumerator Test_ResetCoroutine()
    {
      int counter = 0;

      _component.StartCoroutine(CoroutineToRestart());

      yield return new WaitForSeconds(3f);

      CoroutineUtility.ResetCoroutine(_component, CoroutineToRestart());

      Assert.AreEqual(0f, counter, $"{nameof(counter)} should be reset to zero. Actual value {counter}");

      IEnumerator CoroutineToRestart()
      {
        counter = 0;
        while (true)
        {
          yield return new WaitForSeconds(1f);
          counter++;
        }
      }
    }

    [UnityTest]
    public IEnumerator Test_StartCoroutineDelayed()
    {
      yield return Test_StartDelayed(DELAY_TIME, TestAction);

      StoppableCoroutines TestAction(float delay)
        => CoroutineUtility.StartCoroutineDelayed(_component, () => Coroutine_StartCoroutineDelayed(), delay);

    }

    [UnityTest]
    public IEnumerator Test_StartActionDelayed()
    {
      yield return Test_StartDelayed(DELAY_TIME, TestAction);

      StoppableCoroutines TestAction(float delay)
         => CoroutineUtility.StartActionDelayed(_component, () => _testCounter++, delay);
    }

    [UnityTest]
    public IEnumerator Test_StarCoroutineInInterval()
    {
      yield return Test_StartInterval(INTERVAL_TIME, TestAction);
      StoppableCoroutines TestAction(float delay)
        => CoroutineUtility.StarCoroutineInInterval(_component, () => Coroutine_StartCoroutineDelayed(), delay);
    }

    [UnityTest]
    public IEnumerator Test_StarActionInInterval()
    {
      yield return Test_StartInterval(INTERVAL_TIME, TestAction);
      StoppableCoroutines TestAction(float delay)
         => CoroutineUtility.StartActionInInterval(_component, () => _testCounter++, delay);
    }

    [UnityTest]
    public IEnumerator Test_StartLoop()
    {

      StoppableCoroutines stopper = CoroutineUtility.StartCoroutineInLoop(_component, () => Coroutine_StartCoroutineLoop());
      yield return new WaitForSeconds(1f);

      stopper.StopAllCoroutines();

      Assert.Greater(
        _testCounter,
        0f,
        $"Counter should be greater 0, actual value {_testCounter}"
        );
    }

    [UnityTest]
    public IEnumerator Test_StartActionInLoop()
    {
      const float delaySeconds = 1f;

      StoppableCoroutines stopper = CoroutineUtility.StartActionInLoop(_component, () => _testCounter++);
      yield return new WaitForSeconds(delaySeconds);
      stopper.StopAllCoroutines();

      int endCounter = _testCounter;
      Assert.Greater(endCounter, 0f, $"After {delaySeconds} the counter should have been increased at least once !");


      yield return new WaitForSeconds(delaySeconds);
      Assert.AreEqual(
        endCounter,
        _testCounter,
        $"Test counter should have the still the same value like the test counter {endCounter}, actual Value is {_testCounter}"
        );

    }
    #endregion

    #region Test routines
    private IEnumerator Test_StartDelayed(float delay, Func<float, StoppableCoroutines> delayedCoroutineLogic)
    {
      // Test if after delay the counter is increased.
      const int expectedCounterValue = 1;
      delayedCoroutineLogic(delay);
      yield return new WaitForSeconds(delay + 0.5f);

      Assert.AreEqual(
        expectedCounterValue,
        _testCounter,
        $"_testCounter should have been {expectedCounterValue}, after {delay} seconds. But is was {_testCounter}"
        );

      // Test if after half of the delay canceling of coroutine prevents the increase of counter.
      StoppableCoroutines stopper = delayedCoroutineLogic(delay);

      yield return new WaitForSeconds(delay / 2f);
      stopper.StopAllCoroutines();

      Assert.AreEqual(
        expectedCounterValue,
        _testCounter,
        $"_testCounter should not haven been increased because of canceling the coroutine"
        );
    }

    private IEnumerator Test_StartInterval(float interval, Func<float, StoppableCoroutines> intervalCoroutineLogic)
    {
      // Test if counter is 3 after start + 2 cycle and one second tolerance 
      // because the coroutines are not always executed immediately after 
      // the interval

      const float expectedCounterValue = 3f;
      float firstWaitPeriod = (interval * (expectedCounterValue + 1f));
      StoppableCoroutines stopper = intervalCoroutineLogic(interval);
      yield return new WaitForSeconds(firstWaitPeriod);
      stopper.StopAllCoroutines();

      float freezedCounter = _testCounter;
      Assert.LessOrEqual(
        expectedCounterValue,
        _testCounter,
        $"Counter equal to or less than {expectedCounterValue} after {expectedCounterValue} interval execution, actual value is {_testCounter}."
        );

      // Test if coroutine can be stopped. if it does not work the counter will be 4 after interval + 0.1f

      yield return new WaitForSeconds(interval);

      Assert.AreEqual(
        freezedCounter,
        _testCounter,
        $"Counter should not have been changed and remained at {_testCounter}, actual value {freezedCounter}"
        );

      yield break;
    }


    #endregion

    #region logic and state for test counter to see effect for coroutines
    private static int _testCounter = 0;
    private IEnumerator Coroutine_StartCoroutineDelayed()
    {
      _testCounter++;
      yield break;
    }


    private IEnumerator Coroutine_StartCoroutineLoop()
    {
      _testCounter++;
      yield return new WaitForSeconds(LOOP_TIME);
    }
    #endregion
  }

}
public class DummyComponent : MonoBehaviour { }
