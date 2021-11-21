using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Converter instance to convert list of objects of a given type into a dictionary an back.
  /// </summary>
  /// <typeparam name="TKey">
  /// Type of the key in the converted dictionary
  /// </typeparam>
  /// <typeparam name="TValue">
  /// Type of the value under a key in the converted dictionary
  /// </typeparam>
  /// <typeparam name="OType">
  /// Type of objects to be converted to a dictionary and back from a dictionary
  /// </typeparam>
  /// <remarks>
  /// Use case for this class.
  /// Dictionary can not be serialized in the unity inspector but simple cs classes can be serialized.
  /// Lists of serialized simple cs classes can be set up to look like a dictionary in the unity inspector .
  /// However Dictionaries are more performant for frequent random access than a lists.
  /// This class converts a list of serialized object to a dictionary and lists back. 
  /// </remarks>
  public class ListToDictionaryConverter<TKey, TValue, OType> where OType : new()
  {
    // Both fields must be true if construction is done.
    // Otherwise the given properties can not be found under objects of the given type [OType]     
    private readonly bool _foundKeyProperty = false;
    private readonly bool _foundValueProperty = false;

    private readonly Type _keyType;
    private readonly Type _valueType;
    private readonly Type _objectType;

    // Names of property to extract values for the dictionary.
    private readonly string _keyPropertyName;
    private readonly string _valuePropertyName;

    // Objects to get/set value of a property
    private readonly FieldInfo _keyFieldInfo;
    private readonly FieldInfo _valueFieldInfo;

    /// <param name="keyPropertyName">
    /// Name of the property which has the key for the key of a dictionary
    /// </param>
    /// <param name="valuePropertyName">
    /// Name of the property which has the value for the key of a dictionary
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if no field is found with name of keyPropertyName or valuePropertyName in the class given by OType
    /// </exception>
    public ListToDictionaryConverter(string keyPropertyName, string valuePropertyName)
    {
      _objectType = typeof(OType);
      _keyType = typeof(TKey);
      _valueType = typeof(TValue);

      foreach (MemberInfo member in _objectType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
      {
        if (!_foundKeyProperty && member.Name == keyPropertyName && member is FieldInfo fieldInfoKey && fieldInfoKey.FieldType == _keyType)
        {
          _foundKeyProperty = true;
          _keyPropertyName = keyPropertyName;
          _keyFieldInfo = _objectType.GetField(_keyPropertyName);
        }
        else if (!_foundValueProperty && member.Name == valuePropertyName && member is FieldInfo fieldInfoValue && fieldInfoValue.FieldType == _valueType)
        {
          _foundValueProperty = true;
          _valuePropertyName = valuePropertyName;
          _valueFieldInfo = _objectType.GetField(_valuePropertyName);
        }

      }

      if (!_foundKeyProperty)
      {
        throw new ArgumentException($"In class {_objectType.Name} no field was found with the name [{keyPropertyName}] with type {_keyType.Name}.", nameof(keyPropertyName));
      }
      else if (!_foundValueProperty)
      {
        throw new ArgumentException($"In class {_objectType.Name} no field was found with the name [{valuePropertyName}] with type {_valueType.Name}.", nameof(valuePropertyName));
      }
    }

    /// <summary>
    /// Constructs a dictionary from the list of objects. 
    /// </summary>
    /// <param name="list">
    /// Dictionary to construct a list from. 
    /// If null, returned dictionary is null. If empty, returned dictionary is empty
    /// </param>
    /// <returns>
    /// A dictionary with a key equaling a value of the key property from an object
    /// and with value equaling to the value of the value property from an object.
    /// </returns>    
    public Dictionary<TKey, TValue> CreateDictionaryFrom(IList<OType> list)
    {

      if (list == null)
      {
        return null;
      }

      var dictionary = new Dictionary<TKey, TValue>();

      foreach (OType objectToConvert in list)
      {
        TKey keyProperty = (TKey)_keyFieldInfo.GetValue(objectToConvert);
        TValue valueProperty = (TValue)_valueFieldInfo.GetValue(objectToConvert);

        if (!dictionary.ContainsKey(keyProperty))
        {
          dictionary.Add(keyProperty, valueProperty);
        }
        else
        {
          dictionary[keyProperty] = valueProperty;
        }
      }

      return dictionary;
    }

    /// <summary>
    /// Constructs a list from a given dictionary. This list has objects with properties values extracted from the given dictionary
    /// </summary>
    /// <param name="dictionary">
    /// Dictionary to construct a list from. 
    /// If null, returned list is null. If empty, returned list is empty
    /// </param>
    /// <returns>
    /// A list with elements which are an defaulted objects.
    /// Every object has a value under key property equaling a value of a key of the given dictionary
    /// Every object has a value under value property equaling a value of a value of the given dictionary
    /// </returns>    
    public List<OType> CreateListFrom(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
      {
        return null;
      }

      var list = new List<OType>();

      while (list.Count < dictionary.Count)
      {
        list.Add(new OType());
      }

      KeyValuePair<TKey, TValue>[] keyValuePairs = dictionary.ToArray();
      for (int i = 0; i < keyValuePairs.Length; i++)
      {
        _keyFieldInfo.SetValue(list[i], keyValuePairs[i].Key);
        _valueFieldInfo.SetValue(list[i], keyValuePairs[i].Value);
      }

      return list;
    }

    public override string ToString()
      => $"Converter for type {_objectType.Name} with property Key [{_keyPropertyName}] of type {_keyType} and property Key [{_valuePropertyName}] of type {_valueType}";

  }

}