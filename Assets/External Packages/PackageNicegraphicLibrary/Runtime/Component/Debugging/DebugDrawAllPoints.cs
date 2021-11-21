using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary.Component.Debugging
{
  /// <summary>
  /// Component draw a 2d graph in 3d space based on (x, y) tuples
  /// </summary>
  public class DebugDrawAllPoints : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("x and y values for drawing the graph")]
    private Vector2[] _Values;
    [SerializeField]
    [Tooltip("World position to draw 2d graph")]
    private Vector3 _Start = Vector3.zero;
    [SerializeField]
    [Tooltip("Color of the graph")]
    private Color _LineColor = Color.white;

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = _LineColor;
      

      if (_Values != null && _Values.Length != 0)
      {
        Vector3 previousVector = new Vector3(_Values[0].x + _Start.x, _Values[0].y + _Start.y, _Start.z);

        foreach (Vector2 xAndY in _Values)
        {
          Vector3 nextVector = new Vector3(xAndY.x + _Start.x, xAndY.y + _Start.y, _Start.z);
          Gizmos.DrawLine(previousVector, nextVector);
          previousVector = nextVector;
        }
      }
    }

    /// <summary>
    /// Causes component to draw the graph from the new provided points
    /// </summary>
    /// <param name="points">
    /// Array of points for drawing the new graph.
    /// </param>
    public void Replace2DPoints(Vector2[] points)
    {
      if (points != null)
      {
        _Values = points;
      }
    }

  } 
}