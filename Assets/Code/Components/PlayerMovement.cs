using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [RequireComponent(typeof(Rigidbody))]
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

    private Rigidbody _rigidBody;

    private GameInput _inputs;

    private Vector2 _inputMovementXY;
    private float _inputScaleY;
    private float _inputRotateZ;

    private void OnValidate()
    {
      _speed = Mathf.Max(0f, _speed);
      _rotationSpeed = Mathf.Max(0f, _rotationSpeed);
      _scaleSpeed = Mathf.Max(0f, _scaleSpeed);
      _maxScaleHeight = Mathf.Max(0f, _maxScaleHeight);
      _minScaleX = Mathf.Clamp(_minScaleX, 0f, _maxScaleHeight);
    }

    #region Input handling
    private void ResetInput()
    {
      _inputMovementXY = Vector2.zero;
      _inputScaleY = 0f;
      _inputRotateZ = 0f;
    }

    #region Handling incoming input 
    
    private void OnMovementXY(Vector2 movementXY)
    {
      _inputMovementXY.x = ClampInput(_inputMovementXY.x + movementXY.x);
      _inputMovementXY.y = ClampInput(_inputMovementXY.y + movementXY.y);
    }

    private void OnScaleY(float scale)
      => _inputScaleY = ClampInput(_inputScaleY + scale);

    private void OnRotateZ(float rotation)
      => _inputRotateZ = ClampInput(_inputRotateZ + rotation);

    #endregion

    private float ClampInput(float input) => Mathf.Clamp(input, -1f, 1f);
    #endregion

    #region component callbacks
    // Start is called before the first frame update
    void Start()
    {
      _rigidBody = gameObject.GetComponent<Rigidbody>();      

      _startScaleX = transform.localScale.x;
      _startScaleY = transform.localScale.y;
    }

    private void OnEnable()
    {
      ResetInput();
      StartListeningForInputs();
    }

    private void OnDisable()
      => StopListeningForInputs();

    private void OnDestroy()
      => StopListeningForInputs();

    private void FixedUpdate()
    {
      AdjustMovementXY();
      AdjustRotationZ();
      AdjustScalingXY();
      ResetInput();
    }
    #endregion

    #region subscriber logic
    private void StartListeningForInputs()
    {
      _inputs = FindObjectOfType<GameInput>();

      if (_inputs == null)
      {
        Debug.LogWarning($"No component {typeof(GameInput).Name} found for listening to player input");
      }
      else
      {
        _inputs.OnMovementXYInput += OnMovementXY;
        _inputs.OnScaleYInput += OnScaleY;
        _inputs.OnRotationZInput += OnRotateZ;
      }
    }

    private void StopListeningForInputs()
    {
      if (_inputs != null)
      {
        _inputs.OnMovementXYInput -= OnMovementXY;
        _inputs.OnScaleYInput -= OnScaleY;
        _inputs.OnRotationZInput -= OnRotateZ;
      }
    }

    #endregion

    #region movement, rotating and scaling

    private void AdjustMovementXY()
    {
      float currentSpeed = _speed * Time.deltaTime;
      _inputMovementXY *= currentSpeed;
      _rigidBody.velocity = _inputMovementXY;
    }

    private void AdjustRotationZ()
    {
      float currentAngularSpeed = _rotationSpeed * Time.deltaTime * _inputRotateZ;
      var rotation = Vector3.forward * currentAngularSpeed;
      _rigidBody.angularVelocity = rotation;
    }

    private void AdjustScalingXY()
    {
      _scaleFactorInHeight = Mathf.Clamp(_scaleFactorInHeight, 0f, 1f);

      float currentScaleFactor = _scaleSpeed * Time.deltaTime;
      var scaleChange = _inputScaleY * currentScaleFactor;
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
      _rigidBody.velocity = Vector3.zero;
      _rigidBody.angularVelocity = Vector3.zero;
    }

    #endregion


  }
}
