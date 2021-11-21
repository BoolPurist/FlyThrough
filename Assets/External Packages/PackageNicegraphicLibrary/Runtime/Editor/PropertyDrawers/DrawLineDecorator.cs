using System;
using UnityEditor;
using UnityEngine;

namespace NiceGraphicLibrary.Editor
{
  [CustomPropertyDrawer(typeof(DrawLineAttribute))]
  public class DrawLineDecorator : DecoratorDrawer
  {
    private DrawLineAttribute _usedAttribute;

    private DrawLineAttribute UsedAttribute
    {
      get
      {
        if (_usedAttribute == null)
        {
          _usedAttribute = (DrawLineAttribute)attribute;
        }

        return _usedAttribute;
      }

    }


    public override float GetHeight()
    {            
      return Convert.ToInt32(UsedAttribute.Width) + (Convert.ToInt32(UsedAttribute.Space) * 2f);
    }

    public override void OnGUI(Rect position)
    {
      float nextY = position.y + UsedAttribute.Space;
      Rect spaceAreaTop = position;
      spaceAreaTop.height = UsedAttribute.Space;
      

      Rect lineArea = position;
      lineArea.height = UsedAttribute.Width;
      lineArea.y = nextY;

      nextY += lineArea.height;

      Rect spaceAreaBottom = position;
      spaceAreaBottom.height = UsedAttribute.Space;
      spaceAreaBottom.y = nextY;

      EditorGUI.DrawRect(spaceAreaTop, Color.clear);
      EditorGUI.DrawRect(lineArea, UsedAttribute.LineColor);
      EditorGUI.DrawRect(spaceAreaBottom, Color.clear);
      
    }
  } 
}