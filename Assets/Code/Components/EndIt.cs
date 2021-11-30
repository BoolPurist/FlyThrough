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
    private UnityEvent OnPlayerDeath;    
    [SerializeField]
    private UnityEvent OnPlayerVictory;

    private PushPlayerForwardOrBack _pusher;
    private PlayerMovement _movement;

    public void SetIgnoreDeath(bool ignoresDeath) => _ignoreDeath = ignoresDeath;

    private void Start()
    {
      _pusher = GetComponent<PushPlayerForwardOrBack>();
      _movement = GetComponent<PlayerMovement>();
    }

    public void InitEndGame(bool playerHasWon = false)
    {
      if (playerHasWon)
      {
        StopMoving();
        OnPlayerVictory?.Invoke();
      }
      else if (!_ignoreDeath)
      {
        StopMoving();
        OnPlayerDeath?.Invoke();        
      }
    }

    private void StopMoving()
    {
      _movement.enabled = false;
      _pusher.enabled = false;
      _movement.StopMovement();
    }

  }
}
