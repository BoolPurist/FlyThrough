using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Component;

namespace NiceGraphicLibrary.Component.Movement
{
  public class RigidInterpolatedMovement : RigidInterpolatedMotion
  {
    protected override void ApplyMovement()
    {

      base.CalculateMotionCycleX();
      base.CalculateMotionCycleY();
      base.CalculateMotionCycleZ();

      Vector3 nextMove = Vector3.zero;

      if (_AxisLevel == MovementAxisLevel.Global)
      {
        nextMove = new Vector3(
          _currentSpeedX,
          _currentSpeedY,
          _currentSpeedZ
          );
      }
      else
      {
        nextMove += base._currentSpeedX * transform.TransformDirection(Vector3.right);
        nextMove += base._currentSpeedY * transform.TransformDirection(Vector3.up);
        nextMove += base._currentSpeedZ * transform.TransformDirection(Vector3.forward);
      }

      if (IsSetForUnitTest)
      {
        transform.position += nextMove;
      }
      else
      {
        _rb.MovePosition(_rb.position + nextMove);
      }

    }

    protected override void ProcessAxis()
      => ProcessGlobalAxis();
  }
}