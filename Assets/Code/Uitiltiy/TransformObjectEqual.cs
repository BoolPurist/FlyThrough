using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough 
{
  public static class TransformObjectEqual
  {
    public static void ScaleFromOriginTo(GameObject origin, GameObject toTranslate)
      => toTranslate.transform.localScale = origin.transform.localScale;
    
    public static void TranslateFromOriginTo(GameObject origin, GameObject toRotate) 
      => toRotate.transform.position = origin.transform.position;
    
    public static void RotateFromOriginTo(GameObject origin, GameObject toScale) 
      => toScale.transform.rotation = origin.transform.rotation;
    public static void TransformFromOriginTo(GameObject origin, GameObject toTransform)
    {
      TranslateFromOriginTo(origin, toTransform);
      RotateFromOriginTo(origin, toTransform);
      ScaleFromOriginTo(origin, toTransform);
    }
  }
}
