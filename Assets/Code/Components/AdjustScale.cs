using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [ExecuteInEditMode]
  public class AdjustScale : MonoBehaviour
  {
    [SerializeField]
    private Transform _objectToAdjust;

    private void Start()
    {
      if (_objectToAdjust == null)
      {
        Debug.LogWarning($"[{_objectToAdjust}] Property was not provided");
      }
    }

    private void Update()
    {
      _objectToAdjust.localScale = transform.localScale;
    }
  }
}
