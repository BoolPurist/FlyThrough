using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FlyThrough
{
  [RequireComponent(typeof(TextMeshProUGUI))]
  public class FlashingText : MonoBehaviour
  {
    [SerializeField, Min(0f)]
    private float AlternateDuration = 1f;
    [SerializeField, Range(0f, 1f)]
    private float MinFade = 0.5f;

    private float _currentTime;

    private TextMeshProUGUI _textToFlash;

    private void Start()
    {
      _textToFlash = GetComponent<TextMeshProUGUI>();
      StartCoroutine(StartingAlternating());
    }

    private IEnumerator StartingAlternating()
    {
      while (true)
      {
        yield return StartCoroutine(AlternateFadingTextAlpha(FadeOut));
        yield return StartCoroutine(AlternateFadingTextAlpha(FadeIn));
      }
    }

    private IEnumerator AlternateFadingTextAlpha(Action<float> interpolation)
    {
      while (_currentTime <= AlternateDuration)
      {
        _currentTime += Time.deltaTime;
        float currentRatio = _currentTime / AlternateDuration;
        interpolation(currentRatio);
        yield return new WaitForEndOfFrame();
      }

      _currentTime = 0f;
      
    }

    private void FadeIn(float ratio)
    {
      float newAlpha = Mathf.Lerp(MinFade, 1f, ratio);
      ChangeAlpha(newAlpha);
    }

    private void FadeOut(float ratio)
    {
      float newAlpha = Mathf.Lerp(1f, MinFade, ratio);
      ChangeAlpha(newAlpha);
    }

    private void ChangeAlpha(in float newFadeValue)
    {
      Color newColor = _textToFlash.color;
      newColor.a = newFadeValue;
      _textToFlash.color = newColor;
    }
    
  }
}
