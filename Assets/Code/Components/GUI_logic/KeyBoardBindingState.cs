using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Utility;
using System.Linq;

namespace FlyThrough
{
  [System.Serializable]
  public class KeyBoardBindingState
  {
    // all kinds of actions performable for the player to do in the game like moving, pause etc ..
    public readonly static KeyBindinAction[] AllPlayerAction
      = Enum.GetValues(typeof(KeyBindinAction)).Cast<KeyBindinAction>().ToArray();

    private const string FILE_PATH = "PlayerControlSettings.json";

    private delegate void ActionRef  (ref KeyCode keyBinding);

    [SerializeField]
    private KeyCode MoveUp;
    [SerializeField]
    private KeyCode MoveDown;
    [SerializeField]
    private KeyCode MoveLeft;
    [SerializeField]
    private KeyCode MoveRight;

    [SerializeField]
    private KeyCode ScaleUp;
    [SerializeField]
    private KeyCode ScaleDown;
    [SerializeField]
    private KeyCode RotateLeft;
    [SerializeField]
    private KeyCode RotateRight;

    [SerializeField]
    private KeyCode Pause;

    public KeyCode this[KeyBindinAction action]
    {
      get
      {
        KeyCode returnValue = KeyCode.None;
        MapActionToKeyCodMember(action, (ref KeyCode member) => returnValue = member);
        return returnValue;
      }
      set
      {        
        MapActionToKeyCodMember(action, (ref KeyCode member) => member = value);
      }
    }

    public KeyBoardBindingState CreateClone()
    {
      KeyBoardBindingState clone = new KeyBoardBindingState();

      foreach (KeyBindinAction action in AllPlayerAction)
      {
        clone[action] = this[action];
      }

      return clone;
    }

    
    private void MapActionToKeyCodMember(in KeyBindinAction action, ActionRef execution)
    {
      switch (action)
      {
        case KeyBindinAction.MoveUp:
          execution(ref MoveUp);
          break;
        case KeyBindinAction.MoveDown:
          execution(ref MoveDown);
          break;
        case KeyBindinAction.MoveLeft:
          execution(ref MoveLeft);
          break;
        case KeyBindinAction.MoveRight:
          execution(ref MoveRight);
          break;
        case KeyBindinAction.RotateLeft:
          execution(ref RotateLeft);
          break;
        case KeyBindinAction.RotateRight:
          execution(ref RotateRight);
          break;
        case KeyBindinAction.ScaleUp:
          execution(ref ScaleUp);
          break;
        case KeyBindinAction.ScaleDown:
          execution(ref ScaleDown);
          break;
        case KeyBindinAction.Pause:
          execution(ref Pause);
          break;
        default:
          Debug.LogError($"For enum {nameof(KeyBindinAction)} the value {action} is not accounted for in switch case.");
          break;
      }
    }

    public static KeyBoardBindingState CreateFromSavedSettings()
    {
      if (PersistentDataUtility.FileExits(FILE_PATH))
      {
        string loadedSettings = PersistentDataUtility.ReadFrom(FILE_PATH);
        return JsonUtility.FromJson<KeyBoardBindingState>(loadedSettings);
      }
      else
      {
        return null;
      }
    }

    public static void SaveSetttings(KeyBoardBindingState stateToBeSaved)
    {
      string jsonFromat = JsonUtility.ToJson(stateToBeSaved);
      PersistentDataUtility.Write(FILE_PATH, jsonFromat);
    }
  }
}
