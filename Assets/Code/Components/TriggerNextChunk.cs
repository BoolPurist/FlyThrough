using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [RequireComponent(typeof(BoxCollider))]
  public class TriggerNextChunk : MonoBehaviour
  {
    public event Action OnNextChunk;
    private void OnTriggerEnter(Collider other) => OnNextChunk?.Invoke();
  }
}