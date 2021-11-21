using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.Movement
{
  public class ButtonRigidMotion : RigidInputMotion<string>
  {
    protected override bool InputChecker(string input)
      => base._inputProvider.GetButton(input);
  } 
}