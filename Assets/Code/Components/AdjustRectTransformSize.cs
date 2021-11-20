using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public class AdjustRectTransformSize : MonoBehaviour
{
  private RectTransform _rectTransform;
  [SerializeField]
  private float Height = 100f;
  [SerializeField]
  private float Width = 100f;

  
  private void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
  }

  private void Update()
  {

    _rectTransform.sizeDelta = new Vector2(Width, Height);
    
  }

}
