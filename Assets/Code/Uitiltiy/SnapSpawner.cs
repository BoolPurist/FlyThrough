﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public enum SpawnDirection { Front, Back, Left, Right, Up, Down }

  public static class SnapSpawner
  {
    public static GameObject[] SpawnAdjacantObject(GameObject prefabToSpawnm, GameObject origin, int numberOfSpawns, SpawnDirection spawnDirectionFromOrigin = SpawnDirection.Front)
      => SpawnAdjacantObject(prefabToSpawnm, origin, 0f, numberOfSpawns, spawnDirectionFromOrigin);

    public static GameObject SpawnAdjacantObject(GameObject prefabToSpawnm, GameObject origin, SpawnDirection spawnDirectionFromOrigin = SpawnDirection.Front)
      => SpawnAdjacantObject(prefabToSpawnm, origin, 1, spawnDirectionFromOrigin)[0];


    public static GameObject[] SpawnAdjacantObject(GameObject prefabToSpawnm, GameObject origin, float directionOffset, int numberOfSpawns, SpawnDirection spawnDirectionFromOrigin = SpawnDirection.Front)
    {
      var allSpawnedObjects = new GameObject[numberOfSpawns];
      GameObject lastObjectSpawnedObject = SpawnOneObject(origin);
      allSpawnedObjects[0] = lastObjectSpawnedObject;

      for (int i = 1; i < numberOfSpawns; i++)
      {
        lastObjectSpawnedObject = SpawnOneObject(lastObjectSpawnedObject);
        allSpawnedObjects[i] = lastObjectSpawnedObject;
      }

      return allSpawnedObjects;

      GameObject SpawnOneObject(GameObject nextOrigin)
      {
        float distanceToCenterOfSpawn = 0f;

        var originRenderer = origin.GetComponent<MeshRenderer>();
        Vector3 originExtends = originRenderer != null ? originRenderer.bounds.extents : Vector3.zero;

        var spawnRenderer = prefabToSpawnm.GetComponent<MeshRenderer>();
        Vector3 spawnExtends = spawnRenderer != null ? spawnRenderer.bounds.extents : Vector3.zero;

        Vector3 direction = Vector3.zero;
        Vector3 offset = Vector3.zero;

        switch (spawnDirectionFromOrigin)
        {
          case SpawnDirection.Front:
            distanceToCenterOfSpawn = originExtends.z + spawnExtends.z;
            direction = Vector3.forward;
            offset = new Vector3(0f, 0f, directionOffset);
            break;
          case SpawnDirection.Back:
            distanceToCenterOfSpawn = originExtends.z + spawnExtends.z;
            direction = Vector3.back;
            offset = new Vector3(0f, 0f, directionOffset);
            break;
          case SpawnDirection.Right:
            distanceToCenterOfSpawn = originExtends.x + spawnExtends.x;
            direction = Vector3.right;
            offset = new Vector3(directionOffset, 0f, 0f);
            break;
          case SpawnDirection.Left:
            distanceToCenterOfSpawn = originExtends.x + spawnExtends.x;
            direction = Vector3.left;
            offset = new Vector3(directionOffset, 0f, 0f);
            break;
          case SpawnDirection.Up:
            distanceToCenterOfSpawn = originExtends.z + spawnExtends.z;
            direction = Vector3.up;
            offset = new Vector3(0f, directionOffset, 0f);
            break;
          case SpawnDirection.Down:
            distanceToCenterOfSpawn = originExtends.z + spawnExtends.z;
            direction = Vector3.down;
            offset = new Vector3(0f, directionOffset, 0f);
            break;

        }



        Transform newTranform = GameObject.Instantiate(
            prefabToSpawnm,
            nextOrigin.transform.position,
            nextOrigin.transform.rotation
          ).transform;



        newTranform.position += (direction * distanceToCenterOfSpawn) + offset;

        return newTranform.gameObject;
      }
    }

  }

}