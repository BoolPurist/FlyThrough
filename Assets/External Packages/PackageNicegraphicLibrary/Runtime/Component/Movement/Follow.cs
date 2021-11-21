using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Component.Movement
{
  [ExecuteInEditMode]
  public class Follow : MonoBehaviour
  {
    [SerializeField]
    private Transform _ObjectToFollow;
    [SerializeField]
    private Vector3 _FollowOffset;

    public void SetObjectToFollow(Transform newObjectToFollow)
    {
      if (newObjectToFollow == null)
      {
        Debug.LogWarning(
          $"In the object {name} in the component {nameof(Follow)}, " +
          $"the parameter {nameof(newObjectToFollow)} was null in the method {nameof(SetObjectToFollow)}"
          );
      }
      else
      {
        _ObjectToFollow = newObjectToFollow;
      }
    }

    public void Update()
    {
      if (_ObjectToFollow != null)
      {
        transform.position = _ObjectToFollow.transform.position + _FollowOffset;
      }
    }
  } 
}