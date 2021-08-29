using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class GenerateHallways : MonoBehaviour
  {
    private const int COLLIDER_TRIGGER_FORGOINGBACK_INDEX = 1;
    
    [Header("Required")]
    [SerializeField]  
    [Tooltip("Object, which is spawn for every 2. object")]
    private GameObject _emptyHallway;
    [SerializeField]
    [Tooltip("Collider zone to for the player trigger reset of all objects to origin")]
    private GameObject _colliderTiggerReset;
    [SerializeField]
    [Min(0)]
    [Tooltip("Number of objects which are present at max")]
    private int _numberOfObjects = 10;
    [Header("Optional")]
    [SerializeField]
    private Transform _startLocation;

    private Vector3 _startPosition;

#pragma warning disable IDE0090 // Use 'new(...)'
    // All current spawned objects which make up a linear hallway level.
    private readonly List<GameObject> _currentHallways = new List<GameObject>();
#pragma warning restore IDE0090 // Use 'new(...)'

    private void ValidateObjectBlueprintsToSpawn()
    {     
      if (_emptyHallway == null || _emptyHallway.GetComponent<Renderer>() == null)
      {
        ErrorMessage(nameof(_emptyHallway), typeof(Renderer).Name);
      }      
      else if (_colliderTiggerReset == null || _colliderTiggerReset.GetComponent<Collider>() == null)
      {
        ErrorMessage(nameof(_colliderTiggerReset), typeof(Collider).Name);
      }

      static void ErrorMessage(string nameOfParameter, string nameOfNeededComponent)
        => Debug.LogError($"{nameOfParameter} must be provided with a component derived from {nameOfNeededComponent}");
    }

    // Start is called before the first frame update
    private void Start()
    {
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

    private void AddHallWay(GameObject origin)
    {
      GameObject nextHallway = GameObject.Instantiate<GameObject>(_emptyHallway, transform);
      SnapMover.MoveSnap(origin, nextHallway, PlaceSnapDirection.Front);
      
      _currentHallways.Add(nextHallway);
      nextHallway.transform.SetParent(transform);
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