using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NiceGraphicLibrary.Component.GUI
{
  /// <summary>
  /// Component to let the player select buttons via keyboard
  /// </summary>
  public class KeyboardButtonNavigation : MonoBehaviour
  {
    [SerializeField, Tooltip("Buttons to navigate through via keyboard. The start is at element 0, end is at 1. element from bottom")]
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0090 // Use 'new(...)'
    private List<Button> _buttonToNavigate = new List<Button>();
#pragma warning restore IDE0090 // Use 'new(...)'

    [SerializeField, Tooltip("if pressed then next bottom button in list Button To Navigate ")]
    private KeyCode GoUpKey = KeyCode.UpArrow;
    [SerializeField, Tooltip("if pressed then next top button in list Button To Navigate ")]
    private KeyCode GoDownKey = KeyCode.DownArrow;

    [SerializeField, Tooltip("if pressed then the current selected button is pressed")]
    private KeyCode ConfirmKey = KeyCode.Space;

    [SerializeField, Tooltip("if true then the navigation cycles if selection goes over top or bottom of list.")]
    private bool _navigateInCycle;
#pragma warning restore IDE0044 // Add readonly modifier

    #region internal fields
    private int _lastIndexOfNavigatedButtons = -1;

    private bool isStandaloneInputModulePresent = false;
    private string nameOfSubmitButtonInStandaloneInputModule = "";

    private Button _currentSelectedButton;
    private int _currentIndex;
    #endregion

    #region Unity callbacks

#pragma warning disable IDE0051 // Remove unused private members
    private void OnEnable()
    {
      ResetNavigation();
      EnsureCompatibilityWithStandaloneModule();
      StartListning();
    }
    private void OnDisable() => StopListiningForClickEvents();
    private void OnDestroy() => StopListiningForClickEvents();
    private void OnValidate()
    {
      SetNaviagetInCycle(_navigateInCycle);
      _lastIndexOfNavigatedButtons = CountOfButtons - 1;
    }

    private void Update()
    {
      if (Input.GetKeyDown(GoDownKey))
      {
        _currentIndex++;
        _currentOverflowNavigationHandler();
        SelectNewButton();
      }
      else if (Input.GetKeyDown(GoUpKey))
      {
        _currentIndex--;
        _currentUnderFlowNavigationHandler();
        SelectNewButton();
      }
      else if (!IsSubmitOfStandaloneInputModuleFired && Input.GetKeyDown(ConfirmKey) && _currentIndex != -1)
      {
        _currentSelectedButton.Select();
        _currentSelectedButton.onClick.Invoke();        
      }

      void SelectNewButton()
      {
        _currentSelectedButton = _buttonToNavigate[_currentIndex];
        _currentSelectedButton.Select();
      }
    }


    #endregion

    #region Context menu

    [ContextMenu("Collect buttons under this object."), ExecuteInEditMode]
    private void CollectButtonsUnderThisObject()
    {
      Button[] collectedButtons = GetComponentsInChildren<Button>();
      _buttonToNavigate.Clear();
      _buttonToNavigate.AddRange(collectedButtons);
    }

#pragma warning restore IDE0051 // Remove unused private members

    #endregion

    #region internal routines 

    /// <summary>
    /// Set ups navigation state as if not button were selected yet.
    /// </summary>
    private void ResetNavigation()
    {
      _currentSelectedButton = null;
      _currentIndex = -1;
    }

    /// <summary>
    /// Makes sure that a button is not pressed more than once  
    /// if a stand alone input module is listening for the same submit button like the field [Go Up Key]
    /// </summary>
    private void EnsureCompatibilityWithStandaloneModule()
    {
      var standAloneModule = FindObjectOfType<StandaloneInputModule>();

      if (standAloneModule != null)
      {
        nameOfSubmitButtonInStandaloneInputModule = standAloneModule.submitButton;
        isStandaloneInputModulePresent = true;
      }
    }

    /// <summary>
    /// Updates navigation state if player clicks button via mouse.
    /// </summary>
    private void ReactToClickByUser()
    {
      GameObject currentSelectedButton = EventSystem.current.currentSelectedGameObject;
      if (_currentSelectedButton != null && currentSelectedButton != _currentSelectedButton.gameObject)
      {        
        Button currentButton = currentSelectedButton.GetComponent<Button>();
        int newSelectedIndex = _buttonToNavigate.IndexOf(currentButton);
        _currentIndex = newSelectedIndex;
      }
    }

    /// <summary>
    /// Appends logic to handle mouse clicks by player to keep navigation state correct.
    /// </summary>
    private void StopListiningForClickEvents()
    {
      foreach (Button button in _buttonToNavigate)
      {
        if (button != null)
        {
          button.onClick.RemoveListener(ReactToClickByUser);
        }
      }
    }

    /// <summary>
    /// Ensures correct unsubscribing  for click events of navigate buttons.
    /// </summary>
    private void StartListning()
    {
      foreach (Button button in _buttonToNavigate)
      {
        if (button != null)
        {
          button.onClick.AddListener(ReactToClickByUser);
        }
      }
    }


    #endregion

    #region Internal getters

    /// <summary>
    /// Number of navigated buttons
    /// </summary>
    private int CountOfButtons => _buttonToNavigate.Count;

    /// <summary>
    /// True if submit button was pressed which the standalone input module is listening to.
    /// </summary>
    private bool IsSubmitOfStandaloneInputModuleFired => isStandaloneInputModulePresent && Input.GetButtonDown(nameOfSubmitButtonInStandaloneInputModule);

    #endregion

    #region Handling of navigation index

    /// <summary>
    /// If called then field _navigateInCycle is updated
    /// and the according delegates to handle index going outside the range of the list of navigated buttons will be updated.
    /// </summary>
    private void SetNaviagetInCycle(bool newValue)
    {
      _navigateInCycle = newValue;

      if (newValue)
      {
        _currentOverflowNavigationHandler = CycleOverflowNavigation;
        _currentUnderFlowNavigationHandler = CycleUnderflowNavigation;
      }
      else
      {
        _currentOverflowNavigationHandler = ClampOverflowNavigation;
        _currentUnderFlowNavigationHandler = ClampUnderflowNavigation;
      }
    }

    /// <summary>
    /// Handler for case if index is greater than the last index of navigate buttons.
    /// </summary>
    private Action _currentOverflowNavigationHandler;
    /// <summary>
    /// Handler for case if index is smaller than zero.
    /// </summary>
    private Action _currentUnderFlowNavigationHandler;

    /// <summary>
    /// If index will be clamped at last index of the list of navigate buttons
    /// </summary>
    private void ClampOverflowNavigation() => _currentIndex = Mathf.Min(_currentIndex, _lastIndexOfNavigatedButtons);
    /// <summary>
    /// If index will be clamped at zero
    /// </summary>
    private void ClampUnderflowNavigation() => _currentIndex = Mathf.Max(_currentIndex, 0);

    /// <summary>
    /// If index will be set to zero if index would be greater than the last index.
    /// </summary>
    private void CycleOverflowNavigation()
      => _currentIndex = _currentIndex > _lastIndexOfNavigatedButtons ? 0 : _currentIndex;

    /// <summary>
    /// If index will be set to the last index if the index would be negative.
    /// </summary>
    private void CycleUnderflowNavigation()
      => _currentIndex = _currentIndex < 0 ? _lastIndexOfNavigatedButtons : _currentIndex;

    #endregion



  }
}