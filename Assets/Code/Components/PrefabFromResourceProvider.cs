using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class PrefabFromResourceProvider : MonoBehaviour
  {

    [SerializeField]
    private ResourceLoadParams _loadParameters;


#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly System.Random _randomGenerator = new System.Random();
#pragma warning restore IDE0090 // Use 'new(...)'

    private GameObject[] _allPrefabs;


    public int CountOfLoadedPrefabs => _allPrefabs.Length;

    private void Awake()
    {
      _allPrefabs = Resources.LoadAll<GameObject>(_loadParameters.PathToResources);
      if (_allPrefabs == null)
      {
        Debug.LogWarning(
            "Prefabs for obstacle walls could not be loaded from the resource folder" +
            $" with the path {_loadParameters.PathToResources}."
          );
      }
    }

    public GameObject GetPrefabAt(int index)
    {
      if (CountOfLoadedPrefabs >= index || index < 0)
      {
        Debug.LogError(
            $"{nameof(index)} as parameter for {nameof(GetPrefabAt)} must be between 0 and number of loaded prefabs - 1\n." +
            $"Count of loaded prefabs {CountOfLoadedPrefabs}. Index was {index}"
          );
      }

      return _allPrefabs[index];
    }

    public GameObject GetRandomPrefab() => _allPrefabs[_randomGenerator.Next(0, CountOfLoadedPrefabs)];
    
    

  }
}
