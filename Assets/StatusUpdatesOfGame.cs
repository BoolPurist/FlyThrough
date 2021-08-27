using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlyThrough
{
  public class StatusUpdatesOfGame : MonoBehaviour
  {
    [SerializeField]
    private Text _gameOverTextField;
    [SerializeField]
    private GameObject _victoryPanel;

    private void Start()
    {

      if (_gameOverTextField == null)
      {
        Debug.LogWarning($"Parameter object {nameof(_gameOverTextField)}  provided for showing game over.");
      }
      else if (_victoryPanel == null)
      {
        Debug.LogWarning($"Parameter object  {nameof(_victoryPanel)} is not provided for showing victory.");
      }
      
    }

    public void ReactToGameOver() => _gameOverTextField.enabled = true;

    public void ReactToVictory() => _victoryPanel.SetActive(true);
  }

  
}
