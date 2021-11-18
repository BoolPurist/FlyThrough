using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace FlyThrough
{
  public class GameInputKeyboard : GameInput
  {
    [SerializeField]
    private KeyBindingsKeyboard _bindings;

    private void Update()
    {
      var inputXYMovment = Vector2.zero;
      var inputScale = 0f;
      var inputRotate = 0f;
      var pushManuelly = 0f;

      inputXYMovment.x += GetKeyButtonInput(_bindings.MoveRightButton);
      inputXYMovment.x -= GetKeyButtonInput(_bindings.MoveLeftButton);
      inputXYMovment.y += GetKeyButtonInput(_bindings.MoveUpButton);
      inputXYMovment.y -= GetKeyButtonInput(_bindings.MoveDownButton);

      inputScale += GetKeyButtonInput(_bindings.ScaleUpButton);
      inputScale -= GetKeyButtonInput(_bindings.ScaleDownButton);

      inputRotate += GetKeyButtonInput(_bindings.RotateForwardButton);
      inputRotate -= GetKeyButtonInput(_bindings.RotateBackButton);

      pushManuelly += GetKeyButtonInput(_bindings.PushManuellyForwardButton);
      pushManuelly -= GetKeyButtonInput(_bindings.PushManuellyBackButton);


      InvokeOnMovementXYInput(inputXYMovment);
      InvokeOnScaleYInput(inputScale);
      InvokeOnPushManuelly(pushManuelly);
      InvokeOnRotationZInputt(inputRotate);

      if (Input.GetKey(_bindings.PauseButton)) InvokeOnPauseInput();

#pragma warning disable IDE0062 // Make local function 'static'
      float GetKeyButtonInput(KeyCode key) => Input.GetKey(key) ? 1f : 0f;
#pragma warning restore IDE0062 // Make local function 'static'

    }

    
  }
}
