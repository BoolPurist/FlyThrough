using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Interface for taking implementation for game getting 
  /// </summary>
  public interface ITakesGameInputProviderProvider
  {
    /// <summary>
    /// Used to change the implementation for getting input from the player in a game.
    /// </summary>
    /// <param name="newProvider">
    /// New implementation for getting game input.
    /// </param>
    void SetKeyButtonProvider(IGameInputProvider newProvider);
  }
}