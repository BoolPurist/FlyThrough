using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ListeningToKeyStrokeOnClick : MonoBehaviour, IPointerDownHandler
{
  [SerializeField]
  private TextMeshProUGUI _textForKeybinding;
  

  [SerializeField, Min(0)]
  private float AlternateDuration = 1f;

  [SerializeField, Min(0)]
  private float FadeMin = 0.5f;

  [SerializeField]
  private string ListiningKeyBindingSymbol = "_";

  public event Action<ListeningToKeyStrokeOnClick> OnStartListeningForKeystroke;

  public event Action<ListeningToKeyStrokeOnClick> OnStopListeningForKeystroke;


  private Image _buttonImage;

  private string _keyBindingSymbol;
  private bool _isListeningForKeyStroke = false;
  private Button _button;

  private Coroutine _alternatingFadingRoutine;

  private KeyCode _currentKeyCode = KeyCode.None;

  public KeyCode CurrentKeyCode => _currentKeyCode;

  private void Awake()
  {    
    _keyBindingSymbol = _textForKeybinding.text;    
  }
  
  private void Start()
  {
    _button = GetComponent<Button>();
    _buttonImage = GetComponent<Image>();
  }
  private void InitializeListeningForKeyStroke()
  {
    _isListeningForKeyStroke = true;
    _button.interactable = false;
    _textForKeybinding.text = ListiningKeyBindingSymbol;
    _alternatingFadingRoutine = StartCoroutine(AlternateBetweenFaded());
  }

  private IEnumerator AlternateBetweenFaded()
  {
    Color notFadedColor = _buttonImage.color;
    Color fadedColor = _buttonImage.color;
    fadedColor.a = FadeMin;

    while (_isListeningForKeyStroke)
    {
      _buttonImage.color = fadedColor;
      yield return new WaitForSeconds(AlternateDuration);
      _buttonImage.color = notFadedColor;
      yield return new WaitForSeconds(AlternateDuration);
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (!_isListeningForKeyStroke && _button.interactable)
    {      
      InitializeListeningForKeyStroke();
      
      OnStartListeningForKeystroke?.Invoke(this);
    }
  }

  
  public void OnGUI()
  {
    if (_isListeningForKeyStroke && IsAnyKeyHit())
    {      
      StopListeningForKeyStroke();      
    }
  }

  private void StopListeningForKeyStroke()
  {
    if (_isListeningForKeyStroke) _textForKeybinding.text = _keyBindingSymbol;
    _isListeningForKeyStroke = false;
    
    if (_alternatingFadingRoutine != null) StopCoroutine(_alternatingFadingRoutine);
    _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.b, _buttonImage.color.g, 1f);
    _button.interactable = true;

    OnStopListeningForKeystroke?.Invoke(this);
  }

  private void OnDisable() => StopListeningForKeyStroke();

  private void OnDestroy() => StopListeningForKeyStroke();

  private bool IsAnyKeyHit()
  {
    Event currentEvent = Event.current;
    if (currentEvent.isKey)
    {             
      _currentKeyCode = currentEvent.keyCode;
      _keyBindingSymbol = _currentKeyCode.ToString();
      return true;
    }

    return false;
  }

  
}
