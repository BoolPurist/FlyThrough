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


    private DateTime _currentTimeStamp;
    private bool _gameIsPaused = false;

    private SGameInputKeyboard _input;

    private void ResetCooldown() 
      => _currentTimeStamp = DateTime.Now.AddSeconds(Convert.ToDouble(CoolDownSecondsPause));
    private bool PauseCoolDownIsOff => _currentTimeStamp <= DateTime.Now;
    private void SkipOneCooldown() => _currentTimeStamp = DateTime.Now;


    #region component callbacks
    private void Start()
    {
      if (PauseMenu == null)
      {
        Debug.LogWarning($"{nameof(PauseMenu)} as {typeof(Canvas).Name} component is missing");
      }

      UnPauseGame();
      SkipOneCooldown();
    }

    private void OnEnable()
      => StartListeningForInput();

    private void OnDisable()
      => StopListeningForInputs();
    private void OnDestroy()
      => StopListeningForInputs();
    #endregion

    #region subscriber logic
    private void StartListeningForInput()
    {
      _input = FindObjectOfType<SGameInputKeyboard>();
      if (_input == null)
      {
        Debug.LogWarning($"No component found {typeof(SGameInputKeyboard).Name} for listening to player inputs.");
      }
      else
      {
        _input.OnPauseInput += OnPausePressed;
      }
    }

    private void StopListeningForInputs()
    {
      if (_input != null)
      {
        _input.OnPauseInput -= OnPausePressed;
      }
    }
    #endregion

    #region input handling
    public void OnPausePressed()
    {
      if (PauseCoolDownIsOff)
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

    #endregion


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

