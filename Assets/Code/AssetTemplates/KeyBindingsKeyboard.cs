using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [CreateAssetMenu(fileName = "new keyboard bindings", menuName = "Data/Bindings/Keyboard")]
  public class KeyBindingsKeyboard : ScriptableObject
  {
    #region Bindings
    [SerializeField]
    private KeyCode MoveUp = KeyCode.W;
    [SerializeField]
    private KeyCode MoveDown = KeyCode.S;
    [SerializeField]
    private KeyCode MoveLeft = KeyCode.A;
    [SerializeField]
    private KeyCode MoveRight = KeyCode.D;
    
    [SerializeField]
    private KeyCode RotateForward = KeyCode.LeftArrow;
    [SerializeField]
    private KeyCode RotateBack = KeyCode.RightArrow;

    [SerializeField]
    private KeyCode ScaleUp = KeyCode.UpArrow;
    [SerializeField]
    private KeyCode ScaleDown = KeyCode.DownArrow;
    
    [SerializeField]
    private KeyCode Pause = KeyCode.P;

    [SerializeField]
    private KeyCode PushManuellyForward = KeyCode.Space;
    [SerializeField]
    private KeyCode PushManuellyBack = KeyCode.C;
    #endregion

    #region Getters
    public KeyCode MoveUpButton => MoveUp;
    public KeyCode MoveDownButton => MoveDown;
    public KeyCode MoveLeftButton => MoveLeft;
    public KeyCode MoveRightButton => MoveRight;

    public KeyCode RotateForwardButton => RotateForward;
    public KeyCode RotateBackButton => RotateBack;

    public KeyCode ScaleUpButton => ScaleUp;
    public KeyCode ScaleDownButton => ScaleDown;

    public KeyCode PauseButton => Pause;

    public KeyCode PushManuellyForwardButton => PushManuellyForward;

    public KeyCode PushManuellyBackButton => PushManuellyBack;
    #endregion

  }
}
