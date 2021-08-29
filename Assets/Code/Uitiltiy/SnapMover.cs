using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  /// <summary>
  /// From which side one object is attached to the other
  /// </summary>
  public enum PlaceSnapDirection {
    /// <summary> Attached from z+ </summary>    
    Front,
    /// <summary> Attached from z- </summary>
    Back,
    /// <summary> Attached from x+ </summary>
    Left,
    /// <summary> Attached from x- </summary>
    Right,
    /// <summary> Attached from y+ </summary>
    Up,
    /// <summary> Attached from y- </summary>
    Down
  }

  /// <summary>
  /// API for attaching one object, snapped, to other object
  /// </summary>
  public static class SnapMover
  {
    /// <summary>
    /// Places an object to be placed snapped to another object called origin.
    /// Snapped means that the placed object is aligned 
    /// with its center to center of the origin object
    /// </summary>
    /// <param name="origin">
    /// Object which the other object is aligned to
    /// </param>
    /// <param name="objectToMove">
    /// Object which will be aligned
    /// </param>
    /// <param name="moveDirection">
    /// From side which the [objectToMove] will attached to the origin
    /// </param>
    /// <param name="offset">
    /// Offset to move the placed [objectToMove] will moved after the snap placement.
    /// </param>
    public static void MoveSnap(
        GameObject origin, 
        GameObject objectToMove, 
        PlaceSnapDirection moveDirection,
        Vector3 offset
      )
    {
      Renderer originRenderer = origin.GetComponent<Renderer>();      
      Renderer toMoveRenderer = objectToMove.GetComponent<Renderer>();

      Vector3 originCenter = originRenderer.bounds.center;
      Vector3 toMoveCenter = toMoveRenderer.bounds.center;

      PlaceObjSnappedToOrigin();

      void PlaceObjSnappedToOrigin()
      {
        // Pivot point is not always the same as the center of rendered mesh.
        // the pivotObj is used to ensure that the object to be placed is 
        // moved by the center of its rendered mesh.
        var pivotObj = new GameObject("Snap container");
        pivotObj.transform.position = toMoveCenter;
        Transform previousParent = objectToMove.transform.parent;
        objectToMove.transform.SetParent(pivotObj.transform);

        pivotObj.transform.position = originCenter;
        pivotObj.transform.position += GetDistanceDirectionFromOrigin() + offset;

        DisposePivotObj();

        // The pivot object served its purpose. Time to get rid of it.
        void DisposePivotObj()
        {

          objectToMove.transform.SetParent(previousParent);
          if (!Application.isPlaying)
          {
            Object.DestroyImmediate(pivotObj);
          }
          else
          {
            Object.Destroy(pivotObj);
          }
        }

      }

      // Gets one vector with only one coordinate component which is set.
      // The set component with its sign and value is the direction from the origin to the object to be snap placed     
      Vector3 GetDistanceDirectionFromOrigin()
      {
        Vector3 direction = Vector3.zero;
        float distanceToCenterOfSpawn = 0f;

        Vector3 originExtend = originRenderer.bounds.extents;
        Vector3 toMoveExtend = toMoveRenderer.bounds.extents;

        switch (moveDirection)
        {
          case PlaceSnapDirection.Front:
            distanceToCenterOfSpawn = originExtend.z + toMoveExtend.z;
            direction = Vector3.forward;            
            break;
          case PlaceSnapDirection.Back:
            distanceToCenterOfSpawn = originExtend.z + toMoveExtend.z;
            direction = Vector3.back;            
            break;
          case PlaceSnapDirection.Right:
            distanceToCenterOfSpawn = originExtend.x + toMoveExtend.x;
            direction = Vector3.right;            
            break;
          case PlaceSnapDirection.Left:
            distanceToCenterOfSpawn = originExtend.x + toMoveExtend.x;
            direction = Vector3.left;            
            break;
          case PlaceSnapDirection.Up:
            distanceToCenterOfSpawn = originExtend.z + toMoveExtend.z;
            direction = Vector3.up;            
            break;
          case PlaceSnapDirection.Down:
            distanceToCenterOfSpawn = originExtend.z + toMoveExtend.z;
            direction = Vector3.down;            
            break;
        }

        return direction * distanceToCenterOfSpawn;
      }
      
    }

    public static void MoveSnap(
          GameObject origin,
          GameObject objectToMove,
          PlaceSnapDirection moveDirection
        ) => MoveSnap(origin, objectToMove, moveDirection, Vector3.zero);
  }
}