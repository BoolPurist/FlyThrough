using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility.Cooldown
{
  /// <summary>
  /// Used to manage a countdown independently from Time.deltaTime.
  /// </summary>
  public class CountDown : ITakesDateTimeProvider, IStoppableCoolDownTimer<int>
  {
    /// <summary>
    /// Getter: If true count down is stopped, not counting down further
    /// </summary>
    public bool IsStopped => _isStopped;
    /// <summary>
    /// Getter: If true the count down has counted down completely. 
    /// </summary>
    public bool WornOff => PassedSeconds == 0;

    /// <summary>
    /// Seconds from which the countdown starts to count down.
    /// </summary>
    /// <value>
    /// Negativ values will be converted to positive values
    /// </value>
    public int SecondsToPass
    {
      get => _secondsToPass;
      set
      {
        _secondsToPass = Mathf.Abs(value);
      }
    }
    /// <summary>
    /// Returns remaining seconds to count down. If zero the count down is done.
    /// </summary>
    public int PassedSeconds
    {
      get
      {
        if (_isStopped)
        {
          return _previousRemainingSeconds;
        }
        else
        {
          SetCurrentRemainingSeconds();
          return _previousRemainingSeconds;
        }
      }
    }

    public float PassedTimeRatio => throw new NotImplementedException();


    private IDateTimeProvider _dateTimeProvider = new UtcDateTimeProvider();

    // Moment to pass to reach zero, end of count down.
    private DateTime _timeStamp;
    private int _secondsToPass = 1;

    private bool _isStopped = true;
    private int _previousRemainingSeconds = 1;


    /// <summary>
    /// Sets up count down.
    /// </summary>
    /// <param name="seconds">
    /// Seconds to count down.
    /// Negativ values will be converted to positive values
    /// </param>
    /// <param name="startAfterCreation">
    /// If true then the count down starts after creation. If false the count down is stopped until <see cref="Resume"/> is called
    /// </param>
    public CountDown(int seconds, bool startAfterCreation = false)
    {
      SecondsToPass = seconds;
      if (startAfterCreation)
      {
        ResetAndStart();
      }
      else
      {
        Reset();
      }

    }

    public void ResetAndStart()
      => InnerReset(true);

    public void Reset() 
      => InnerReset(false);

    /// <remarks>
    /// Resets the count down after this call
    /// </remarks>
    public void SetDateTimeProvider(IDateTimeProvider provider)
    {
      if (provider != null)
      {
        _dateTimeProvider = provider;
        Reset();
      }
    }

    /// <summary>
    /// Stops the count down from counting further. <see cref="PassedSeconds"/> does not change anymore.
    /// </summary>
    public void Stop()
    {
      if (!_isStopped)
      {
        SetCurrentRemainingSeconds();
        _isStopped = true;
      }
    }

    /// <summary>
    /// Resumes the counting. <see cref="PassedSeconds"/> changes again. 
    /// Counting starts from the remaining seconds before <see cref="Stop"/> was called.
    /// </summary>
    public void Resume()
    {
      if (_isStopped)
      {
        SetTimeStamp(_previousRemainingSeconds);
        _isStopped = false;
      }
    }


    private void SetCurrentRemainingSeconds()
      => _previousRemainingSeconds = Math.Max(0, (_timeStamp - _dateTimeProvider.GetNowDateTime()).Seconds);

    private void SetTimeStamp(in float seconds)
      => _timeStamp = _dateTimeProvider.GetNowDateTime().AddSeconds(seconds);

    /// <summary>
    /// Resets the countdown. it starts from <see cref="StartSeconds"/> again
    /// </summary>
    /// <param name="startAfterReset">
    /// If true the count down starts from <see cref="StartSeconds"/> to count down right away.
    /// If false the count down does not start until <see cref="Resume"/> is called
    /// </param>
    private void InnerReset(bool startAfterReset = false)
    {
      _isStopped = true;
      _previousRemainingSeconds = SecondsToPass;
      SetTimeStamp(SecondsToPass);

      _isStopped = !startAfterReset;
    }

  }
}