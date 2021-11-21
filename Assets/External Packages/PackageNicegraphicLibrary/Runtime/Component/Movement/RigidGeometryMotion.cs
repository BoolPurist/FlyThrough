using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary.Component.Movement
{
  public abstract class RigidGeometryMotion : MonoBehaviour, IAdjustableForUnitTest
  {
    [SerializeField]
    protected MovementAxisLevel _AxisLevel = MovementAxisLevel.Global;
    [SerializeField]
    [Min(0)]
    private float _Speed = 1f;
    [SerializeField]
#pragma warning disable IDE0044 // Add readonly modifier
    private bool _AllowX = true;
    [SerializeField]
    private bool _AllowY = true;
    [SerializeField]
    private bool _AllowZ = true;
#pragma warning restore IDE0044 // Add readonly modifier

    public bool IsSetForUnitTest { get; set; }

    public Vector3 Velocity => _rb != null ? _rb.velocity : Vector3.zero;
    public float Speed => _Speed;

    public Vector3 AxisINput => _movement;

    public virtual void ForceStand()
      => _rb.velocity = Vector3.zero;

    protected IDeltaTimeProvider _timeDelta = new UnityDeltaTimeProvider();

    public MovementAxisLevel AxisLevel
    {
      get => _AxisLevel;
      set => _AxisLevel = value;
    }

    public void ProvideDeltaTimeWith(IDeltaTimeProvider provider)
    {
      if (provider != null)
      {
        _timeDelta = provider;
      }
    }


    private float _speedModifier = 1f;
    public float SpeedModifier
    {
      get => _speedModifier;
      set
      {
        _speedModifier = Mathf.Max(0f, value);
        _Speed *= _speedModifier;
      }
    }

    


    // movement direction from -1 to 1.
    protected float _axisX = 0f;
    protected float _axisY = 0f;
    protected float _axisZ = 0f;

    protected Vector3 _movement;
    
    protected Rigidbody _rb = null;
    protected void FixedUpdate()
    {
      ProcessAxis();      
      ApplyMovement();
      ResetInput();
    }

    protected abstract void ApplyMovement();

    protected abstract void ProcessAxis();

    protected void ProcessMovementAxis()
    {
      if (_AxisLevel == MovementAxisLevel.Global)
      {
        ProcessGlobalAxis();
      }
      else
      {
        ProcessLocalAxis();
      }
    }

    #region Input on axis
    public void OnXMotion(float newInput)
      => _axisX = ClampInput(newInput);

    public void OnYMotion(float newInput)
      => _axisY = ClampInput(newInput);

    public void OnZMotion(float newInput)
      => _axisZ = ClampInput(newInput);

    protected float ClampInput(float inputAxis)
    {
      inputAxis = inputAxis < 0 ? -1f : inputAxis;
      inputAxis = inputAxis > 0 ? 1f : inputAxis;
      inputAxis = inputAxis == 0f ? 0f : inputAxis;
      return inputAxis;
    }
    #endregion

    protected virtual void Awake()
    {
      _rb = GetComponent<Rigidbody>();
      if (_rb == null)
      {
        _rb = gameObject.AddComponent<Rigidbody>();
      }
    }

    protected void ProcessGlobalAxis()
    {
      if (_AllowX) SetXGlobalDirection();
      if (_AllowY) SetYGlobalDirection();
      if (_AllowZ) SetZGlobalDirection();
    }

    protected void ProcessLocalAxis()
    {
      if (_AllowX) SetXLocalDirection();
      if (_AllowY) SetYLocalDirection();
      if (_AllowZ) SetZLocalDirection();
    }

    private void SetXGlobalDirection() => _movement.x = _axisX;
    private void SetYGlobalDirection() => _movement.y = _axisY;
    private void SetZGlobalDirection() => _movement.z = _axisZ;

    private void SetXLocalDirection() => _movement += transform.right * _axisX;
    private void SetYLocalDirection() => _movement += transform.up * _axisY;
    private void SetZLocalDirection() => _movement += transform.forward * _axisZ;


    private void ResetInput()
    {
      _axisX = 0f;
      _axisY = 0f;
      _axisZ = 0f;
      _movement = Vector3.zero;
    }



    public void SetSpeed(float newSpeed)
    {
      _Speed = newSpeed;
    }

    /// <summary>
    /// Meant to be executed by unit tests so they do not have to wait for the next frame/ not use coroutines/IEnumrator stuff.
    /// If executed outside of unit tests nothing will be done with this call.
    /// </summary>
    public void ExecuteNextFixedUpdate()
    {
      if (IsSetForUnitTest)
      {
        FixedUpdate();
      }
    }

    

  }
}