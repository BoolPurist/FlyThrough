using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace FlyThrough
{
  public class WaitForAnyKeyStroke : MonoBehaviour
  {
    
    public UnityEvent OnKeyStroke;

    private void OnGUI()
    {
      if (Event.current.isKey)
      {
        OnKeyStroke.Invoke();
      }
    }
  }
}
