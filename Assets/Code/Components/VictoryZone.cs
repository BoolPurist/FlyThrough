using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlyThrough
{
  [RequireComponent(typeof(Collider))]
  public class VictoryZone : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      GameObject player = other.gameObject;
      if (player.CompareTag("Player"))
      {
        player.GetComponent<EndIt>().InitEndGame(true);
      }
    }
  }
}
