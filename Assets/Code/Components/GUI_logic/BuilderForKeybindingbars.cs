
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FlyThrough
{
  public enum KeyBindinAction { MoveUp, MoveDown, MoveLeft, MoveRight, RotateLeft, RotateRight, ScaleUp, ScaleDown }

  public class BuilderForKeybindingbars : MonoBehaviour
  {
    [SerializeField]
    GameObject _bluePrintForKeybindingBar;

    private KeyBoardBindingState _currentKeyboardBindingState;
    private Dictionary<KeyBindinAction, GameObject> _createdKeyBindings = new Dictionary<KeyBindinAction, GameObject>();
    private List<Button> _buttonsToToggle = new List<Button>();

    public GameObject this[KeyBindinAction action]
    {
      get => _createdKeyBindings[action];
    }
 
    private void Start()
    {

      _currentKeyboardBindingState = SGameInputKeyboard.Instance.CloneBindingKeyboardState();
      CreateKeyBinindBars();
    }
  
    private void CreateKeyBinindBars()
    {
      _createdKeyBindings.Clear();
      DestroyAllChildren();
      CreateBindings();

      void DestroyAllChildren()
      {
        int childrenCount = transform.childCount;
        for (int i = childrenCount - 1; i > -1; i--)
        {
          Destroy(transform.GetChild(i).gameObject);
        }
      }

      void CreateBindings()
      {
        CreateKeybindBar("Move Up", _currentKeyboardBindingState.MoveUp.ToString(), KeyBindinAction.MoveUp);
        CreateKeybindBar("Move Down", _currentKeyboardBindingState.MoveDown.ToString(), KeyBindinAction.MoveDown);
        CreateKeybindBar("Move Left", _currentKeyboardBindingState.MoveLeft.ToString(), KeyBindinAction.MoveLeft);
        CreateKeybindBar("Move Right", _currentKeyboardBindingState.MoveRight.ToString(), KeyBindinAction.MoveRight);
        CreateKeybindBar("Scale Up", _currentKeyboardBindingState.ScaleUp.ToString(), KeyBindinAction.ScaleUp);
        CreateKeybindBar("Scale Down", _currentKeyboardBindingState.ScaleDown.ToString(), KeyBindinAction.ScaleDown);
        CreateKeybindBar("Rotate Left", _currentKeyboardBindingState.RotateLeft.ToString(), KeyBindinAction.RotateLeft);
        CreateKeybindBar("Rotate Right", _currentKeyboardBindingState.RotateRight.ToString(), KeyBindinAction.RotateRight);        
      }
    }

    private void CreateKeybindBar(in string lableText, in string keybinding, in KeyBindinAction keyAction)
    {
      var newKeybindingBar = Instantiate<GameObject>(_bluePrintForKeybindingBar, transform);
      var data = newKeybindingBar.GetComponent<KeybindingBarComponentAccessor>();
      newKeybindingBar.name = $"KeyboardBinding{lableText.Replace(" ", "")}";
      data.KeyLabelText.text = lableText;
      data.KeybindingText.text = keybinding;
      _createdKeyBindings.Add(keyAction, newKeybindingBar);

      Button newButton = data.KeybindingButton;
      _buttonsToToggle.Add(newButton);
      var keyStrokeListener = newButton.GetComponent<ListeningToKeyStrokeOnClick>();
      keyStrokeListener.OnStartListeningForKeystroke += DeactivateAllButtons;
      keyStrokeListener.OnStopListeningForKeystroke += ActiavetAllButtons;
    }

    private void DeactivateAllButtons(ListeningToKeyStrokeOnClick keyStrokeListener)
    {
      ToggleAllButtons(false);
    }

    private void ActiavetAllButtons(ListeningToKeyStrokeOnClick keyStrokeListener)
    {
      ToggleAllButtons(true);
    }

    private void ToggleAllButtons(bool toggleValue)
    {
      foreach (Button oneButtonToToggle in _buttonsToToggle)
      {
        oneButtonToToggle.interactable = toggleValue;
      }
    }
  }

}