using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlyThrough
{
  public class KeybindingBarComponentAccessor : MonoBehaviour
  {
    
    public TextMeshProUGUI KeyLabelText;    
    public TextMeshProUGUI KeybindingText;
    public Button KeybindingButton;
    public ListeningToKeyStrokeOnClick KeyListener;
  }

}
