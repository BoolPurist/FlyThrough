using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


namespace NiceGraphicLibrary.Tests.Editor.Stubs
{
  public partial class FakeGameInputProvider : IGameInputProvider
  {
#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly Dictionary<KeyCode, InputState> _keyCodes = new Dictionary<KeyCode, InputState>();
    private readonly Dictionary<string, InputState> _keyNames = new Dictionary<string, InputState>();
    private readonly Dictionary<string, InputState> _buttonNames = new Dictionary<string, InputState>();
    private readonly Dictionary<string, float> _axisNames = new Dictionary<string, float>();
#pragma warning restore IDE0090 // Use 'new(...)'

    public Vector2 FakeMousePosition { get; set; } = Vector2.zero;

    public void UpdateKeyCode(InpuType type, KeyCode code, bool newValue)
      => SetInputState(_keyCodes, code, newValue, type);

    public void UpdateKeyName(InpuType type, string name, bool newValue)
      => SetInputState(_keyNames, name, newValue, type);

    public void UpdateButton(InpuType type, string name, bool newValue)
      => SetInputState(_buttonNames, name, newValue, type);

    public void UpdateAxis(string name, float newAxisValue)
    {
      if (_axisNames.ContainsKey(name))
      {
        _axisNames[name] = newAxisValue;
      }
      else
      {
        _axisNames.Add(name, newAxisValue);
      }
    }

    public void ResteInputs()
    {
      SetAllValuesFalse(_keyCodes);
      SetAllValuesFalse(_keyNames);
      SetAllValuesFalse(_buttonNames);

      
      foreach (string axisName in _axisNames.Keys.ToList())
      {
        _axisNames[axisName] = 0f;
      }

#pragma warning disable IDE0062 // Make local function 'static'
      void SetAllValuesFalse<TName>(Dictionary<TName, InputState> dictionary)
      {
        foreach (InputState state in dictionary.Values.ToList())
        {
          state.ResetInputState();
        }
      }
#pragma warning restore IDE0062 // Make local function 'static'
    }

    public float GetAxis(string axisName)
    {
      if (_axisNames.ContainsKey(axisName))
      {
        return _axisNames[axisName];
      }
      else
      {
        return 0f;
      }
    }



    public bool GetButton(string buttonName)
      => GetInputState(_buttonNames, buttonName, InpuType.Pressed);

    public bool GetButtonDown(string buttonName)
      => GetInputState(_buttonNames, buttonName, InpuType.Down);

    public bool GetButtonUp(string buttonName)
      => GetInputState(_buttonNames, buttonName, InpuType.Released);

    public bool GetKey(string keyName)
      => GetInputState(_keyNames, keyName, InpuType.Pressed);

    public bool GetKey(KeyCode keyCode)
      => GetInputState(_keyCodes, keyCode, InpuType.Pressed);

    public bool GetKeyDown(string keyName)
      => GetInputState(_buttonNames, keyName, InpuType.Down);

    public bool GetKeyDown(KeyCode keyCode)
      => GetInputState(_keyCodes, keyCode, InpuType.Down);

    public bool GetKeyUp(string keyName)
      => GetInputState(_buttonNames, keyName, InpuType.Released);

    public bool GetKeyUp(KeyCode keyCode)
      => GetInputState(_keyCodes, keyCode, InpuType.Released);

    public Vector2 GetMousePosition()
     => FakeMousePosition;



    private bool GetInputState<TName>(Dictionary<TName, InputState> dictionary, TName name, InpuType type)
    {
      if (dictionary.ContainsKey(name))
      {
        return dictionary[name].GetInputStateActive(type);
      }
      else
      {
        return false;
      }
    }

    private void SetInputState<TName>(Dictionary<TName, InputState> dictionary, TName name, bool newValue, InpuType type)
    {
      if (dictionary.ContainsKey(name))
      {
        dictionary[name].SetInputStateActive(type, newValue);
      }
      else
      {
        var newState = new InputState();
        newState.SetInputStateActive(type, newValue);
        dictionary.Add(name, newState);
      }
    }

   
  }
}