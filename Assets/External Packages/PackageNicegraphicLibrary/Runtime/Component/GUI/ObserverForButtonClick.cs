using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Base class for attaching a handler to the click event of an unity button component
  /// </summary>
  [RequireComponent(typeof(Button))]
  public abstract class ObserverForButtonClick : MonoBehaviour
  {
    /// <summary>
    /// Button which this component listens to for a click event.
    /// </summary>
    protected Button attachedButton;

    /// <summary>
    /// Registers the event handler <see cref="OnButtonClicked"/> to unity button <see cref="attachedButton"/>
    /// </summary>
    protected virtual void OnEnable() => RegisterClickHandler();

    /// <summary>
    /// Unregisters the event handler <see cref="OnButtonClicked"/> to unity button <see cref="attachedButton"/>
    /// </summary>

    protected virtual void OnDisable() => UnRegisterClickHandler();

    /// <inheritdoc cref="OnEnable"/>
    protected virtual void OnDestroy() => UnRegisterClickHandler();

    private void RegisterClickHandler()
    {
      attachedButton = GetComponent<Button>();
      if (attachedButton != null)
      {
        attachedButton.onClick.AddListener(OnButtonClicked);
      }
    }

    private void UnRegisterClickHandler()
    {
      if (attachedButton != null)
      {
        attachedButton.onClick.RemoveListener(OnButtonClicked);
      }
    }

    /// <summary>
    /// Code to be executed if the unity button <see cref="attachedButton"/> is clicked
    /// </summary>
    protected abstract void OnButtonClicked();
  } 
}