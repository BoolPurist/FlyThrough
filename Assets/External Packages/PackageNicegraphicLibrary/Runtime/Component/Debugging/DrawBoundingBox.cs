using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Component.Debugging
{
  /// <summary>
  /// Draws a bounding box which contains all meshes under one game object
  /// while selected even in editor mode.
  /// </summary>
  public class DrawBoundingBox : MonoBehaviour
  {
    /// <summary>
    /// Used to group all colors adjustable via inspector for the component <see cref="DrawBoundingBox"/>
    /// </summary>s
    [System.Serializable]
    public class ElementColors
    {
      [Tooltip("Color of the drawn center.")]
      public Color Center = Color.red;
      [Tooltip("Color of the point equaling center - extends.")]
      public Color Min = Color.blue;
      [Tooltip("Color of the point equaling center + extends.")]
      public Color Max = Color.green;
      [Tooltip("Color of the rim.")]
      public Color WireFrame = Color.grey;
      [Tooltip("Color of the diagonal between min and max.")]
      public Color Diagonal = Color.cyan;
    }

#pragma warning disable IDE0044 // Add readonly modifier
    [SerializeField]
    [Min(0)]
    [Tooltip("Radius of a sphere which represents a point")]
    private float RadiusSpherePoint = 0.1f;
    [SerializeField]
    private ElementColors Colors;
#pragma warning restore IDE0044 // Add readonly modifier

#pragma warning disable IDE0051 // Remove unused private members
    private void OnDrawGizmosSelected()

    {      
      Bounds boundingBox = Geometry3DUtility.GetBoundingBoxOfAllMeshes(gameObject);
      boundingBox.Encapsulate(this.transform.TransformPoint(Vector3.zero));
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(boundingBox.center, RadiusSpherePoint);
      Gizmos.color = Colors.Min;
      Gizmos.DrawSphere(boundingBox.min, RadiusSpherePoint);
      Gizmos.color = Colors.Max;
      Gizmos.DrawSphere(boundingBox.max, RadiusSpherePoint);
      Gizmos.color = Colors.WireFrame;
      Gizmos.DrawWireCube(boundingBox.center, boundingBox.size);
      Gizmos.color = Colors.Diagonal;
      Gizmos.DrawLine(boundingBox.min, boundingBox.max);
     
    }
#pragma warning restore IDE0051 // Remove unused private members

  }

}
