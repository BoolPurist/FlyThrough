using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Extensions
{
  public static class StringExtension
  {
    /// <summary>
    /// Returns a string encapsulated with a [color] tag so it will be displayed as colored text if printed. 
    /// </summary>
    public static string WithColor(this string text, Color color)
      => $"<color=\"#{ColorUtility.ToHtmlStringRGB(color)}\">{text}</color>";
    /// <summary>
    /// Returns a string encapsulated with a [color] tag so it will be displayed as colored text if printed
    /// according to given hex code.
    /// </summary>
    /// <param name="hexCodeColor">
    /// Expected Format: 6 symbols from 0 to F as hex number. # at the start is not needed.
    /// </param>
    public static string WithHexColor(this string text, string hexCodeColor)
    {
      hexCodeColor = hexCodeColor.Contains("#") ? hexCodeColor : "#" + hexCodeColor;
      return $"<color=\"{hexCodeColor}\">{text}</color>";
    } 
    /// <summary>
    /// Returns a string encapsulated with a [b] tag so it will be displayed as bold text if printed
    /// </summary>
    public static string Bold(this string text) => $"<b>{text}</b>";
    /// <summary>
    /// Returns a string encapsulated with a [i] tag so it will be displayed as italic text if printed
    /// </summary>
    public static string Italic(this string text) => $"<i>{text}</i>";
    /// <summary>
    /// Returns a string encapsulated with a [size] tag so it will be displayed in a given font size
    /// </summary>
    public static string WithSize(this string text, int size) => $"<size={Mathf.Abs(size)}>{text}</size>";
  } 
}