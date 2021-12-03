using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FlyThrough
{
  public class MainMenuCallbacks : MonoBehaviour
  {
    
    public void EndGame() => Application.Quit();

    public void StartGame() => SceneManager.LoadScene(SceneNames.START_LEVEL);

    public void GoToCredits() => SceneManager.LoadScene(SceneNames.CREDIT_LEVEL);
  }
}
