using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class GenerateHallways : MonoBehaviour
  {
    [SerializeField]
    [Min(2)]
    private int _numberOfObjects = 10;

    [SerializeField]
    private GameObject _hallway;
    [SerializeField]
    private Transform _startHead;
    [SerializeField]
    private GameObject _colliderToNextChunk;

    private Vector3 _startHeadLocation;
    private const int CHUNCK_CHANGE_INDEX = 1;

#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly List<GameObject> _currentHallways = new List<GameObject>();
#pragma warning restore IDE0090 // Use 'new(...)'

    // Start is called before the first frame update
    private void Start()
    {
      _startHeadLocation = _startHead == null ? Vector3.zero : _startHead.position;
      GameObject firstHallway = GameObject.Instantiate<GameObject>(_hallway, transform);
      firstHallway.transform.position = _startHeadLocation;
      _currentHallways.Add(firstHallway);

      for (int i = 1; i < _numberOfObjects; i++)
      {
        AddHallWay(_currentHallways[i - 1]);
      }

      _colliderToNextChunk = GameObject.Instantiate<GameObject>(_colliderToNextChunk, transform);
      _colliderToNextChunk.transform.position = _currentHallways[CHUNCK_CHANGE_INDEX].transform.position;
      _colliderToNextChunk.GetComponent<TriggerNextChunk>().OnNextChunk += UpdateHallways;
    }

    public void AddHallWay(GameObject origin)
    {
      GameObject nextHallway = SnapSpawner.SpawnAdjacantObject(_hallway, origin, SpawnDirection.Front);
      _currentHallways.Add(nextHallway);
      nextHallway.transform.SetParent(transform);
    }

    public void UpdateHallways()
    {
      Destroy(_currentHallways[0]);
      _currentHallways.RemoveAt(0);
      AddHallWay(_currentHallways[_currentHallways.Count - 1]);
      _colliderToNextChunk.transform.position = _currentHallways[CHUNCK_CHANGE_INDEX].transform.position;
    }

    public void ResetHallwaysWorldOrigin()
    {
      _startHead.position = Vector3.zero;
      Transform headHallwayTrans = _currentHallways[0].transform;
      for (int i = 1; i < _currentHallways.Count; i++)
      {
        _currentHallways[i].transform.SetParent(headHallwayTrans);
      }

      _colliderToNextChunk.transform.SetParent(headHallwayTrans);

      headHallwayTrans.position = Vector3.zero;

      foreach (GameObject hallway in _currentHallways)
      {
        hallway.transform.SetParent(transform);
      }

      _colliderToNextChunk.transform.SetParent(transform);
    }
  }

}