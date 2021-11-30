using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
  private const string PROPERTY_NAME_SKYBOX = "_Rotation";

  [SerializeField]
  [Tooltip("Speed at which the sky box is rotating")]
  [Min(0)]
  private float _rotationSpeed = 1f;

  private void Start() => RenderSettings.skybox.SetFloat(PROPERTY_NAME_SKYBOX, 0f);

  private void OnDestroy() => RenderSettings.skybox.SetFloat(PROPERTY_NAME_SKYBOX, 0f);


  void FixedUpdate()
  {
    
    float oldRotation = RenderSettings.skybox.GetFloat(PROPERTY_NAME_SKYBOX);
    float newRotation = (oldRotation + (_rotationSpeed * Time.deltaTime)) % 360f;
    RenderSettings.skybox.SetFloat(PROPERTY_NAME_SKYBOX, newRotation);
  }
}
