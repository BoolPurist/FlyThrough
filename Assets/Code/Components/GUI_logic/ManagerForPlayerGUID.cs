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

    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _victoryPanel;

    private void OnEnable() => StartListening();
    private void OnDestroy() => StopListening();
    private void OnDisable() => StopListening();
    

    private void StartListening() => _hallWaySpawner.OnPlayerHasPassedObstacle += UpdateObstaclesPassedScore;

    private void StopListening() => _hallWaySpawner.OnPlayerHasPassedObstacle -= UpdateObstaclesPassedScore;   

    private void UpdateObstaclesPassedScore(int newScoreState)
    {
      _textForObstaclePassedByPlayer.text = newScoreState.ToString();
    }

    public void ReactToGameOver() => _gameOverPanel.SetActive(true);

    public void ReactToVictory() => _victoryPanel.SetActive(true);
  }
}
  
