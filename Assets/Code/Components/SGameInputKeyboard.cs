using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace FlyThrough
{
  public class SGameInputKeyboard : GameInput<SGameInputKeyboard>
  {
    [SerializeField]
    private KeyDefaultBindingsKeyboard _defaultBindings;

    private KeyBoardBindingState _currentBindingState;

    protected override void Start()
    {
      base.Start();      
      KeyBoardBindingState loadedSettings = KeyBoardBindingState.CreateFromSavedSettings();

      if (loadedSettings == null)
      {
        _currentBindingState = new KeyBoardBindingState()
        {
          MoveUp = _defaultBindings.MoveUpButton,
          MoveDown = _defaultBindings.MoveDownButton,
          MoveLeft = _defaultBindings.MoveLeftButton,
          MoveRight = _defaultBindings.MoveRightButton,
          ScaleUp = _defaultBindings.ScaleUpButton,
          ScaleDown = _defaultBindings.ScaleDownButton,
          RotateLeft = _defaultBindings.RotateLeftButton,
          RotateRight = _defaultBindings.RotateRightButton,
          Pause = _defaultBindings.PauseButton,
        };
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

      inputXYMovment.x += GetKeyButtonInput(_currentBindingState.MoveRight);
      inputXYMovment.x -= GetKeyButtonInput(_currentBindingState.MoveLeft);
      inputXYMovment.y += GetKeyButtonInput(_currentBindingState.MoveUp);
      inputXYMovment.y -= GetKeyButtonInput(_currentBindingState.MoveDown);

      inputScale += GetKeyButtonInput(_currentBindingState.ScaleUp);
      inputScale -= GetKeyButtonInput(_currentBindingState.ScaleDown);

      inputRotate += GetKeyButtonInput(_currentBindingState.RotateLeft);
      inputRotate -= GetKeyButtonInput(_currentBindingState.RotateRight);

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
      =>  new KeyBoardBindingState()
      {
        MoveUp = _currentBindingState.MoveUp,
        MoveDown = _currentBindingState.MoveDown,
        MoveLeft = _currentBindingState.MoveLeft,
        MoveRight = _currentBindingState.MoveRight,
        ScaleUp = _currentBindingState.ScaleUp,
        ScaleDown = _currentBindingState.ScaleDown,
        RotateLeft = _currentBindingState.RotateLeft,
        RotateRight = _currentBindingState.RotateRight,
        Pause = _currentBindingState.Pause,
      };

    public void SetBindingKeyboardState(KeyBoardBindingState newBindingState)
    {
      _currentBindingState = newBindingState;
      KeyBoardBindingState.SaveSetttings(_currentBindingState);
    }





  }
}
