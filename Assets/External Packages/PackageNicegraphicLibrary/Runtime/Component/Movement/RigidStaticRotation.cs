using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.Movement
{
  public class RigidStaticRotation : RigidGeometryMotion
  {
    
    private void ApplyRotationMotion()
    {
      float currentSpeed = Speed * Time.deltaTime;     
      Quaternion targetRotation = Quaternion.AngleAxis(currentSpeed, _movement);      
      _rb.MoveRotation(_rb.rotation * targetRotation);
    }

    private void ApplyLocalRotationMotion()
    {
      float currentSpeed = Speed * Time.deltaTime;
      transform.Rotate(currentSpeed * _movement, Space.World);
    }

    protected override void ApplyMovement()
    {
      if (_AxisLevel == MovementAxisLevel.Global)
      {
        ApplyLocalRotationMotion();        
      }
      else
      {
        ApplyRotationMotion();
      }
    }

    protected override void ProcessAxis()
     => ProcessGlobalAxis();
  }
}