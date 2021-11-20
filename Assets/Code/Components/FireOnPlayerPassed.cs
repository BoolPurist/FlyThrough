using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  public class FireOnPlayerPassed : MonoBehaviour
  {

    public event Action OnPlayerHasPassed;

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        OnPlayerHasPassed?.Invoke();
      }
    }
  }
}

