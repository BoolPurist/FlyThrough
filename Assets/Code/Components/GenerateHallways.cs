using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [RequireComponent(typeof(PrefabFromResourceProvider))]
  public class GenerateHallways : MonoBehaviour
  {
    private const int COLLIDER_TRIGGER_FORGOINGBACK_INDEX = 3;
    
    [Header("Required")]
    [SerializeField]  
    [Tooltip("Object, which is spawn for every 2. object")]
    private GameObject _emptyHallway;
    [SerializeField]
    private GameObject _obstacle;
    [SerializeField]
    [Tooltip("Collider zone to for the player trigger reset of all objects to origin")]
    private GameObject _colliderTiggerReset;
    [Header("Optional")]
    [SerializeField]
    private Transform _startLocation;
    [Min(3)]
    [SerializeField]
    [Tooltip("Number of objects which are present at max")]
    private int _numberOfObjects = 10;
    [SerializeField]
    [Tooltip("Number of empty hallwasys between a obstacle")]
    [Min(1)]
    private int _noObstaclePattern = 1;

    private Vector3 _startPosition;
    private bool _spawnObstacle = true;    
    private int _emptySpawnCounter = 0;

    private PrefabFromResourceProvider _prefabProvider;


#pragma warning disable IDE0090 // Use 'new(...)'
    // All current spawned objects which make up a linear hallway level.
    private readonly List<GameObject> _currentHallways = new List<GameObject>();
#pragma warning restore IDE0090 // Use 'new(...)'

    private void ValidateObjectBlueprintsToSpawn()
    {     
      if (_emptyHallway == null || _emptyHallway.GetComponent<Renderer>() == null)
      {
        ErrorMessageOfMissingObjOrMissingComponent(nameof(_emptyHallway), typeof(Renderer).Name);
      }      
      else if (_colliderTiggerReset == null || _colliderTiggerReset.GetComponent<Collider>() == null)
      {
        ErrorMessageOfMissingObjOrMissingComponent(nameof(_colliderTiggerReset), typeof(Collider).Name);
      }      
    }

    private static void ErrorMessageOfMissingObjOrMissingComponent(string nameOfParameter, string nameOfNeededComponent)
        => Debug.LogError($"{nameOfParameter} must be provided with a component derived from {nameOfNeededComponent}");

    // Start is called before the first frame update
    private void Start()
    {
      _prefabProvider = GetComponent<PrefabFromResourceProvider>();
      
      ValidateObjectBlueprintsToSpawn();
      SpawnAndPlaceStartObj();
      SpawnFromSecondToLastPresentObj();
      AddColliderTriggerForGoingBack();

      void SpawnAndPlaceStartObj()
      {
        _startPosition = _startLocation == null ? Vector3.zero : _startLocation.position;
        GameObject firstHallway = GameObject.Instantiate<GameObject>(_emptyHallway, transform);
        firstHallway.transform.position = _startPosition;

        _currentHallways.Add(firstHallway);
      }

      void AddColliderTriggerForGoingBack()
      {
        _colliderTiggerReset = GameObject.Instantiate<GameObject>(_colliderTiggerReset, transform);
        _colliderTiggerReset.transform.position =
          _currentHallways[COLLIDER_TRIGGER_FORGOINGBACK_INDEX].transform.position;
        _colliderTiggerReset.GetComponent<TriggerNextChunk>().OnNextChunk += DestroyHeadAndSpawnNewTail;
      }

      void SpawnFromSecondToLastPresentObj()
      {
        for (int i = 1; i < _numberOfObjects; i++)
        {
          AddHallWay(_currentHallways[i - 1]);
        }
      }

    }
    
    private GameObject NextBluePrintObject => _spawnObstacle ? _prefabProvider.GetRandomPrefab() : _emptyHallway;

    private void AddHallWay(GameObject origin)
    {

      DecideBetweenObstacleAndEmptyHallway();

      // Spawn next object and place in respective position.
      GameObject nextHallway = GameObject.Instantiate<GameObject>(NextBluePrintObject);
      SnapMover.MoveSnap(origin, nextHallway, PlaceSnapDirection.Front);

      // Rotate the wall of the obstacle. Rotation changes the position of holes.
      // This way one can  get out more content of the assets as obstacles.
      if (_spawnObstacle)
      {        
        nextHallway.GetComponentInChildren<RotateBy90Degree>().RotateRandomSteps();        
      }
      
      _currentHallways.Add(nextHallway);      
      nextHallway.transform.SetParent(transform);
      
      void DecideBetweenObstacleAndEmptyHallway()
      {
        if (_spawnObstacle)
        {
          _spawnObstacle = !_spawnObstacle;
        }
        else
        {
          bool switchToSpawnCategory = (_emptySpawnCounter / _noObstaclePattern) == 1;
          _spawnObstacle = switchToSpawnCategory ? !_spawnObstacle : _spawnObstacle;
          _emptySpawnCounter = switchToSpawnCategory ? 0 : _emptySpawnCounter;
          _emptySpawnCounter++;
        }
      }
      
    }

    public void DestroyHeadAndSpawnNewTail()
    {
      Destroy(_currentHallways[0]);
      _currentHallways.RemoveAt(0);
      AddHallWay(_currentHallways[_currentHallways.Count - 1]);
      _colliderTiggerReset.transform.position = _currentHallways[COLLIDER_TRIGGER_FORGOINGBACK_INDEX].transform.position;
    }

    public void ResetObjectsBackToStartLocation()
    {
      _startLocation.position = Vector3.zero;
      Transform headHallwayTrans = _currentHallways[0].transform;
      for (int i = 1; i < _currentHallways.Count; i++)
      {
        _currentHallways[i].transform.SetParent(headHallwayTrans);
      }

      _colliderTiggerReset.transform.SetParent(headHallwayTrans);

      headHallwayTrans.position = Vector3.zero;

      foreach (GameObject hallway in _currentHallways)
      {
        hallway.transform.SetParent(transform);
      }

      _colliderTiggerReset.transform.SetParent(transform);
    }
  }

}