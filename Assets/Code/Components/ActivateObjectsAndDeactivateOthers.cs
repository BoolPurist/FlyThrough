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

    public static void ToggleObjects_Static(IList<GameObject> objectesToActivate, IList<GameObject> objectsToDeactivate)
    {
      ToggleFromBool(objectesToActivate, true);
      ToggleFromBool(objectsToDeactivate, false);

      void ToggleFromBool(IList<GameObject> objectsToToggle, bool toggleValue)
      {
        foreach (GameObject objectToToggle in objectsToToggle)
        {
          objectToToggle.SetActive(toggleValue);
        }
      }
    }

    public void ToggleObjects() => ToggleObjects_Static(ObjectesToActivate, ObjectesToDeactivate);
  }
}
