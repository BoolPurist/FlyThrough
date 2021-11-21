namespace NiceGraphicLibrary.Utility.Cooldown
{
  /// <summary>
  /// Provides a way to stop an cool down time and resume it later 
  /// </summary>
  /// <typeparam name="TSeconds">
  /// Type for seconds of the cool down for example like int or float
  /// </typeparam>
  public interface IStoppableCoolDownTimer<TSeconds> : ICooldownTimer<TSeconds>
  {
    /// <summary>
    /// It is true if the cool down timer is stopped.
    /// </summary>
    bool IsStopped { get; }
    /// <summary>
    /// Lets the cool down timer continue with its counting if it was stopped before.
    /// </summary>
    void Resume();
    /// <summary>
    /// Freezes the cool down timer if it is not already stopped.
    /// </summary>
    void Stop();
    /// <summary>
    /// Deletes the progress of cool down timer and starts it again.
    /// </summary>
    void ResetAndStart();
  }
}