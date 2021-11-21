using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

[ExecuteInEditMode]
public class CreateNewKeyBinding : MonoBehaviour
{
  [SerializeField]
  private string KeyBindingName;
  [SerializeField]
  private string ObjectName;
  [SerializeField]
  private string KeyBinding;

  [SerializeField]
  private GameObject Prefab;

  [ContextMenu("Create Key Binding")]
  private void CreateKeyBinding()
  {
    GameObject newKeyBinding = (GameObject)PrefabUtility.InstantiatePrefab(Prefab, transform);
    newKeyBinding.name = ObjectName;
    newKeyBinding.transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = KeyBindingName;
    newKeyBinding.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<TextMeshProUGUI>().text = KeyBindingName;
  }
}
