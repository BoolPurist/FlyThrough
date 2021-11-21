using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary
{
  /// <summary>
  /// Base class to ensure that a component only exits once during runtime in a scene.
  /// </summary>
  /// <typeparam name="TComponent">
  /// TComponent must be the same type as the class which inherits from this base class.
  /// </typeparam>
  public abstract class SingletonComponent<TComponent> : MonoBehaviour where TComponent : MonoBehaviour
  {
    private static TComponent instance;

    /// <summary>
    /// Returns the only existing component as singleton.
    /// If no object exits with the component attached, a new object is created with the component attached.
    /// </summary>
    public static TComponent Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindObjectOfType<TComponent>();
          if (instance == null)
          {            
            GameObject obj = new GameObject();
            obj.name = $"(Singleton) {typeof(TComponent).Name}";
            instance = obj.AddComponent<TComponent>();
          }
        }
        return instance;
      }
    }

    /// <summary>
    /// Makes sure that during runtime that the singleton component which is created more than once will be destroyed
    /// </summary>
    protected virtual void Awake()
    {
      System.Type typeOfThis = this.GetType();
      System.Type typeOfGenericParameter = typeof(TComponent);

      if (typeOfGenericParameter != typeOfThis)
      {
        Debug.LogError(
          $"Type of component [{typeOfThis.Name}] is not the same as the type parameter [{typeOfGenericParameter.Name}] for Singleton given !",
          this.gameObject);
        return;
      }

      if (instance == null)
      {
        instance = this as TComponent;
      }
      else if (instance != this)
      {
        if (!(typeOfThis.IsSubclassOf(typeof(SingletonComponentPersistent<TComponent>))))
        {
          Debug.LogError(
            $"Singleton of type [{typeOfGenericParameter.Name}] was created more than once !" +
            $"Object with duplicate component has the name [{gameObject.name}]"
            );
        }
        Destroy(this);
        
        
      }
    }
  }

}