using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary.Component.Movement
{
  public class RigidForceMovement : RigidGeometryMotion
  {
    [SerializeField]
#pragma warning disable IDE0044 // Add readonly modifier
    private ForceMode _ForceMode = ForceMode.VelocityChange;
#pragma warning restore IDE0044 // Add readonly modifier

    private Action<Vector3> _movementFunction;

    public ForceMode ForceMode
    {
      get => _ForceMode;
      set => _ForceMode = value;
    }

    protected override void ApplyMovement()
    {
      float currentSpeed = Speed * _timeDelta.GetDelatTime();
      Vector3 currentMovement = _movement * currentSpeed;

      if (IsSetForUnitTest)
      {
        transform.position += currentMovement;
      }
      else
      {
        _rb.AddForce(currentMovement, _ForceMode);
      }     

    }

    protected override void ProcessAxis()
      => ProcessMovementAxis();

  }
}