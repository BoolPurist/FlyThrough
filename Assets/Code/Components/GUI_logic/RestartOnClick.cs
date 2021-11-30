using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Component.GUI;
using UnityEngine.SceneManagement;

namespace FlyThrough
{
  public class RestartOnClick : ObserverForButtonClick
  {
    protected override void OnButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
  }
}
