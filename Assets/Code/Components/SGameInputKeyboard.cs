using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using NiceGraphicLibrary;

namespace FlyThrough
{
  public class SGameInputKeyboard : GameInput<SGameInputKeyboard>
  {
    [SerializeField]
    private KeyDefaultBindingsKeyboard _defaultBindings;

    [SerializeField, ReadOnlyField]    
    private KeyBoardBindingState _currentBindingState;
    

    protected override void Start()
    {
      base.Start();      
      KeyBoardBindingState loadedSettings = KeyBoardBindingState.CreateFromSavedSettings();

      if (loadedSettings == null)
      {
        _currentBindingState = new KeyBoardBindingState();
        _currentBindingState[KeyBindinAction.MoveUp] = _defaultBindings.MoveUpButton; 
        _currentBindingState[KeyBindinAction.MoveDown] = _defaultBindings.MoveDownButton; 
        _currentBindingState[KeyBindinAction.MoveLeft] = _defaultBindings.MoveLeftButton; 
        _currentBindingState[KeyBindinAction.MoveDown] = _defaultBindings.MoveRightButton;
        
        _currentBindingState[KeyBindinAction.ScaleUp] = _defaultBindings.ScaleUpButton; 
        _currentBindingState[KeyBindinAction.ScaleDown] = _defaultBindings.ScaleDownButton;
        
        _currentBindingState[KeyBindinAction.RotateLeft] = _defaultBindings.RotateLeftButton; 
        _currentBindingState[KeyBindinAction.RotateRight] = _defaultBindings.RotateRightButton;
        
        _currentBindingState[KeyBindinAction.Pause] = _defaultBindings.PauseButton; 
      }
      else
      {
        _currentBindingState = loadedSettings;
      }
    }

    private void Update()
    {
      var inputXYMovment = Vector2.zero;
      var inputScale = 0f;
      var inputRotate = 0f;
      var pushManuelly = 0f;

      inputXYMovment.x += GetKeyButtonInput(_currentBindingState[KeyBindinAction.MoveRight]);
      inputXYMovment.x -= GetKeyButtonInput(_currentBindingState[KeyBindinAction.MoveLeft]);
      inputXYMovment.y += GetKeyButtonInput(_currentBindingState[KeyBindinAction.MoveUp]);
      inputXYMovment.y -= GetKeyButtonInput(_currentBindingState[KeyBindinAction.MoveDown]);

      inputScale += GetKeyButtonInput(_currentBindingState[KeyBindinAction.ScaleUp]);
      inputScale -= GetKeyButtonInput(_currentBindingState[KeyBindinAction.ScaleDown]);

      inputRotate += GetKeyButtonInput(_currentBindingState[KeyBindinAction.RotateLeft]);
      inputRotate -= GetKeyButtonInput(_currentBindingState[KeyBindinAction.RotateRight]);

      pushManuelly += GetKeyButtonInput(_defaultBindings.PushManuellyForwardButton);
      pushManuelly -= GetKeyButtonInput(_defaultBindings.PushManuellyBackButton);


      InvokeOnMovementXYInput(inputXYMovment);
      InvokeOnScaleYInput(inputScale);
      InvokeOnPushManuelly(pushManuelly);
      InvokeOnRotationZInputt(inputRotate);

      if (Input.GetKey(_defaultBindings.PauseButton)) InvokeOnPauseInput();

#pragma warning disable IDE0062 // Make local function 'static'
      float GetKeyButtonInput(KeyCode key) => Input.GetKey(key) ? 1f : 0f;
#pragma warning restore IDE0062 // Make local function 'static'

    }

    public KeyBoardBindingState CloneBindingKeyboardState()
      =>  _currentBindingState.CreateClone();

    public void SetBindingKeyboardState(KeyBoardBindingState newBindingState)
    {
      _currentBindingState = newBindingState;
      KeyBoardBindingState.SaveSetttings(_currentBindingState);
    }





  }
}
