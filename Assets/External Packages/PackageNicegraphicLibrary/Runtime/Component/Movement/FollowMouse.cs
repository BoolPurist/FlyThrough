using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.Movement
{
  /// <summary>
  /// Game object which the component is attached to, follows the mouse cursor.
  /// </summary>
  public class FollowMouse : MonoBehaviour, ITakesGameInputProviderProvider
  {
    private IGameInputProvider _gameInputProvider = new UnityGameInputProvider();
    public void SetKeyButtonProvider(IGameInputProvider newProvider)
    {
      if (newProvider != null)
      {
        _gameInputProvider = newProvider;
      }
    }

    private void Update()
    {      
      Vector2 currentMousePosition = _gameInputProvider.GetMousePosition();
      gameObject.transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, transform.position.z);
    }


  }
}