using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.ScriptableObjects
{
  /// <summary>
  /// Asset as parameter for component <see cref="NiceGraphicLibrary.Component.GUI.TextTemplate"/> to provide a template for a text with values to be inserted at certain places marked by keys
  /// </summary>
  [CreateAssetMenu(fileName = "NewTextTemplateData", menuName = "NiceGraphicLibrary/TextTemplateData")]
  public class TextTemplateData : ScriptableObject
  {
    private const string DEFAULT_LEFT_SEPARATOR = "{";
    private const string DEFAULT_RIGHT_SEPARATOR = "}";

    [System.Serializable]
    public class ValueEntry
    {
      [Tooltip("Name of key as place to insert value there")]
      public string Name = "";
      [Tooltip("Default value to insert at the certain place")]
      public string DefaultValue = "";
    }

    [SerializeField, Tooltip("Left symbols marking the content of a value to be inserted.")]
    private string _leftSeparator = DEFAULT_LEFT_SEPARATOR;
    [SerializeField, Tooltip("Right symbols marking the content of a value to be inserted.")]
    private string _rightSeparator = DEFAULT_RIGHT_SEPARATOR;

    [SerializeField, TextArea, Tooltip("Template to render a text with inserted values.")]
    private string TextTemplate = "";

    [SerializeField, TextArea, Tooltip("Result text with default values to get impression.")]
    private string TextPreview = "";

    [SerializeField, Tooltip("Key for a certain place with a certain value inserted there.")]
    private List<ValueEntry> ValueInText;

    /// <summary>
    /// Text created from the text template with default values inserted.
    /// </summary>
    public string GetPreviewText() => TextPreview;

    /// <summary>
    /// Table of keys with their default values in the text template.
    /// </summary>
    public Dictionary<string, string> DefaultValuesCopy => new Dictionary<string, string>(_defaultValueTable);

    
    private Dictionary<string, string> _defaultValueTable = new Dictionary<string, string>();

    private ListToDictionaryConverter<string, string, ValueEntry> _converter =
      new ListToDictionaryConverter<string, string, ValueEntry>("Name", "DefaultValue");

    private void OnValidate()
    {
      _defaultValueTable = _converter.CreateDictionaryFrom(ValueInText);
      TextPreview = GetUpdatedText(_defaultValueTable);
    }

    /// <summary>
    /// Returns a text on base of the text template and provided values.
    /// </summar>
    /// <param name="values">
    /// Values with keys for the places to be inserted.
    /// </param>
    private string GetUpdatedText(Dictionary<string, string> values)
    {     
      var newTextBuilder = new StringBuilder(TextTemplate);

      foreach (KeyValuePair<string, string> valueEntry in values)
      {
        newTextBuilder.Replace($"{_leftSeparator}{valueEntry.Key}{_rightSeparator}", valueEntry.Value);
      }

      return newTextBuilder.ToString();
    }

    /// <summary>
    /// Returns a rendered text with values provided by a dictionary and the new value.
    /// Provided dictionary will be updated after call.
    /// </summary>
    /// <param name="Name">
    /// Key of new value. Note if not defined in text template the new value will not considered.
    /// </param>
    /// <param name="newValue">
    /// New value to be inserted. if null, the word [null] will be inserted.
    /// </param>
    public string GetTextWithInsertedValue(
      in string Name, 
      in object newValue, 
      Dictionary<string, string> currentValues
      )
    {
      if (currentValues.ContainsKey(Name))
      {
        currentValues[Name] = newValue == null ? "null" : newValue.ToString();        
      }
      else
      {
        Debug.LogWarning($"{nameof(Name)} [{Name}] does not exit in this text template.");
      }

      return GetUpdatedText(currentValues);
    }

    

   

  }
}
