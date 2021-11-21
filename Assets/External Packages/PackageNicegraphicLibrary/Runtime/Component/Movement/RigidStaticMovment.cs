using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.Movement
{
  public class RigidStaticMovment : RigidGeometryMotion
  {

    protected override void ApplyMovement()
    {
      float delatTime = _timeDelta.GetDelatTime();
      float currentSpeed = Speed * delatTime;
      Vector3 movement = currentSpeed * _movement;      
      Vector3 nextPosition = _rb.position + movement;

      if (IsSetForUnitTest)
      {
        transform.position = nextPosition;
      }
      else
      {
        _rb.MovePosition(nextPosition);
      }

    }

    protected override void ProcessAxis()
     => ProcessMovementAxis();
  }
}