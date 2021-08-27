using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{

  [ExecuteInEditMode]
  /// <summary>
  /// Allows to spawn an game object from a given prefab exactly snapped to the origin.
  /// Origin is the game object with this component. During run time origin can be changed.
  /// </summary>
  public class SnapSpawnerComponent : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("Which direction the object is spawn away from the origin")]
    private SpawnDirection _spawnDirectionFromOrigin = SpawnDirection.Front;
    [SerializeField]
    [Tooltip("Object to spawn from the origin, should not be null !")]
    private GameObject _prefabToSpawn;
    [SerializeField]
    [Tooltip("Number of objects spawns accepts only positive values.")]
    private int _numberOfSpawns = 1;

    public SpawnDirection SpawnDirectionFromOrigin
    {
      get => _spawnDirectionFromOrigin;
      set => _spawnDirectionFromOrigin = value;
    }


    public GameObject PrefabToSpawn
    {
      get => _prefabToSpawn;
      set
      {
        if (_prefabToSpawn == null)
        {
          Debug.LogWarning($"[{PrefabToSpawn}] has an object to spawn must not be null !");
        }
        else if (_prefabToSpawn.GetComponent<MeshRenderer>() == null)
        {
          Debug.LogWarning($"[{PrefabToSpawn}] has an object to spawn, has no [{nameof(MeshRenderer)}] component. The object to spawn will not snap correctly.");
        }

        _prefabToSpawn = value;
      }
    }

    public int NumberOfSpawns
    {
      get => _numberOfSpawns;
      set => _numberOfSpawns = Math.Max(0, _numberOfSpawns);
    }

    private GameObject _origin;

    public void SetOrigin(GameObject newOrigin) => _origin = newOrigin;


    private void Awake()
    {
      _origin = gameObject;
    }

    private void OnValidate()
    {
      PrefabToSpawn = _prefabToSpawn;
      NumberOfSpawns = _numberOfSpawns;
      SpawnDirectionFromOrigin = _spawnDirectionFromOrigin;
    }

    [ContextMenu("Snap spawn")]
    public GameObject[] SpawnAdjacantObject() => SnapSpawner.SpawnAdjacantObject(_prefabToSpawn, _origin, _numberOfSpawns, _spawnDirectionFromOrigin);

  }
}
