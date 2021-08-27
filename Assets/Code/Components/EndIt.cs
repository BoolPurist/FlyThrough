using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;


namespace FlyThrough
{
  [RequireComponent(typeof(PushPlayerForwardOrBack))]
  [RequireComponent(typeof(PlayerMovement))]
  public class EndIt : MonoBehaviour
  {
    [SerializeField]
    private bool _ignoreDeath = false;
    [SerializeField]
    private float _timeAfterDeath = 2f;
    [SerializeField]
    private UnityEvent OnPlayerDeath;    
    [SerializeField]
    private UnityEvent OnPlayerVictory;

    private PushPlayerForwardOrBack _pusher;
    private PlayerMovement _movement;

    private void Start()
    {
      _pusher = GetComponent<PushPlayerForwardOrBack>();
      _movement = GetComponent<PlayerMovement>();
    }

    public void InitEndGame(bool playerHasWon = false)
    {
      if (playerHasWon)
      {
        OnPlayerVictory?.Invoke();
        _pusher.enabled = false;

        _movement.StopMovement();
        _movement.enabled = false;

        StartCoroutine(ReloadScene());
      }
      else if (!_ignoreDeath)
      {
        _pusher.InitLastMovesToGameOver();
        _movement.enabled = false;
        OnPlayerDeath?.Invoke();

        StartCoroutine(ReloadScene());
      }
    }

    private IEnumerator ReloadScene()
    {
      yield return new WaitForSeconds(_timeAfterDeath);
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}
