using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NiceGraphicLibrary.Utility.Cooldown
{
  /// <summary>
  /// Object to return a value for the passed time within an interval.
  /// Inner timer stops if Time.timeScale == 0.
  /// </summary>
  public class CooldownTimeStampTimer : ITakesDateTimeProvider, IStoppableCoolDownTimer<float>
  {

    public bool WornOff => PassedTimeRatio == 1f;

    public float PassedSeconds => (PassedTimeRatio * Convert.ToSingle(_miliSecondsToPass)) / 1000f;

    public bool IsStopped => _isStopped;

    public float SecondsToPass
    {
      get => Convert.ToSingle(_miliSecondsToPass);
      set => _miliSecondsToPass = Convert.ToDouble(Mathf.Abs(value) * 1000f);
    }

    public float PassedTimeRatio
    {
      get
      {
        if (_isStopped)
        {
          return Convert.ToSingle(_passedMiliSecondsBeforeStop / _miliSecondsToPass);
        }
        else
        {
          DateTime currentMoment = _dateTimeProvider.GetNowDateTime();
          TimeSpan difference = currentMoment - _startMoment;
          double clampedDifference = Math.Min(difference.TotalMilliseconds, _miliSecondsToPass) / _miliSecondsToPass;
          return Convert.ToSingle(clampedDifference);
        }
      }
    }

    // Moment in time at which the cool down started to wear off.
    private DateTime _startMoment;

    // Used to calculates the left time until the cool down wears off.
    private double _miliSecondsToPass;

    private IDateTimeProvider _dateTimeProvider = new UtcDateTimeProvider();

    private double _passedMiliSecondsBeforeStop = 0.0;
    private bool _isStopped = false;


    /// <param name="secondsToPass">
    /// Time needs to pass untiles the cool down wears off
    /// Negative value will be converted to a positive one.
    /// </param>
    public CooldownTimeStampTimer(float secondsToPass = 1f)
    {
      SecondsToPass = secondsToPass;
      Reset();
    }

    public void Reset()
      => InnerReset(true);

    public void ResetAndStart()
      => InnerReset(false);

    public void SetDateTimeProvider(IDateTimeProvider provider)
    {
      if (provider != null)
      {
        _dateTimeProvider = provider;
      }
    }

    public void Resume()
    {
      if (_isStopped)
      {
        _startMoment = _dateTimeProvider.GetNowDateTime().AddMilliseconds(-_passedMiliSecondsBeforeStop);
        _isStopped = false;
      }
    }

    public void Stop()
    {
      if (!_isStopped)
      {
        _passedMiliSecondsBeforeStop = Convert.ToDouble(PassedSeconds * 1000f);        
        _isStopped = true;
      }
    }

    private void InnerReset(bool doNotStartAfterReset)
    {
      _startMoment = _dateTimeProvider.GetNowDateTime();
      if (doNotStartAfterReset)
      {
        _passedMiliSecondsBeforeStop = 0f;
        _isStopped = true;
      }
      else
      {
        Resume();
      }
    }

  }
}
