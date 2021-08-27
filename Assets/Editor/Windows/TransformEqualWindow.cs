using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlyThrough.Tools {

  public enum TranformationAction { Transform, Translate, Rotate, Scale }

  public class TransformEqualWindow : EditorWindow
  {
    private const string WINDOW_TITLE = "Transform Objects Equally";
    private const int SPACE_SIZE = 10;

    private GameObject _origin;
    private GameObject _toTransform;
    private TranformationAction _chosenAction = TranformationAction.Transform;


    [MenuItem("Level Tools/Transform Equally")]
    private static void CreateWindow()
    {
      var window = GetWindow<TransformEqualWindow>();
      window.titleContent = new GUIContent(WINDOW_TITLE);
      window.minSize = new Vector2(400f, 250f);

      window.Show();
    }

    private void OnGUI()
    {

      SpawningHeader();
      GettingInput();
      if (CheckingInput())
      {
        DoTranformation();
      }
      


      // Spawning input fields

      void SpawningHeader()
      {
        EditorGUILayout.Space(SPACE_SIZE);
        GUILayout.Label(
            WINDOW_TITLE,
            new GUIStyle()
            {
              fontStyle = FontStyle.Bold,
              alignment = TextAnchor.MiddleCenter
            }
          );
        EditorGUILayout.Space(SPACE_SIZE);
      }

      void GettingInput()
      {
        _origin = (GameObject)EditorGUILayout.ObjectField("Origin of transform:", _origin, typeof(GameObject), true);
        _toTransform = (GameObject)EditorGUILayout.ObjectField("Origin of transform:", _toTransform, typeof(GameObject), true);
        _chosenAction = (TranformationAction)EditorGUILayout.EnumPopup("Transform Action: ", _chosenAction);
      }

      bool CheckingInput()
      {
        if (CheckForValidTransform(_origin))
        {
          SpawnHelpMessage($"Target for the transformation must be provided and placed in a scene");
          return false;
        }
        else if (CheckForValidTransform(_toTransform))
        {
          SpawnHelpMessage($"Target for the transformation must be provided and placed in a scene");
          return false;
        }
        else if (_origin == _toTransform)
        {
          SpawnHelpMessage("Origin and target are the same !");
          return false;
        }
        else
        {
          return true;
        }

        void SpawnHelpMessage(string message) => EditorGUILayout.HelpBox(message, MessageType.Warning);


        bool CheckForValidTransform(GameObject objectToCheck) => objectToCheck == null || !objectToCheck.scene.IsValid();
      }
    
      void DoTranformation()
      {
        EditorGUILayout.Space(SPACE_SIZE);
        if (GUILayout.Button("Transform equally"))
        {
          switch (_chosenAction)
          {
            case TranformationAction.Transform:
              TransformObjectEqual.TransformFromOriginTo(_origin, _toTransform);
              break;
            case TranformationAction.Translate:
              TransformObjectEqual.TranslateFromOriginTo(_origin, _toTransform);
              break;
            case TranformationAction.Rotate:
              TransformObjectEqual.RotateFromOriginTo(_origin, _toTransform);
              break;
            case TranformationAction.Scale:
              TransformObjectEqual.ScaleFromOriginTo(_origin, _toTransform);
              break;
          }
          
        }
      }
    }

    

  }
}

