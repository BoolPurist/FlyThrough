using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlyThrough.Tools
{
  public static class SnapSpawnController
  {

#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly static List<SnapSpawnMomentum> _spawningHistroy = new List<SnapSpawnMomentum>();
#pragma warning restore IDE0090 // Use 'new(...)'

    private static bool HasHistory => _spawningHistroy.Count != 0;

    public static void SpawnCommand(GameObject objectToSpawn, float offset, int numberOfSpawns, SpawnDirection spawnDirection, GameObject parentToSpawn = null)
    {
      GameObject[] newSpawnedObjects = SnapSpawner.SpawnAdjacantObject(objectToSpawn, Selection.activeGameObject, offset, numberOfSpawns, spawnDirection);

      if (parentToSpawn != null)
      {
        foreach (GameObject spawnedObject in newSpawnedObjects)
        {
          spawnedObject.transform.SetParent(parentToSpawn.transform);
        }
      }

      _spawningHistroy.Add(new SnapSpawnMomentum(newSpawnedObjects));
    }

    public static void UndoSpawnCommand()
    {
      if (HasHistory)
      {
        int lastIndex = _spawningHistroy.Count - 1;
        SnapSpawnMomentum lastSpawn = _spawningHistroy[lastIndex];

        foreach (GameObject spawnedObject in lastSpawn.SpawnedObjects)
        {
          if (spawnedObject != null)
          {
            GameObject.DestroyImmediate(spawnedObject);
          }
        }

        _spawningHistroy.RemoveAt(lastIndex);
      }

    }

    public static void SelectLastItemCommand()
    {
      if (HasHistory)        
      {
        for (int i = 0; i < _spawningHistroy.Count; i++)
        {
          GameObject[] _pastSpawn = _spawningHistroy[i].SpawnedObjects;

          for (int j = _pastSpawn.Length - 1; j > -1; j--)
          {
            GameObject _spawnedObject = _pastSpawn[j];
            if (_spawnedObject != null)
            {
              Selection.activeObject = _spawnedObject;
              return;
            }

          }
        }
      }
    }

    private static GameObject SpawnCommandPrefab(GameObject objectForPrefab, float offset, SpawnDirection spawnDirection, GameObject parentToSpawn = null, GameObject origin = null)
    {
      GameObject spawnedPrefab = PrefabUtility.InstantiatePrefab(objectForPrefab) as GameObject;
      if (spawnedPrefab != null)
      {
        TranformationForLaterSpawning transformForPrefab = SnapSpawner.CreateEmptyObjectForLaterSnapSpawn(
          objectForPrefab,
          origin != null ? origin : Selection.activeGameObject, 
          offset, 
          spawnDirection
          );

        spawnedPrefab.transform.SetPositionAndRotation(transformForPrefab.Position, transformForPrefab.Rotation);
        if (parentToSpawn != null)
        {
          spawnedPrefab.transform.SetParent(parentToSpawn.transform);
        }        
      }

      
      return spawnedPrefab;
    }

    public static void SpawnCommandPrefab(
      GameObject objectForPrefab, 
      float offset, 
      int numberOfSpawns, 
      SpawnDirection spawnDirection, 
      GameObject 
      parentToSpawn = null
      )
    {
      GameObject[] _spawnedPrefabs = new GameObject[numberOfSpawns];
      GameObject lastSpawnedPrefab = SpawnCommandPrefab(
        objectForPrefab, 
        offset, 
        spawnDirection, 
        parentToSpawn
        );

      _spawnedPrefabs[0] = lastSpawnedPrefab;
      
      for (int i = 1; i < _spawnedPrefabs.Length; i++)
      {
        
        lastSpawnedPrefab = SpawnCommandPrefab(
          objectForPrefab, 
          offset, 
          spawnDirection, 
          parentToSpawn,
          lastSpawnedPrefab
          );

        
        _spawnedPrefabs[i] = lastSpawnedPrefab;

      }

      _spawningHistroy.Add(new SnapSpawnMomentum(_spawnedPrefabs));
    }

  }

}