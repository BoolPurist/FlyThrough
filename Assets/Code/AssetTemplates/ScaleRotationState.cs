using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [CreateAssetMenu(fileName = "new transformation configuration", menuName = "Transformation State")]
  /// <summary>
  /// Template to create configuration for start rotation and scaling 
  /// used in the component ApplyRotationScaleState to give object desired
  /// value/offset scaling and rotation.
  /// </summary>
  public class ScaleRotationState : ScriptableObject
  {    
    [SerializeField]
    [Tooltip("Scale which the object will have, if spawn in scene.")]
    private Vector3 _scaling;
    [SerializeField]
    [Tooltip("Rotation which the object will have, if spawn in scene.")]
    private Vector3 _rotation;
    public Vector3 Scaling => _scaling;
    public Vector3 Rotation => _rotation;
  }
}
