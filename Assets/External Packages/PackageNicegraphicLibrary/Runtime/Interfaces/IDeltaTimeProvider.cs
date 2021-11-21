using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Interface to change the source for Time.deltaTime
  /// </summary>
  public interface IDeltaTimeProvider
  {
    /// <summary>
    /// Gets seconds between the last past frame and the current frame
    /// </summary>
    /// <returns>
    /// Passed seconds between the last past frame and the current frame
    /// </returns>
    float GetDelatTime();
  }
}
