using System;
using UnityEngine;

namespace NiceGraphicLibrary
{
  /// <summary>
  /// Attribute placed above a field to rendere the variable only as read only in editor/play mode or only play mode
  /// </summary>
  public class ReadOnlyFieldAttribute : PropertyAttribute 
  {
    private readonly string _differentName = null;
    public string DifferentName => _differentName;

    public bool ReadOnlyForPlay { get; private set; } = false;

    public bool IsEmpty => string.IsNullOrWhiteSpace(DifferentName);


    /// <param name="differentName">
    /// Different name to render for the lable name instead of the field name of the variable in the class
    /// </param>
    public ReadOnlyFieldAttribute(string differentName = null) => _differentName = differentName; 
    public ReadOnlyFieldAttribute(bool readOnlyForPlay) => ReadOnlyForPlay = readOnlyForPlay;

    public ReadOnlyFieldAttribute(string differentName, bool readOnlyForPlay)
    {
      _differentName = differentName;
      ReadOnlyForPlay = readOnlyForPlay;
    }
  }
}