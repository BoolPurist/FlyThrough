using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary
{
  public interface IGameInputProvider
  {
    bool GetKey(string keyName);
    bool GetKey(KeyCode keyCode);
    bool GetKeyDown(string keyName);
    bool GetKeyDown(KeyCode keyCode);
    bool GetKeyUp(string keyName);
    bool GetKeyUp(KeyCode keyCode);

    bool GetButton(string buttonName);
    bool GetButtonDown(string buttonName);
    bool GetButtonUp(string buttonName);

    float GetAxis(string axisName);

    Vector2 GetMousePosition();
  }
}