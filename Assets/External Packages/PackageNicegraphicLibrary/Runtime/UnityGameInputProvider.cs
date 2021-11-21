using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  public class UnityGameInputProvider : IGameInputProvider
  {
    public float GetAxis(string axisName)
      => Input.GetAxis(axisName);

    public bool GetButton(string buttonName)
      => Input.GetButton(buttonName);

    public bool GetButtonDown(string buttonName)
      => Input.GetButtonDown(buttonName);

    public bool GetButtonUp(string buttonName)
      => Input.GetButtonUp(buttonName);

    public bool GetKey(string keyName)
      => Input.GetKey(keyName);

    public bool GetKey(KeyCode keyCode)
      => Input.GetKey(keyCode);

    public bool GetKeyDown(string keyName)
      => Input.GetKeyDown(keyName);

    public bool GetKeyDown(KeyCode keyCode)
      => Input.GetKeyDown(keyCode);

    public bool GetKeyUp(string keyName)
      => Input.GetKeyUp(keyName);

    public bool GetKeyUp(KeyCode keyCode)
      => Input.GetKeyUp(keyCode);

    public Vector2 GetMousePosition()
     => Input.mousePosition;
  }
}