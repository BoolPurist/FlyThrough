using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FlyThrough
{
  public class MainMenuCallbacks : MonoBehaviour
  {
    [SerializeField]
    private string NameOfStartLevel = "EndlessLevel";

    public void EndGame() => Application.Quit();

    public void StartGame() => SceneManager.LoadScene(NameOfStartLevel);
  }
}
