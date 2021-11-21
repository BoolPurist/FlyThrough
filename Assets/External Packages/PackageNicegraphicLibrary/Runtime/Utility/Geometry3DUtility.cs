using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Provides functions for handling math related task in the 3d geometry area
  /// </summary>
  public static class Geometry3DUtility
  {
    /// <summary>
    /// Gets the center, size etc from a whole mesh of an object. Whole mesh can made up of several meshes
    /// residing in the object itself and its children
    /// </summary>
    /// <param name="rootObject">
    /// Object to get the bounding box from
    /// </param>
    /// <return>
    /// Returns the bounding box of the all meshes under the object.
    /// Returns a bounding box with a vector (0,0,0) for center, min, max, extends and size  
    /// if the object itself and none of its children has a MeshRenderer Component or is null
    /// </return>
    /// <remarks>
    /// To work, object with a mesh needs a component MeshRenderer. 
    /// </remarks>
    public static Bounds GetBoundingBoxOfAllMeshes(GameObject rootObject)
    {      
      if (rootObject == null)
      {
        Debug.LogWarning($" {nameof(rootObject)} is null. Returned bounds with no volume and with a center of {Vector3.zero}");
        return new Bounds(Vector3.zero, Vector3.zero);
      }

      var resultBoundingBox = new Bounds();

      float minX = float.MaxValue;
      float maxX = float.MinValue;
      float minY = float.MaxValue;
      float maxY = float.MinValue;
      float minZ = float.MaxValue;
      float maxZ = float.MinValue;

      bool hasMeshRenderer = false;

      CompareOneMesh(rootObject);

      if (hasMeshRenderer)
      {
        resultBoundingBox.max = new Vector3(maxX, maxY, maxZ);
        resultBoundingBox.min = new Vector3(minX, minY, minZ);
      }
      else
      {
        resultBoundingBox.center = rootObject.transform.position;
      }

      return resultBoundingBox;

      void CompareOneMesh(GameObject oneObject)
      {
        var meshRenderer = oneObject.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
          hasMeshRenderer = true;
          Bounds boundingBox = meshRenderer.bounds;
          minX = Mathf.Min(minX, boundingBox.min.x);
          maxX = Mathf.Max(maxX, boundingBox.max.x);
          minY = Mathf.Min(minY, boundingBox.min.y);
          maxY = Mathf.Max(maxY, boundingBox.max.y);
          minZ = Mathf.Min(minZ, boundingBox.min.z);
          maxZ = Mathf.Max(maxZ, boundingBox.max.z);
        }

        for (int i = 0; i < oneObject.transform.childCount; i++)
        {
          CompareOneMesh(oneObject.transform.GetChild(i).gameObject);
        }
      }
    }

    /// <summary>
    /// Finds out if a point is on the line which can be drawn by the a given direction
    /// </summary>
    /// <param name="point">Point to be check for being on the line</param>
    /// <param name="direction">Direction which draws the imaginative line </param>
    /// <param name="offset">Start point for line in the direction</param>
    /// <param name="delatTolerance">
    /// Tolerance for the check. The higher, the less closer the point must the to the direction line.
    /// Negative values will be converted to their absolute value.
    /// </param>
    /// <returns>
    /// True if the point in on the direction line.
    /// </returns>
    public static bool IsPointInDirection(
      Vector3 point, 
      Vector3 direction, 
      Vector3 offset = new Vector3(), 
      float delatTolerance = 0.001f
      )
    {
      delatTolerance = Mathf.Abs(delatTolerance);

      // Equation for one line with a start point and direction: 
      // point.x = offset.x + factor * direction.x
      // point.y = offset.y + factor * direction.y
      // point.z = offset.z + factor * direction.z

      // To find out if point is part of the line extending in infinity for the direction
      // Solve the equation for the factor on each component 
      // the factor on each component should be equal.

      // Solution for factor on one component
      // factor = (point.x - offset.x) / direction

      float xOffset = point.x - offset.x;
      float yOffset = point.y - offset.y;
      float zOffset = point.z - offset.z;

      bool xDirectionIsNull = direction.x == 0f;
      bool yDirectionIsNull = direction.y == 0f;
      bool zDirectionIsNull = direction.z == 0f;

      // float.PositiveInfinity used to prevent passing the check
      // (difference between to factors) <= delatTolerance for component 
      // of the direction being zero.
      float xFactor = xDirectionIsNull ? float.PositiveInfinity : xOffset / direction.x;
      float yFactor = yDirectionIsNull ? float.PositiveInfinity : yOffset / direction.y;
      float zFactor = zDirectionIsNull ? float.PositiveInfinity : zOffset / direction.z;

      float differenceXAndY = Mathf.Abs(xFactor - yFactor);
      float differenceXAndZ = Mathf.Abs(xFactor - zFactor);
      float differenceYAndZ = Mathf.Abs(yFactor - zFactor);

      bool xFactorToYFactorDifferenceInTolerance = differenceXAndY <= delatTolerance;
      bool xFactorToZFactorDifferenceInTolerance = differenceXAndZ <= delatTolerance;
      bool yFactorToZFactorDifferenceInTolerance = differenceYAndZ <= delatTolerance;

      bool xIsAtStartWithNoDirection = IsAtStartWihtNoDirectionAmount(xDirectionIsNull, xOffset);
      bool yIsAtStartWithNoDirection = IsAtStartWihtNoDirectionAmount(yDirectionIsNull, yOffset);
      bool zIsAtStartWithNoDirection = IsAtStartWihtNoDirectionAmount(zDirectionIsNull, zOffset);

      if (
        (xIsAtStartWithNoDirection && yIsAtStartWithNoDirection) || 
        (xIsAtStartWithNoDirection && zIsAtStartWithNoDirection) ||
        (yIsAtStartWithNoDirection && zIsAtStartWithNoDirection)
        )
      {
        return true;
      }
      else if (xIsAtStartWithNoDirection)
      {
        return yFactorToZFactorDifferenceInTolerance;
      }
      else if (yIsAtStartWithNoDirection)
      {
        return xFactorToZFactorDifferenceInTolerance;
      }
      else if (zIsAtStartWithNoDirection)
      {
        return xFactorToYFactorDifferenceInTolerance;
      }
      else
      {
        return xFactorToYFactorDifferenceInTolerance && xFactorToZFactorDifferenceInTolerance && yFactorToZFactorDifferenceInTolerance;
      }

      bool IsAtStartWihtNoDirectionAmount(in bool isZero, in float coordinateComponentOffset)
        => isZero && coordinateComponentOffset == 0f;
    }
  }

}