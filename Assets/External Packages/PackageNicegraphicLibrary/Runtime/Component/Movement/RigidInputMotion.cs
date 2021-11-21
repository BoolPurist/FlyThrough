using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary.Component.Movement
{
  public abstract class RigidInputMotion<TInput> : MonoBehaviour
  {
    [SerializeField]
    private RigidGeometryMotion _Motion;
    [SerializeField]
    private TInput moveLeft;
    [SerializeField]
    private TInput moveRight;
    [SerializeField]
    private TInput moveUp;
    [SerializeField]
    private TInput moveDown;
    [SerializeField]
    private TInput moveForward;
    [SerializeField]
    private TInput moveBack;
    public TInput MoveLeft { get => moveLeft; set => moveLeft = value; }
    public TInput MoveRight { get => moveRight; set => moveRight = value; }
    public TInput MoveUp { get => moveUp; set => moveUp = value; }
    public TInput MoveDown { get => moveDown; set => moveDown = value; }
    public TInput MoveForward { get => moveForward; set => moveForward = value; }
    public TInput MoveBack { get => moveBack; set => moveBack = value; }

    protected IGameInputProvider _inputProvider = new UnityGameInputProvider();


    private void Start()
    {
      if (_Motion == null)
      {
        Debug.LogWarning($"In object {name} in the component {nameof(KeyboardRigidMotion)} the property {nameof(_Motion)}");
      }
    }

    public void Update()
    {
      if (_Motion != null)
      {
        float x = ClampInput(MoveRight);
        x -= ClampInput(MoveLeft);

        float y = ClampInput(MoveUp);
        y -= ClampInput(MoveDown);

        float z = ClampInput(MoveForward);
        z -= ClampInput(MoveBack);

        if (_Motion != null)
        {
          _Motion.OnXMotion(x);
          _Motion.OnYMotion(y);
          _Motion.OnZMotion(z);
        }
      }
    }

    private float ClampInput(TInput pressed) => InputChecker(pressed) ? 1f : 0f;

    protected abstract bool InputChecker(TInput input);

    public void SetKeyButtonProvider(IGameInputProvider newProvider)
    {
      if (newProvider != null)
      {
        _inputProvider = newProvider;
      }
    }

    /// <summary>
    /// Allows to set on which motion component the received input is applied to.
    /// </summary>
    /// <param name="newMotion">
    /// New motion to apply input to.
    /// If null the motion is not changed.
    /// </param>
    public void SetRigidMotion(RigidGeometryMotion newMotion)
    {
      if (newMotion != null)
      {
        _Motion = newMotion;
      }
    }
  }
}