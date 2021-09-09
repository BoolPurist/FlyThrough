using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class PlayerInput : MonoBehaviour
  {
    [System.Serializable]
    public class MovementKeyBindings
    {
      public KeyCode MoveUP = KeyCode.W;
      public KeyCode MoveDown = KeyCode.S;
      public KeyCode MoveLeft = KeyCode.A;
      public KeyCode MoveRight = KeyCode.D;
      public KeyCode MoveForward = KeyCode.Space;
      public KeyCode MoveBackward = KeyCode.C;
    }

    [System.Serializable]
    public class RotationKeyBindings
    {
      public KeyCode RotateLeft = KeyCode.Q;
      public KeyCode RotateRight = KeyCode.E;
    }


    [System.Serializable]
    public class ScalingKeyBindings
    {
      public KeyCode ScaleInHeigth = KeyCode.Z;
      public KeyCode ScaleInWidth = KeyCode.V;
    }

    [System.Serializable]
    public class MenuKeyBindings
    {
      public KeyCode Pause = KeyCode.P;
    }

    #region Key bindings

    [SerializeField]
    private MovementKeyBindings MovementBindings;
    [SerializeField]
    private RotationKeyBindings RotationBindings;
    [SerializeField]
    private ScalingKeyBindings ScalingBindings;
    [SerializeField]
    private MenuKeyBindings MenuBindings;
    

    #endregion

    #region State of pressed key buttons
    public bool MoveUpPressed { get; private set; } = false;
    public bool MoveDownPressed { get; private set; } = false;
    public bool MoveLeftPressed { get; private set; } = false;
    public bool MoveRightPressed { get; private set; } = false;
    public bool MoveForwardPressed { get; private set; } = false;
    public bool MoveBackwardPressed { get; private set; } = false;

    public bool RotateLeftPressed { get; private set; } = false;
    public bool RotateRigthPressed { get; private set; } = false;

    public bool ScaleInHeightPressed { get; private set; } = false;
    public bool ScaleInWidthPressed { get; private set; } = false;

    public bool PausedPressed { get; private set; } = false;
    #endregion


    // Update is called once per frame
    void Update()
    {
      MoveUpPressed = Input.GetKey(MovementBindings.MoveUP);
      MoveDownPressed = Input.GetKey(MovementBindings.MoveDown);
      MoveLeftPressed = Input.GetKey(MovementBindings.MoveLeft);
      MoveRightPressed = Input.GetKey(MovementBindings.MoveRight);
      MoveForwardPressed = Input.GetKey(MovementBindings.MoveForward);
      MoveBackwardPressed = Input.GetKey(MovementBindings.MoveBackward);

      RotateLeftPressed = Input.GetKey(RotationBindings.RotateLeft);
      RotateRigthPressed = Input.GetKey(RotationBindings.RotateRight);

      ScaleInHeightPressed = Input.GetKey(ScalingBindings.ScaleInHeigth);
      ScaleInWidthPressed = Input.GetKey(ScalingBindings.ScaleInWidth);
      PausedPressed = Input.GetKey(MenuBindings.Pause);

    }
  }

  


}