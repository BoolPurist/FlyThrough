using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary.Component.Movement
{
  /// <summary>
  /// Provides input management for keyboard inputs to control a component inheriting from type <see cref="RigidGeometryMotion"/> 
  /// </summary>  
  public class KeyboardRigidMotion : RigidInputMotion<KeyCode>
  {

    protected override bool InputChecker(KeyCode input)
      => _inputProvider.GetKey(input);


  }

}