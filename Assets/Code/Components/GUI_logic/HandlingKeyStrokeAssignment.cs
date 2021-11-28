using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlyThrough
{
  /// <summary>
  /// Logic when player starts assigning or has just assigned a key.
  /// Makes that no more key assignment can be made.
  /// Makes that settings with duplicate key assignment.
  /// </summary>
  public class HandlingKeyStrokeAssignment : MonoBehaviour
  {

         
    // GUI object which let the player assign a key to a playe action.
    private Dictionary<KeyBindinAction, GameObject> _keyBindingsBar;
    private readonly List<Button> _buttonsToToggle = new List<Button>();
    private readonly List<ListeningToKeyStrokeOnClick> _keyBindingListener = new List<ListeningToKeyStrokeOnClick>();

    [SerializeField]
    private TextMeshProUGUI _errorMessageForDuplicateKey;
    [SerializeField]
    private Button _applyButton;

    private bool _wasInit = false;

    public void Initialize(Dictionary<KeyBindinAction, GameObject> keyBindingsBar)
    {
      _keyBindingsBar = keyBindingsBar;
      SetUpInternalCollections();

      _wasInit = true;
      OnEnable();
    }

    private void SetUpInternalCollections()
    {
      foreach (KeyBindinAction action in KeyBoardBindingState.AllPlayerAction)
      {
        KeybindingBarComponentAccessor keybindingBar = this[action];
        _buttonsToToggle.Add(keybindingBar.KeybindingButton);
        _keyBindingListener.Add(keybindingBar.KeyListener);

      }
    }

    public KeybindingBarComponentAccessor this[KeyBindinAction action]
    {
      get => _keyBindingsBar[action].GetComponent<KeybindingBarComponentAccessor>();
    }

    private void OnEnable()
    {
      if (_wasInit)
      {
        StartListening();
      }
    }

    private void OnDisable()
    {
      StopListening();
      ToggleApplyingForKeyAssignedKey(true);
    }

    private void OnDestroy() => StopListening();

    private void StopListening()
    {
      foreach (ListeningToKeyStrokeOnClick keyListener in _keyBindingListener)
      {
        keyListener.OnStartListeningForKeystroke -= OnStartListeningForNewKey;
        keyListener.OnStopListeningForKeystroke -= OnStopListeningForNewKey;
      }
    }

    private void StartListening()
    {
      foreach (ListeningToKeyStrokeOnClick keyListener in _keyBindingListener)
      {
        keyListener.OnStartListeningForKeystroke += OnStartListeningForNewKey;
        keyListener.OnStopListeningForKeystroke += OnStopListeningForNewKey;
      }
    }

    private HashSet<KeyCode> _otherKeys = new HashSet<KeyCode>();


    private void RememberAllOtherAssingedKeys(KeyCode keyToExclude)
    {
      _otherKeys.Clear();
      foreach (KeyBindinAction action in KeyBoardBindingState.AllPlayerAction)
      {
        _otherKeys.Add(this[action].KeyListener.CurrentKeyCode);
      }

      _otherKeys.Remove(keyToExclude);
    }

    private void OnStartListeningForNewKey(ListeningToKeyStrokeOnClick keyStrokeListener)
    {
      RememberAllOtherAssingedKeys(keyStrokeListener.CurrentKeyCode);
      DeactivateAllButtons();
    }

    private void OnStopListeningForNewKey(ListeningToKeyStrokeOnClick keyStrokeListener)
    {
      ToggleApplyingForKeyAssignedKey(!IsDuplicateKey(keyStrokeListener.CurrentKeyCode));

      ActiavetAllButtons();
    }

    private void ToggleApplyingForKeyAssignedKey(in bool toggleValue)
    {
      _applyButton.interactable = toggleValue;
      _errorMessageForDuplicateKey.gameObject.SetActive(!toggleValue);
    }

    private bool IsDuplicateKey(KeyCode keyToCheck) => _otherKeys.Contains(keyToCheck);

    private void DeactivateAllButtons() => ToggleAllButtons(false);
  
    private void ActiavetAllButtons() => ToggleAllButtons(true);
    
    private void ToggleAllButtons(bool toggleValue)
    {
      foreach (Button oneButtonToToggle in _buttonsToToggle)
      {
        oneButtonToToggle.interactable = toggleValue;
      }
    }
  }
}
