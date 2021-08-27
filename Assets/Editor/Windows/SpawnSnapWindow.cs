using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlyThrough.Tools { 

  public class SpawnSnapWindow : EditorWindow
  {
    const string WINDOW_TITLE = "Snap placing";

    #region values from inputs field
    private SpawnDirection _spawnDirection = SpawnDirection.Front;
    private GameObject _objectToSpawn;
    private int _numberOfSpawns = 1;
    private GameObject _parentToSpawnIn;
    private float _offset = 0f;
    #endregion

    #region internal facts
    private const int SPACE_SIZE = 5;
    
    private bool ParentIsGiven => _parentToSpawnIn != null;
    
    #endregion

    [MenuItem("Level Tools/Snap Spawn")]
    private static void CreateWindow()
    {
      SpawnSnapWindow window = GetWindow<SpawnSnapWindow>();
      window.titleContent = new GUIContent(WINDOW_TITLE);
      window.minSize = new Vector2(400f, 250f);

      window.Show();
    }

    // Used to style all headers.
    private GUIStyle SeperateLableStyle
    {
      get
      {
        var style = EditorStyles.boldLabel;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        return style;
      }
    }

    private void OnGUI()
    {
      // Spawning input fields
      GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
      EditorGUILayout.Space(SPACE_SIZE);

      _objectToSpawn = (GameObject)EditorGUILayout.ObjectField("Object to spawn.", _objectToSpawn, typeof(GameObject), true);
      _spawnDirection = (SpawnDirection)EditorGUILayout.EnumPopup("Direction to spawn object.", _spawnDirection);
      _numberOfSpawns = EditorGUILayout.IntField("Number of spawns", _numberOfSpawns);

      EditorGUILayout.Space(SPACE_SIZE);
      GUILayout.Label("Optional", SeperateLableStyle);
      EditorGUILayout.Space(SPACE_SIZE);

      _parentToSpawnIn = (GameObject)EditorGUILayout.ObjectField("Parent to spawn in", _parentToSpawnIn, typeof(GameObject), true);
      _offset = EditorGUILayout.FloatField("Offset from origin", _offset);

      GameObject selectedObject = Selection.activeGameObject;

      // Checking if input is valid
      // Spawning buttons for actions.
      EditorGUILayout.Space(SPACE_SIZE);

      if (GUILayout.Button("Select last spawn"))
      {
        SnapSpawnController.SelectLastItemCommand();
      }
      if (GUILayout.Button("Undo last spawn"))
      {
        SnapSpawnController.UndoSpawnCommand();
      }

      if (_objectToSpawn == null)
      {
        EditorGUILayout.HelpBox("No object to spawn, provided.", MessageType.Warning);
      }      
      else if (ParentIsGiven && !_parentToSpawnIn.scene.IsValid())
      {
        EditorGUILayout.HelpBox("Given parent object must be placed in a scene", MessageType.Warning);
      }
      else if (_numberOfSpawns < 1)
      {
        EditorGUILayout.HelpBox("Number of spawned object must be at least one", MessageType.Warning);
      }
      else
      {

        if (selectedObject == null)
        {
          EditorGUILayout.HelpBox("No object selected to use a origin for the next spawn", MessageType.Warning);
        }
        else if (!selectedObject.scene.IsValid())
        {
          EditorGUILayout.HelpBox("Selected object is not placed in the current scene.", MessageType.Warning);
        }        
        else if (GUILayout.Button("Create"))
        {
          SnapSpawnController.SpawnCommand(_objectToSpawn, _offset, _numberOfSpawns, _spawnDirection, _parentToSpawnIn);          
        }
          
      }
    }

  }
}

