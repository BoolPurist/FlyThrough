using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [ExecuteAlways]
  /// <summary>
  /// Object gets scaled/rotated whenever it placed in a scene or spawned runtime.
  /// with direct values in combination with an offset.
  /// Used to ensure unified scaling/rotation across different prefabs
  /// </summary>
  public class ApplyRotationScaleState : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("Start rotation and scaling value when spawned")]
    private ScaleRotationState _tranformationState;
    [SerializeField]
    [Tooltip("Added as offset to start rotation and scaling value when spawned")]
    private ScaleRotationState _transformOffset;

    private void Awake() => Apply();

    private void OnValidate() => Apply();
    
    [ContextMenu("Apply")]
    private void Apply()
    {
      if (_tranformationState != null)
      {
        ApplyScaling();
        ApplyRotation();
      }
      if (_transformOffset != null)
      {
        ApplyScalingOffset();
        ApplyRotationOffset();
      }
    }


    private void ApplyScaling() => transform.localScale = _tranformationState.Scaling;
    private void ApplyScalingOffset() => transform.localScale += _transformOffset.Scaling;

    private void ApplyRotation() => transform.eulerAngles = _tranformationState.Rotation;
    private void ApplyRotationOffset() => transform.Rotate(_transformOffset.Rotation);
  }
}
