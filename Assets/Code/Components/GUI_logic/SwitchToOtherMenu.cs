using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class SwitchToOtherMenu : MonoBehaviour
  {

    [SerializeField]
    private GameObject[] ObjectesToActivate;

    private GameObject[] _self;

    private void Start() => _self = new GameObject[] { this.gameObject };

    public void ToggleObjects() => ActivateObjectsAndDeactivateOthers.ToggleObjects_Static(ObjectesToActivate, _self);
}
}
