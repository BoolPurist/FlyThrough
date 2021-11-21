using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Component which enables chosen objects and disables other chosen game objects
  /// </summary>
  /// <remarks>
  /// Useful for toggling different GUI menus
  /// </remarks>
  public class EnableDisableObjectsOnClick : ObserverForButtonClick
  {
    [SerializeField, Tooltip("Objects to enable if clicked")]
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0090 // Use 'new(...)'
    private List<GameObject> _toEnableOnClick = new List<GameObject>();
    [SerializeField, Tooltip("Objects to disable if clicked")]
    private List<GameObject> _toDisableOnClick = new List<GameObject>();
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    /// If attached button component is clicked then objects in inspector field [To Disable On Click] as list are enabled
    /// and objects in the field [To Enable On Click] as list are disabled.
    /// </summary>
    protected override void OnButtonClicked()
    {
      ToggleObjects(_toEnableOnClick, true);
      ToggleObjects(_toDisableOnClick, false);
    }

    /// <summary>
    /// Disables or enables all objects.
    /// </summary>
    /// <param name="objectsToToggle">
    /// Objects to be toggled. Assumes the list is not null
    /// </param>
    /// <param name="toggleValue">
    /// If true all objects will be enabled in the list otherwise disabled
    /// </param>
    private void ToggleObjects(List<GameObject> objectsToToggle,in bool toggleValue)
    {
      foreach (GameObject gameObjectToToggle in objectsToToggle)
      {
        if (gameObjectToToggle != null)
        {
          gameObjectToToggle.SetActive(toggleValue);
        }
      }
    }
  }
}