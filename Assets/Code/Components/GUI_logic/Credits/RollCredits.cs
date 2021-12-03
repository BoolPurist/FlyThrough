
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NiceGraphicLibrary.Utility.Coroutines;

[RequireComponent(typeof(ScrollRect))]
public class RollCredits : MonoBehaviour
{
  

  [SerializeField, Min(0f)]
  private float TimeToScroll = 5f;

  [SerializeField, Min(0f)]
  private float DelayUntilScroll = 0f;

  private float _currentPassedTime = 0f;
  private ScrollRect _scrollRect;

  private void Start()
  {
    _scrollRect = GetComponent<ScrollRect>();
    CoroutineUtility.StartCoroutineDelayed(this, RollDown, DelayUntilScroll);
  }

  private IEnumerator RollDown()
  {
    while (_currentPassedTime <= TimeToScroll)
    {
      float currentRatio = _currentPassedTime / TimeToScroll;
      _scrollRect.verticalNormalizedPosition = 1f - currentRatio;
      _currentPassedTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    
  }

  
}
