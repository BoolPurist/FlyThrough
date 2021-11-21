using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

using NiceGraphicLibrary.ScriptableObjects;
using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Component to update [text mesh pro] component according to text template.
  /// Text template is text with certain place to insert values.
  /// </summary>
  [RequireComponent(typeof(TextMeshProUGUI))]
  public class TextTemplate : MonoBehaviour
  {
    [SerializeField]
    private TextTemplateData _templateDate;

    private TextMeshProUGUI _textComponent;

    private Dictionary<string, string> _valueTable = new Dictionary<string, string>();

    private void OnValidate()
    {
      Start();
    }

    public void Start()
    {      
      _textComponent = ComponentUtility.EnsureComponentOn<TextMeshProUGUI>(gameObject);
      if (_textComponent != null)
      {
        
        if (_templateDate == null)
        {
          _textComponent.text = "No data asset applied !";
        }
        else
        {
          _textComponent.text = _templateDate.GetPreviewText();
          _valueTable = _templateDate.DefaultValuesCopy;
        }
      }    
    }

    /// <summary>
    /// Allows to insert a value at certain place in the text template.
    /// </summary>    
    /// <param name="name">
    /// Key for the certain place. Note: if not found in asset [Template Date] nothing will happen.
    /// </param>
    /// <param name="insertedValue">
    /// Value to be inserted at a place in the text template. Note: if null then "null" as word will be inserted.
    /// </param>
    public void InsertValueWithName(in string name, in string insertedValue)
    {
      _textComponent.text = _templateDate.GetTextWithInsertedValue(name, insertedValue, _valueTable);
    }
  } 
}