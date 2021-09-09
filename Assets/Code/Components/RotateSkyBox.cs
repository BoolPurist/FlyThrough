using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
  [SerializeField]
  [Tooltip("Speed at which the sky box is rotating")]
  [Min(0)]
  private float _rotationSpeed = 1f;

  void FixedUpdate()
  {
    const string PROPERTY_NAME = "_Rotation";
    float oldRotation = RenderSettings.skybox.GetFloat(PROPERTY_NAME);
    float newRotation = (oldRotation + (_rotationSpeed * Time.deltaTime)) % 360f;
    RenderSettings.skybox.SetFloat(PROPERTY_NAME, newRotation);
  }
}
