using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class ActivateObjectsAndDeactivateOthers : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] ObjectesToActivate;
    [SerializeField]
    private GameObject[] ObjectesToDeactivate;

    public void ToggleObjects()
    {
      ToggleFromBool(ObjectesToActivate, true);
      ToggleFromBool(ObjectesToDeactivate, false);

      void ToggleFromBool(GameObject[] objectsToToggle,bool toggleValue)
      {
        foreach (GameObject objectToToggle in objectsToToggle)
        {
          objectToToggle.SetActive(toggleValue);
        }
      }
    }
  }
}
