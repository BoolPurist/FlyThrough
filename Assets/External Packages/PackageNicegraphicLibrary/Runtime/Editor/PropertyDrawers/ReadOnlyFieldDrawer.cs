using UnityEditor;
using UnityEngine;

namespace NiceGraphicLibrary.Editor
{
  [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
  public class ReadOnlyFieldDrawer : PropertyDrawer
  {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      ReadOnlyFieldAttribute Atrribute = (ReadOnlyFieldAttribute)attribute;

      if (!Atrribute.ReadOnlyForPlay || Application.isPlaying)
      {
        label.text = Atrribute.IsEmpty ? label.text : Atrribute.DifferentName;

        bool wasEnabled = GUI.enabled;
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = wasEnabled;
      }
      else 
      {
        EditorGUI.PropertyField(position, property, label, true);
      }
    }
  } 
}