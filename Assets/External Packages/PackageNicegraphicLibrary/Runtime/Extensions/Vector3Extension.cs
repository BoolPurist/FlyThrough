using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NiceGraphicLibrary.Extensions
{
  /// <summary>
  /// Extensions methods to calculate Vector3 with floats, multiply Vector3 with each other.
  /// </summary>
  public static class Vector3Extension
  {
    public static Vector3 AddScale(this Vector3 vector, float scale) => vector + (Vector3.one * scale);
    public static Vector3 SubScale(this Vector3 vector, float scale) => vector - (Vector3.one * scale);

    public static Vector3 AddXScale(this Vector3 vector, float scale) => vector + (Vector3.right * scale);
    public static Vector3 AddYScale(this Vector3 vector, float scale) => vector + (Vector3.up * scale);
    public static Vector3 AddZScale(this Vector3 vector, float scale) => vector + (Vector3.forward * scale);

    public static Vector3 SubXScale(this Vector3 vector, float scale) => vector - (Vector3.right * scale);
    public static Vector3 SubYScale(this Vector3 vector, float scale) => vector - (Vector3.up * scale);
    public static Vector3 SubZScale(this Vector3 vector, float scale) => vector - (Vector3.forward * scale);
    
    public static Vector3 MultiplyWithXScale(this Vector3 vector, float scale)
#pragma warning disable IDE0090 // Use 'new(...)'
      => new Vector3(vector.x * scale, vector.y, vector.z);

    public static Vector3 MultiplyWithYScale(this Vector3 vector, float scale)
      => new Vector3(vector.x, vector.y * scale, vector.z);
    public static Vector3 MultiplyWithZScale(this Vector3 vector, float scale)
      => new Vector3(vector.x, vector.y, vector.z * scale);

    public static Vector3 DivideByXScale(this Vector3 vector, float scale)
      => new Vector3(vector.x / scale, vector.y, vector.z);
    public static Vector3 DivideByYScale(this Vector3 vector, float scale)
      => new Vector3(vector.x, vector.y / scale, vector.z);
    public static Vector3 DivideByZScale(this Vector3 vector, float scale)
      => new Vector3(vector.x, vector.y, vector.z / scale);

    public static Vector3 DivideByVector(this Vector3 vector, Vector3 otherVector)
      => new Vector3(vector.x / otherVector.x, vector.y / otherVector.y, vector.z / otherVector.z);

#pragma warning restore IDE0090 // Use 'new(...)'
  }
}
