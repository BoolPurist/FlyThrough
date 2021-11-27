using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{  
  public class KillPlayer : MonoBehaviour
  {
    private void OnCollisionEnter(Collision collision)
    {
      GameObject player = collision.gameObject;
      bool collidedWithPlayer = player.CompareTag("Player");
      
      if (collidedWithPlayer)
      {
        player.GetComponent<EndIt>().InitEndGame();
      }
    }    
  }
}
