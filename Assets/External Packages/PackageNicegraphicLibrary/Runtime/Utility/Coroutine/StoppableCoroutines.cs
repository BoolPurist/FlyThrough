using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility.Coroutines
{
  /// <summary>
  /// Allows to stop all coroutines, including sub coroutines, started by a function which returns this instance.
  /// Can not be constructed but is returned by certain functions for example in the static class <see cref="CoroutineUtility"/>
  /// </summary>
  public class StoppableCoroutines
  {
    private readonly MonoBehaviour _coroutineController;
    private readonly List<Coroutine> _startedCoroutines = new List<Coroutine>();

    internal StoppableCoroutines(MonoBehaviour component)
    {
      if (component != null)
      {
        _coroutineController = component;

      }
      else
      {
        throw new ArgumentNullException($"{nameof(component)} must not be null.");
      }

    }

    internal StoppableCoroutines AddCreatedCoroutine(Coroutine createdCoroutine)
    {
      _startedCoroutines.Add(createdCoroutine);
      return this;
    }

    internal void RemoveDoneCoroutine(Coroutine coroutineToRemove)
    {
      if (coroutineToRemove != null)
      {
        _startedCoroutines.Remove(coroutineToRemove);
      }
    }

    internal void CleanUpCoroutines() => _startedCoroutines.RemoveAll(element => element == null);

    /// <summary>
    /// Stops all coroutines including the sub coroutines
    /// </summary>
    public void StopAllCoroutines()
    {
      CleanUpCoroutines();
      if (_coroutineController != null)
      {
        foreach (Coroutine coroutineToStop in _startedCoroutines)
        {
          if (coroutineToStop != null)
          {
            _coroutineController.StopCoroutine(coroutineToStop);
          }          
        }
      }
    }
  } 
}