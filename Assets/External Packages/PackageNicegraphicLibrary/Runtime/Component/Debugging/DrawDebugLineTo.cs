using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Component.Debugging
{
  /// <summary>
  /// Draws a thin line between 2 objects even in editor mode.
  /// </summary>
  public class DrawDebugLineTo : MonoBehaviour
  {
    public enum ReferencePoint { Pivot, Center }
#pragma warning disable CS0649
    [SerializeField]
    private GameObject _Target;
#pragma warning restore CS0649
    [SerializeField]
    private Color _GizmoColor = Color.white;
    [SerializeField]
    private ReferencePoint _PointKind = ReferencePoint.Pivot;


    private void OnDrawGizmosSelected()
    {
      if (_Target != null)
      {
        if (_PointKind == ReferencePoint.Pivot)
        {
          Gizmos.color = _GizmoColor;
          Gizmos.DrawLine(transform.position, _Target.transform.position);
        }
        else
        {
          Vector3 ownCenter = Geometry3DUtility.GetBoundingBoxOfAllMeshes(gameObject).center;
          Vector3 targetCenter = Geometry3DUtility.GetBoundingBoxOfAllMeshes(_Target).center;
          Gizmos.color = _GizmoColor;
          Gizmos.DrawLine(ownCenter, targetCenter);
        }

      }
    }
  } 
}