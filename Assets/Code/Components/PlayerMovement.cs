using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(PlayerInput))]
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("Speed at which the players moves left, right, up and down")]
    private float _speed = 100f;
    [SerializeField]
    [Tooltip("Speed at which the players rotates along z axis")]
    private float _rotationSpeed = 100f;
    [SerializeField]
    [Tooltip("Speed at which the player grows in length or shrinks to cube")]
    private float _scaleSpeed = 1;
    [SerializeField]
    [Tooltip("Max length the player can grow")]
    private float _maxScaleHeight = 3f;
    [SerializeField]
    [Tooltip("Width of player if the player has grown to maximum length.")]
    private float _minScaleX = 0.2f;

    private float _scaleFactorInHeight = 0f;
    private float _startScaleX;
    private float _startScaleY;

    private PlayerInput _playerInput;
    private Rigidbody _rigidBody;

    private void OnValidate()
    {
      _speed = Mathf.Max(0f, _speed);
      _rotationSpeed = Mathf.Max(0f, _rotationSpeed);
      _scaleSpeed = Mathf.Max(0f, _scaleSpeed);
      _maxScaleHeight = Mathf.Max(0f, _maxScaleHeight);
      _minScaleX = Mathf.Clamp(_minScaleX, 0f, _maxScaleHeight);
    }

    // Start is called before the first frame update
    void Start()
    {
      _playerInput = gameObject.GetComponent<PlayerInput>();
      _rigidBody = gameObject.GetComponent<Rigidbody>();

      _startScaleX = transform.localScale.x;
      _startScaleY = transform.localScale.y;
    }



    private void FixedUpdate()
    {
      AdjustMovementXY();
      AdjustRotationZ();
      AdjustScalingXY();
    }

    private void AdjustMovementXY()
    {
      var movement = new Vector3();

      float currentSpeed = _speed * Time.deltaTime;

      movement.x -= _playerInput.MoveLeftPressed ? currentSpeed : 0f;
      movement.x += _playerInput.MoveRightPressed ? currentSpeed : 0f;
      movement.y += _playerInput.MoveUpPressed ? currentSpeed : 0f;
      movement.y -= _playerInput.MoveDownPressed ? currentSpeed : 0f;

      _rigidBody.velocity = movement;
    }

    private void AdjustRotationZ()
    {
      float currentAngularSpeed = _rotationSpeed * Time.deltaTime;
      var rotation = new Vector3();

      rotation.z += _playerInput.RotateLeftPressed ? currentAngularSpeed : 0f;
      rotation.z -= _playerInput.RotateRigthPressed ? currentAngularSpeed : 0f;

      _rigidBody.angularVelocity = rotation;
    }

    private void AdjustScalingXY()
    {
      _scaleFactorInHeight = Mathf.Clamp(_scaleFactorInHeight, 0f, 1f);

      float currentScaleFactor = _scaleSpeed * Time.deltaTime;
      var scaleChange = 0f;

      scaleChange += _playerInput.ScaleInHeightPressed ? currentScaleFactor : 0f;
      scaleChange -= _playerInput.ScaleInWidthPressed ? currentScaleFactor : 0f;

      _scaleFactorInHeight += scaleChange;
      _scaleFactorInHeight = Mathf.Clamp(_scaleFactorInHeight, 0f, _maxScaleHeight);

      transform.localScale = new Vector3(
        Mathf.Lerp(_minScaleX, 1f, 1f - _scaleFactorInHeight) * _startScaleX,
        Mathf.Lerp(1f, _maxScaleHeight, _scaleFactorInHeight) * _startScaleY,
        transform.localScale.z
      );
    }

    public void StopMovement()
    {
      Vector3 stopper = Vector3.zero;
      _rigidBody.velocity = stopper;
      _rigidBody.angularVelocity = stopper;
    }
  }
}
