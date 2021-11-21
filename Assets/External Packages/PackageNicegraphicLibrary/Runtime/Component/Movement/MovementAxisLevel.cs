using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceGraphicLibrary.Component.Movement
{
  /// <summary>
  /// Determines if the object is moved along global axis or its local axis, orientation influenced by rotation.
  /// </summary>
  public enum MovementAxisLevel
  {
    /// <summary>
    /// Object is moved along the global axis, ignoring rotation
    /// </summary>
    Global,
    /// <summary>
    /// Object is moved along its local axis, considering its rotation
    /// </summary>
    Local
  }
}
