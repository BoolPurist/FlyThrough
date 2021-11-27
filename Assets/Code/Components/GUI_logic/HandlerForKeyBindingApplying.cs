using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [RequireComponent(typeof(BuilderForKeybindingbars))]
  [RequireComponent(typeof(ActivateObjectsAndDeactivateOthers))]
  public class HandlerForKeyBindingApplying : MonoBehaviour
  {

    private BuilderForKeybindingbars _keyBindingProvider;
    private ActivateObjectsAndDeactivateOthers _objectToggler;
    
    private void Start()
    {
      _keyBindingProvider = GetComponent<BuilderForKeybindingbars>();
      _objectToggler = GetComponent<ActivateObjectsAndDeactivateOthers>();
    }

    public void OnCancleNewSettings()
    {
      KeyBoardBindingState keyBindingsBeforeCancleApply = SGameInputKeyboard.Instance.CloneBindingKeyboardState();
      ResetOneKeyBinding(KeyBindinAction.MoveUp, keyBindingsBeforeCancleApply.MoveUp);
      ResetOneKeyBinding(KeyBindinAction.MoveDown, keyBindingsBeforeCancleApply.MoveDown);
      ResetOneKeyBinding(KeyBindinAction.MoveLeft, keyBindingsBeforeCancleApply.MoveLeft);
      ResetOneKeyBinding(KeyBindinAction.MoveRight, keyBindingsBeforeCancleApply.MoveRight);

      ResetOneKeyBinding(KeyBindinAction.RotateLeft, keyBindingsBeforeCancleApply.RotateLeft);
      ResetOneKeyBinding(KeyBindinAction.RotateRight, keyBindingsBeforeCancleApply.RotateRight);
      ResetOneKeyBinding(KeyBindinAction.ScaleUp, keyBindingsBeforeCancleApply.ScaleUp);
      ResetOneKeyBinding(KeyBindinAction.ScaleDown, keyBindingsBeforeCancleApply.ScaleDown);

      _objectToggler.ToggleObjects();

      void ResetOneKeyBinding(in KeyBindinAction action, in KeyCode keyBeforeCancle)
      {
        var keyBinding = _keyBindingProvider[action].GetComponent<KeybindingBarComponentAccessor>();
        keyBinding.KeybindingText.text = keyBeforeCancle.ToString();
      }
    }

    public void OnApplyNewSettings()
    {
      KeyBoardBindingState keyBindingsBeforeCancleApply = SGameInputKeyboard.Instance.CloneBindingKeyboardState();
     
      SetOneKeyBinding(KeyBindinAction.MoveUp);
      SetOneKeyBinding(KeyBindinAction.MoveDown);
      SetOneKeyBinding(KeyBindinAction.MoveLeft);
      SetOneKeyBinding(KeyBindinAction.MoveRight);

      SetOneKeyBinding(KeyBindinAction.ScaleUp);
      SetOneKeyBinding(KeyBindinAction.ScaleDown);
      SetOneKeyBinding(KeyBindinAction.RotateLeft);
      SetOneKeyBinding(KeyBindinAction.RotateRight);

      SGameInputKeyboard.Instance.SetBindingKeyboardState(keyBindingsBeforeCancleApply);

      _objectToggler.ToggleObjects();

      void SetOneKeyBinding(in KeyBindinAction action)
      {
        var keyBindingListener = _keyBindingProvider[action].GetComponentInChildren<ListeningToKeyStrokeOnClick>();
        KeyCode newKeyCode = keyBindingListener.CurrentKeyCode;

        if (newKeyCode != KeyCode.None)
        {
          switch (action)
          {
            case KeyBindinAction.MoveUp:
              keyBindingsBeforeCancleApply.MoveUp = newKeyCode;
              break;
            case KeyBindinAction.MoveDown:
              keyBindingsBeforeCancleApply.MoveDown = newKeyCode;
              break;
            case KeyBindinAction.MoveLeft:
              keyBindingsBeforeCancleApply.MoveLeft= newKeyCode;
              break;
            case KeyBindinAction.MoveRight:
              keyBindingsBeforeCancleApply.MoveRight = newKeyCode;
              break;
            case KeyBindinAction.ScaleUp:
              keyBindingsBeforeCancleApply.ScaleUp = newKeyCode;
              break;
            case KeyBindinAction.ScaleDown:
              keyBindingsBeforeCancleApply.ScaleDown = newKeyCode;
              break;
            case KeyBindinAction.RotateLeft:
              keyBindingsBeforeCancleApply.RotateLeft = newKeyCode;
              break;
            case KeyBindinAction.RotateRight:
              keyBindingsBeforeCancleApply.RotateRight = newKeyCode;
              break;
            default:
              Debug.LogWarning($"For enum {nameof(KeyBindinAction)} the value {action} is not accounted for !");
              break;

          }
        }

      }
    }
    

    
    
  }
}
