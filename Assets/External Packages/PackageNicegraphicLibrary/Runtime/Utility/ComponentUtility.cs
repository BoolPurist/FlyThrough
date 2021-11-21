using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility
{
  public static class ComponentUtility
  {
    /// <summary>
    /// Checks if a specific component exits on the given game object
    /// </summary>
    /// <typeparam name="TComponent">
    /// The component to check for.
    /// </typeparam>
    /// <param name="gameObject">
    /// Game object to make the check on.
    /// </param>
    /// <returns>
    /// Returns true if the component exits at least once.
    /// Returns false if the game object itself is null or the given component is not attached to it.
    /// </returns>
    public static bool HasComponentOn<TComponent>(GameObject gameObject)
      => gameObject != null && gameObject.GetComponent<TComponent>() != null;

    /// <summary>
    /// Makes sure that the game object will have the specific component at least once.
    /// </summary>
    /// <typeparam name="TComponent">
    /// Which component should be attached at least once.
    /// </typeparam>
    /// <param name="gameObject">
    /// The object on which the component is attached if the component does not already exits on this object.
    /// </param>
    /// <returns>
    /// Returns the already attached component if it was attached already or the new attached component
    /// Returns null if the parameter [gameObject] is null itself. 
    /// </returns>
    public static TComponent EnsureComponentOn<TComponent>(GameObject gameObject) where TComponent : MonoBehaviour
    {
      if (gameObject == null)
      {
        return null;
      }
      else
      {
        var component = gameObject.GetComponent<TComponent>();

        if (component == null)
        {
          component = gameObject.AddComponent<TComponent>();
        }

        return component;        
      }
    }
    
  } 
}