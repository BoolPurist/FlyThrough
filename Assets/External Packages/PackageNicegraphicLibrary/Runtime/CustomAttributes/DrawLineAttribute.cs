using System;
using UnityEngine;

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Draws a colored line in the inspector between property fields
  /// </summary>
  public class DrawLineAttribute : PropertyAttribute 
  {
    /// <summary>
    /// Width of the line drawn in inceptor
    /// </summary>
    public int Width { get; private set; } = START_WIDTH;
    /// <summary>
    /// Empty space from the drawn line and the above inspector field
    /// </summary>
    public int Space { get; private set; } = START_SPACE;
    /// <summary>
    /// Color to draw the line in
    /// </summary>
    public Color LineColor { get; private set; } = START_LINE_COLOR;

    private const int START_WIDTH = 5;
    private const int START_SPACE = 5;
    private static readonly Color START_LINE_COLOR = Color.white;
    private const string START_COLOR_NAME = "white";
    
    /// <param name="color">
    /// Color to draw the line in
    /// </param>
    /// <param name="width">
    /// Width of the line drawn in inceptor
    /// </param>
    /// <param name="space">
    /// Empty space from the drawn line and the above inspector field
    /// </param>
    public DrawLineAttribute(string color, int width, int space)
    {
      TryConvertStringToLineColor(color);
      Width = Math.Abs(width);
      Space = Math.Abs(space);
    }

    public DrawLineAttribute() { }

    /// <param name="color">
    /// Color to draw the line in
    /// </param>
    public DrawLineAttribute(string color) : this(color, START_WIDTH, START_SPACE) { }
    /// <param name="width">
    /// Width of the line drawn in inceptor
    /// </param>
    public DrawLineAttribute(int width) : this(START_COLOR_NAME, width, START_SPACE) { }
    /// <param name="width">
    /// Width of the line drawn in inceptor
    /// </param>
    /// <param name="space">
    /// Empty space from the drawn line and the above inspector field
    /// </param>
    public DrawLineAttribute(int width, int space) : this(START_COLOR_NAME, width, space) { }
    /// <param name="color">
    /// Color to draw the line in
    /// </param>
    /// <param name="width">
    /// Width of the line drawn in inceptor
    /// </param>
    public DrawLineAttribute(string color, int width) : this(color, width, START_SPACE) { }
    
    private void TryConvertStringToLineColor(string colorToParse)
    {
      Color newColor = LineColor;
      bool couldParse = ColorUtility.TryParseHtmlString(colorToParse, out newColor);
      LineColor = newColor;

      if (!couldParse)
      {
        Debug.LogError($"String to parse of the color of the {nameof(DrawLineAttribute)} could not be parsed to a unity color. String is {colorToParse}");
      }
    }
  }
}