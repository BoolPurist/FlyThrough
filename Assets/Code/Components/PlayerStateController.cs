using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FlyThrough
{
  public class PlayerStateController : MonoBehaviour
  {
    [System.Serializable]
    public class UIFields
    {
      public Canvas UIContainer;
      public Toggle GoodModeOnToggle;
      public Toggle IsControledManuellyToggle;
      public Toggle IgnoreObstaclesToggle;
    }

    private const string PLAYER_TAG_NAME = "Player";
    private const string PLAYER_LAYER = "Player";
    private const string PLAYER_LAYER_IGNORE_NORMAL_WALLS = "IgnoreNormalWalls";

    [SerializeField]
    private bool ShowDebugMenu = false;
    [SerializeField]
    private bool GoodModeOn = false;
    [SerializeField]
    private bool IsControledManuelly = false;
    [SerializeField]
    private bool IgnoresObstacles = false;
    [SerializeField]
    private UIFields ToggleControls;

    private string CurrentLayer => IgnoresObstacles ?
      PLAYER_LAYER_IGNORE_NORMAL_WALLS : PLAYER_LAYER;

    private void Update()
    {
      _playerPusher.SetManuelControl(IsControledManuelly);
      _playerDeathControll.SetIgnoreDeath(GoodModeOn);
      _player.layer = LayerMask.NameToLayer(CurrentLayer);

      if (ShowDebugMenu)
      {
        ToggleControls.UIContainer.gameObject.SetActive(true);
      }
      else
      {
        HidePlayerDebugMenu();
      }
    }

    private GameObject _player;
    private EndIt _playerDeathControll;
    private PushPlayerForwardOrBack _playerPusher;

    private void Awake()
    {
      if (!Debug.isDebugBuild)
      {
        GoodModeOn = false;
        IsControledManuelly = false;
        IgnoresObstacles = false;
        HidePlayerDebugMenu();
      }
    }

    private void OnValidate()
    {
      if (ToggleControls.GoodModeOnToggle != null)
      {
        ToggleControls.GoodModeOnToggle.isOn = GoodModeOn;
      }
      if (ToggleControls.IsControledManuellyToggle != null)
      {
        ToggleControls.IsControledManuellyToggle.isOn = IsControledManuelly;
      }
      if (ToggleControls.IgnoreObstaclesToggle != null)
      {
        ToggleControls.IgnoreObstaclesToggle.isOn = IgnoresObstacles;
      }
    }

    private void Start()
    {
      _player = GameObject.FindGameObjectWithTag(PLAYER_TAG_NAME);

      if (_player == null)
      {
        Debug.LogWarning($"object {nameof(_player)} with tag name {PLAYER_TAG_NAME} as player not found in scene !");
      }

      _playerPusher = _player.GetComponent<PushPlayerForwardOrBack>();
      _playerDeathControll = _player.GetComponent<EndIt>();
    }

    public void ToggleGoodMode(bool toogleValue) => GoodModeOn = toogleValue;
    public void ToggleIsControledManuelly(bool toogleValue) => IsControledManuelly = toogleValue;
    public void ToggleIgnoresObstacles(bool toogleValue) => IgnoresObstacles = toogleValue;

    public void ResetLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void HidePlayerDebugMenu()
    {
      ShowDebugMenu = false;
      ToggleControls.UIContainer.gameObject.SetActive(false);
    }
  }
}
