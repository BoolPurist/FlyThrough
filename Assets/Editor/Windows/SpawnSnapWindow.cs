using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlyThrough.Tools { 

  public class SpawnSnapWindow : EditorWindow
  {
    const string WINDOW_TITLE = "Snap placing";

    #region values from inputs field
    private PlaceSnapDirection _spawnDirection = PlaceSnapDirection.Front;
    private GameObject _objectToSpawn;
    private int _numberOfSpawns = 1;
    private GameObject _parentToSpawnIn;
    private Vector3 _offset;
    private bool _spawnPrefabs = false;
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
      SpawnHeader();
      GetInputFromWindow();
      GameObject selectedObject = Selection.activeGameObject;
      SpawnUndoAndLastSelectionButton();
      if (IsInputValid())
      {
        SpawnCreateButton();
      }

      void SpawnHeader()
      {
        // Spawning input fields
        GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space(SPACE_SIZE);
      }

      void GetInputFromWindow()
      {
        _objectToSpawn = (GameObject)EditorGUILayout.ObjectField("Object to spawn.", _objectToSpawn, typeof(GameObject), true);
        _spawnDirection = (PlaceSnapDirection)EditorGUILayout.EnumPopup("Direction to spawn object.", _spawnDirection);
        _numberOfSpawns = EditorGUILayout.IntField("Number of spawns", _numberOfSpawns);

        EditorGUILayout.Space(SPACE_SIZE);
        GUILayout.Label("Optional", SeperateLableStyle);
        EditorGUILayout.Space(SPACE_SIZE);

        _parentToSpawnIn = (GameObject)EditorGUILayout.ObjectField("Parent to spawn in", _parentToSpawnIn, typeof(GameObject), true);
        _offset = EditorGUILayout.Vector3Field("Offset from origin", _offset);
        _spawnPrefabs = EditorGUILayout.Toggle("With prefab connection", _spawnPrefabs);

      }

      void SpawnUndoAndLastSelectionButton()
      {
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
      }

      bool IsInputValid()
      {
        if (_objectToSpawn == null)
        {
          EditorGUILayout.HelpBox("No object to spawn, provided.", MessageType.Warning);
          return false;
        }
        else if (_objectToSpawn.scene.IsValid())
        {
          EditorGUILayout.HelpBox("Object to spawn must be a prefab !", MessageType.Warning);
          return false;
        }
        else if (ParentIsGiven && !_parentToSpawnIn.scene.IsValid())
        {
          EditorGUILayout.HelpBox("Given parent object must be placed in a scene", MessageType.Warning);
          return false;
        }
        else if (_numberOfSpawns < 1)
        {
          EditorGUILayout.HelpBox("Number of spawned object must be at least one", MessageType.Warning);
          return false;
        }
        else
        {

          if (selectedObject == null)
          {
            EditorGUILayout.HelpBox("No object selected to use a origin for the next spawn", MessageType.Warning);
            return false;
          }
          else if (!selectedObject.scene.IsValid())
          {
            EditorGUILayout.HelpBox("Selected object is not placed in the current scene.", MessageType.Warning);
            return false;
          }

        }

        return true;
      }

      void SpawnCreateButton()
      {
        if (GUILayout.Button("Create"))
        {
          SnapSpawnController.SnapSpawnCommand(
               _objectToSpawn,
               _spawnDirection,
               _offset,
               _numberOfSpawns,
               _spawnPrefabs,
               _parentToSpawnIn == null ? null : _parentToSpawnIn.transform
             );
        }
      }

      
    }



  }
}

