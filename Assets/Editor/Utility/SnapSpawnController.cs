using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlyThrough.Tools
{
  /// <summary>
  /// Provides an api for the SnapSpawner to spawn new objects snapped to selected objects 
  /// in scene to spawn.
  /// </summary>
  public static class SnapSpawnController
  {

#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly static List<SnapSpawnMomentum> _spawningHistroy = new List<SnapSpawnMomentum>();
#pragma warning restore IDE0090 // Use 'new(...)'

    private static bool HasHistory => _spawningHistroy.Count != 0;

    /// <summary>
    /// Spawns new objects snapped, in a certain direction, to a given object as origin location.
    /// Will only spawn if play mode is not active.
    /// </summary>
    public static void SnapSpawnCommand(
        GameObject objectToSpawn, 
        PlaceSnapDirection spawnDirection, 
        Vector3 offset, 
        int numberOfSpawns, 
        bool spawnAsPrefab = false,
        Transform parentToSpawnIn = null
      )
    {
      if (!Application.isPlaying)
      {
        
        Func<GameObject, int, GameObject[], GameObject> spawnFunction = spawnAsPrefab ?
          (Func<GameObject, int, GameObject[], GameObject>)SpawnOnePrefabObject : 
          (Func<GameObject, int, GameObject[], GameObject>)SpawnOneCloneObject;
        var newSpawnedObjects = new GameObject[numberOfSpawns];
        GameObject lastSpawnedObject = spawnFunction(Selection.activeGameObject, 0, newSpawnedObjects);
        
        for (int i = 1; i < numberOfSpawns; i++)
        {
          lastSpawnedObject = spawnFunction(lastSpawnedObject, i, newSpawnedObjects);
        }

        _spawningHistroy.Add(new SnapSpawnMomentum(newSpawnedObjects));

        ApplyGivenParent();

        GameObject SpawnOneCloneObject(GameObject origin, int index, GameObject[] historyArray)
        {
          GameObject newSpawnedObject = GameObject.Instantiate(objectToSpawn, null, true);
          PlaceOneObject(newSpawnedObject, origin, index, historyArray);
          return newSpawnedObject;
        }

        GameObject SpawnOnePrefabObject(GameObject origin, int index, GameObject[] historyArray)
        {
          GameObject newSpawnedObject = PrefabUtility.InstantiatePrefab(objectToSpawn) as GameObject;          
          PlaceOneObject(newSpawnedObject, origin, index, historyArray);
          return newSpawnedObject;
        }
        
        void PlaceOneObject(GameObject spawnedObject, GameObject origin, int index, GameObject[] historyArray)
        {
          SnapMover.MoveSnap(origin, spawnedObject, spawnDirection, offset);
          historyArray[index] = spawnedObject;
        }


        void ApplyGivenParent()
        {
          if (parentToSpawnIn != null)
          {
            foreach (GameObject spawnedObject in newSpawnedObjects)
            {              
              spawnedObject.transform.SetParent(parentToSpawnIn);
            }
          }
        }
      }
      
      //GameObject[] newSpawnedObjects = SnapSpawner.SpawnAdjacantObject(objectToSpawn, Selection.activeGameObject, offset, numberOfSpawns, spawnDirection);

      //if (parentToSpawn != null)
      //{
      //  foreach (GameObject spawnedObject in newSpawnedObjects)
      //  {
      //    spawnedObject.transform.SetParent(parentToSpawn.transform);
      //  }
      //}

      //_spawningHistroy.Add(new SnapSpawnMomentum(newSpawnedObjects));
    }

    /// <summary>
    /// Deletes the objects which were to spawned by the call of SnapSpawnCommand
    /// </summary>
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

    /// <summary>
    /// Selects last object which was spawned by the SnapSpawnCommand in the scene.
    /// </summary>
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

  }

}