using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Base class to ensure that a component only exits once during runtime in a scene. 
  /// The component is not destroyed if a scene is switched.
  /// </summary>
  /// <typeparam name="TComponent">
  /// TComponent must be the same type as the class which inherits from this base class.
  /// </typeparam>
  public abstract class SingletonComponentPersistent<TComponent> : SingletonComponent<TComponent> where TComponent : MonoBehaviour
  {
    protected virtual void Start()
    {
      DontDestroyOnLoad(Instance.gameObject);
    }
  }
}