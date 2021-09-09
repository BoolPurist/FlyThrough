using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlyThrough
{
  public class Pause : MonoBehaviour
  {
    [SerializeField]
    [Min(0)]
    private float CoolDownSecondsPause = 1f;
    [SerializeField]
    private Canvas PauseMenu;

    [SerializeField]
    private PlayerInput InputFromPlayer;

    private DateTime _currentTimeStamp;
    private bool _gameIsPaused = false;

    private void ResetCooldown() 
      => _currentTimeStamp = DateTime.Now.AddSeconds(Convert.ToDouble(CoolDownSecondsPause));
    private bool PauseCoolDownIsOff => _currentTimeStamp <= DateTime.Now;
    private void SkipOneCooldown() => _currentTimeStamp = DateTime.Now;

    // Start is called before the first frame update
    void Start()
    {
      if (InputFromPlayer == null)
      {
        Debug.LogWarning($"{nameof(InputFromPlayer)} as {typeof(PlayerInput).Name} component is missing");
      }
      else if (PauseMenu == null)
      {
        Debug.LogWarning($"{nameof(PauseMenu)} as {typeof(Canvas).Name} component is missing");
      }

      UnPauseGame();
      SkipOneCooldown();
    }

    // Update is called once per frame
    void Update()
    {
      if (InputFromPlayer.PausedPressed && PauseCoolDownIsOff)
      {
        
        if (_gameIsPaused)
        {
          UnPauseGame();
        }
        else
        {
          PauseGame();
        }
        
      }
    }

    public void UnPauseGame()
    {
      _gameIsPaused = false;
      PauseMenu.gameObject.SetActive(false);
      Time.timeScale = 1f;
      ResetCooldown();
    }

    private void PauseGame()
    {
      _gameIsPaused = true;
      PauseMenu.gameObject.SetActive(true);
      Time.timeScale = 0f;
      ResetCooldown();
    }

    public void EndApp() => Application.Quit();
  }
}

