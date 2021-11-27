using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Utility;

[System.Serializable]
public class KeyBoardBindingState
{
  private const string FILE_PATH = "PlayerControlSettings.json";

  public KeyCode MoveUp;
  public KeyCode MoveDown;
  public KeyCode MoveLeft;
  public KeyCode MoveRight;

  public KeyCode ScaleUp;
  public KeyCode ScaleDown;
  public KeyCode RotateLeft;
  public KeyCode RotateRight;
  public KeyCode Pause;

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
