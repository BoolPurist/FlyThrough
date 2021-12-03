using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlyThrough
{
  public class CallbacksInCredits : MonoBehaviour
  {
    public void ChangeBackToMainMenu() => SceneManager.LoadScene(SceneNames.START_MENU_LEVEL);
  }
}
