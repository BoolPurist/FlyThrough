using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Component to end game by a button click.
  /// </summary>
  public class QuitGameOnButtonClick : ObserverForButtonClick
  {
    /// <summary>
    /// Ends the game if button is clicked.
    /// </summary>
    protected override void OnButtonClicked() => Application.Quit();
    
  }
}