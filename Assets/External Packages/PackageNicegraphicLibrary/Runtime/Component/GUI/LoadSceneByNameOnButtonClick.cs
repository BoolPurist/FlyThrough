using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Loads scene by a given name if the unity button on the same object is clicked.
  /// </summary>
  public class LoadSceneByNameOnButtonClick : ObserverForButtonClick
  {
    [SerializeField]
    [Tooltip("Name of the scene to load in the build.")]
    private string NameOfScene = "";

    [SerializeField]
    [Tooltip("In which way a scene is loaded.")]
    private LoadSceneMode LoadMode = LoadSceneMode.Single;

    /// <summary>
    /// Loads a scene with name given by the value of inspector field NameOfScene
    /// </summary>
    protected override void OnButtonClicked() => SceneManager.LoadScene(NameOfScene, LoadMode);
  }
}