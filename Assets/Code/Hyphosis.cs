using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Hyphosis : MonoBehaviour
{
  public GameObject _param;

  [ContextMenu("Move")]
  private void SnapMove()
  {
    Renderer _paramRenderer = _param.GetComponent<Renderer>();
    Renderer _ownRenderer = GetComponent<Renderer>();

    Vector3 _paramRenderCenter = _paramRenderer.bounds.center;
    Vector3 _ownRenderCenter = _ownRenderer.bounds.center;

    Vector3 _paramRenderExtends = _paramRenderer.bounds.extents;
    Vector3 _ownRenderExtends = _ownRenderer.bounds.extents;

    var _helpObj = new GameObject("Help object");
    _helpObj.transform.position = _ownRenderCenter;
    Transform _previousTransform = transform.parent;
    transform.SetParent(_helpObj.transform);
    // Debug.Log($"Distance {Vector3.Distance(_helpObj.transform.position, _paramRenderCenter)}");
    _helpObj.transform.position = _paramRenderCenter;
    _helpObj.transform.position += new Vector3(0f, 0f, _paramRenderExtends.z + _ownRenderExtends.z);
    transform.SetParent(null);

    if (!Application.isPlaying)
    {
      DestroyImmediate(_helpObj);
    }
    else
    {
      Destroy(_helpObj);
    }
    transform.SetParent(_previousTransform);
  }


}
