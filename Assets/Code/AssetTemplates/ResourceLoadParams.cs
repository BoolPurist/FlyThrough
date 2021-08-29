using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [CreateAssetMenu(fileName = "new resource load param", menuName = "Data/Resource Load Parameters")]
  public class ResourceLoadParams : ScriptableObject
  {
    [SerializeField]
    private string _pathToResources;

    public string PathToResources => _pathToResources;
  }

}