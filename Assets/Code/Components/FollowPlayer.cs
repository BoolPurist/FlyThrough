using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  
  public class FollowPlayer : MonoBehaviour
  {
    [SerializeField]
    private Vector3 OffsetToPlayer;
    [SerializeField]
    private Transform _playerPosition;



    // Update is called once per frame
    void FixedUpdate() => MoveToPlayer();


    [ExecuteInEditMode]
    [ContextMenu("Move to player")]
    public void MoveToPlayer() => this.gameObject.transform.position = _playerPosition.position + OffsetToPlayer;

  }

}