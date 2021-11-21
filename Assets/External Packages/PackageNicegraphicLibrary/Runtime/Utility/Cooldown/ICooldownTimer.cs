namespace NiceGraphicLibrary.Utility.Cooldown
{
  /// <summary>
  /// Demands a cool down which returns a value between 0 and 1 for passed time within an interval 
  /// </summary>
  /// <typeparam name="TSeconds">
  /// Type for seconds of the cool down for example like int or float
  /// </typeparam>
  public interface ICooldownTimer<TSeconds>
  {   
    /// <summary>
    /// Returns a value between 0 and 1. 0 for no time passed and 1 for the cool down has worn off.
    /// </summary>
    float PassedTimeRatio { get; }
    /// <summary>
    /// Returns the passed time between zero and the given end time
    /// </summary>
    TSeconds PassedSeconds { get; }
    /// <summary>
    /// Returns true if the cool down has worn off completely
    /// </summary>
    bool WornOff { get; }
    /// <summary>
    /// Sets time until the cool down wears off.
    /// </summary>
    TSeconds SecondsToPass { get; set; }
    /// <summary>
    /// Deletes passed time as if pauses the cool down.
    /// </summary>
    void Reset();
  }
}
