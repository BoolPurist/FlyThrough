using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  
  [RequireComponent(typeof(HandlingKeyStrokeAssignment))]
  public class HandlerForKeyBindingApplying : MonoBehaviour
  {

    private HandlingKeyStrokeAssignment _keyBindingProvider;
    [SerializeField]
    private SwitchToOtherMenu _menuSwitcher;
    
    private void Start()
    {
      _keyBindingProvider = GetComponent<HandlingKeyStrokeAssignment>();
    }

    public void OnCancleNewSettings()
    {
      KeyBoardBindingState keyBindingsBeforeCancleApply = SGameInputKeyboard.Instance.CloneBindingKeyboardState();

      foreach (KeyBindinAction action in KeyBoardBindingState.AllPlayerAction)
      {
        ResetOneKeyBinding(action);
      }

      _menuSwitcher.ToggleObjects();

      void ResetOneKeyBinding(in KeyBindinAction action)
      {        
        _keyBindingProvider[action].KeybindingText.text = keyBindingsBeforeCancleApply[action].ToString();
      }
    }

    public void OnApplyNewSettings()
    {
      KeyBoardBindingState keyBindingsBeforeCancleApply = SGameInputKeyboard.Instance.CloneBindingKeyboardState();

      foreach (KeyBindinAction action in KeyBoardBindingState.AllPlayerAction)
      {
        SetKeyBinding(action);
      }

      SGameInputKeyboard.Instance.SetBindingKeyboardState(keyBindingsBeforeCancleApply);

      _menuSwitcher.ToggleObjects();

      KeyCode SetKeyBinding(KeyBindinAction action) 
        => keyBindingsBeforeCancleApply[action] = _keyBindingProvider[action].KeyListener.CurrentKeyCode;

    }
    

    
    
  }
}
