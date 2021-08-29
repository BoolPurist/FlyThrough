using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestParentTransform : MonoBehaviour
{
  private void Start()
  {
    transform.SetParent(null);
  }
}
