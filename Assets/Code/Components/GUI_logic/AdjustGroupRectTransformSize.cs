using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [ExecuteAlways]
  public class AdjustGroupRectTransformSize : MonoBehaviour
  {
    private enum AdjustMode { Width, Height, WidthAndHeight, Nothing }

    [SerializeField]
    private List<RectTransform> _rectTransformToScale;
    [SerializeField]
    private float Height = 100f;
    [SerializeField]
    private float Width = 100f;
    [SerializeField]
    private AdjustMode WhatToAdjust = AdjustMode.Nothing;

    private bool ListIsNotEmpty => _rectTransformToScale != null && _rectTransformToScale.Count != 0;

    private void Update()
    {
      if (ListIsNotEmpty)
      {
        foreach (RectTransform toAdjust in _rectTransformToScale)
        {
          if (toAdjust != null)
          {
            if (WhatToAdjust == AdjustMode.WidthAndHeight)
            {
              toAdjust.sizeDelta = new Vector2(Width, Height);
            }
            else if (WhatToAdjust == AdjustMode.Width)
            {
              toAdjust.sizeDelta = new Vector2(Width, toAdjust.sizeDelta.y);
            }
            else if (WhatToAdjust == AdjustMode.Height)
            {
              toAdjust.sizeDelta = new Vector2(toAdjust.sizeDelta.y, Height);
            }            
          }
        }
      }

    }
  }

}
