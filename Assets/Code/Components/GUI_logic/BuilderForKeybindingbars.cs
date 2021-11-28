
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FlyThrough
{
  /// <summary>
  /// Constructs all elements for an assignment to a player action
  /// </summary>
  [RequireComponent(typeof(HandlingKeyStrokeAssignment))]
  public class BuilderForKeybindingbars : MonoBehaviour
  {
    [SerializeField]
    GameObject _bluePrintForKeybindingBar;

    private HandlingKeyStrokeAssignment _handlerForKeyAssignment;
 
    private void Start()
    {
      _handlerForKeyAssignment = GetComponent<HandlingKeyStrokeAssignment>();
      
      CreateKeyBinindBars();
    }

    private void CreateKeyBinindBars()
    {
      Dictionary<KeyBindinAction, GameObject> createdKeyBindings = new Dictionary<KeyBindinAction, GameObject>();
      KeyBoardBindingState currentKeyboardBindingState = SGameInputKeyboard.Instance.CloneBindingKeyboardState();

      DestroyAllChildren();
      CreateBindings();

      _handlerForKeyAssignment.Initialize(createdKeyBindings);

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
        CreateKeybindBar("Move Up", KeyBindinAction.MoveUp);
        CreateKeybindBar("Move Down", KeyBindinAction.MoveDown);
        CreateKeybindBar("Move Left", KeyBindinAction.MoveLeft);
        CreateKeybindBar("Move Right", KeyBindinAction.MoveRight);
        CreateKeybindBar("Scale Up", KeyBindinAction.ScaleUp);
        CreateKeybindBar("Scale Down", KeyBindinAction.ScaleDown);
        CreateKeybindBar("Rotate Left", KeyBindinAction.RotateLeft);
        CreateKeybindBar("Rotate Right", KeyBindinAction.RotateRight);        
        CreateKeybindBar("Pause", KeyBindinAction.Pause);        
      }

      void CreateKeybindBar(in string lableText, in KeyBindinAction keyAction)
      {
        KeyCode keyBinding = currentKeyboardBindingState[keyAction];

        var newKeybindingBar = Instantiate<GameObject>(_bluePrintForKeybindingBar, transform);
        var data = newKeybindingBar.GetComponent<KeybindingBarComponentAccessor>();
        newKeybindingBar.name = $"KeyboardBinding{lableText.Replace(" ", "")}";
        data.KeyLabelText.text = lableText;
        createdKeyBindings.Add(keyAction, newKeybindingBar);

        Button newButton = data.KeybindingButton;
        var keyStrokeListener = newButton.GetComponent<ListeningToKeyStrokeOnClick>();
        keyStrokeListener.CurrentKeyCode = keyBinding;
      }
    }
  }

}