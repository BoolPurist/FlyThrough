
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using NiceGraphicLibrary.Utility.Coroutines;

[RequireComponent(typeof(ScrollRect))]
public class RollCredits : MonoBehaviour, IDragHandler
{
  

  [SerializeField, Min(0f)]
  private float TimeToScroll = 5f;

  [SerializeField, Min(0f)]
  private float DelayUntilScroll = 0f;

  private RollCredits _rollerForCredits;

  private float _currentPassedTime = 0f;
  private ScrollRect _scrollRect;

  private void Start()
  {
    _scrollRect = GetComponent<ScrollRect>();
    _rollerForCredits = GetComponentInChildren<RollCredits>();
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

  public void OnDrag(PointerEventData eventData)
  {    
    // If user drags the credit scroll rectangle then automatic scrolling should end.
    _rollerForCredits.StopAllCoroutines();
  }
}
