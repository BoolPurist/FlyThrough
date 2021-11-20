using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FlyThrough
{
  public class ManagerForPlayerGUID : MonoBehaviour
  {
    [SerializeField]
    private GenerateHallways _hallWaySpawner;

    [SerializeField]
    private TextMeshProUGUI _textForObstaclePassedByPlayer;

    private void OnEnable() => StartListening();
    private void OnDestroy() => StopListening();
    private void OnDisable() => StopListening();
    

    private void StartListening() => _hallWaySpawner.OnPlayerHasPassedObstacle += UpdateObstaclesPassedScore;

    private void StopListening() => _hallWaySpawner.OnPlayerHasPassedObstacle -= UpdateObstaclesPassedScore;   

    private void UpdateObstaclesPassedScore(int newScoreState)
    {
      _textForObstaclePassedByPlayer.text = newScoreState.ToString();
    }
  }
}
  
