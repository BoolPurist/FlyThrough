using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FlyThrough.Tools
{
  /// <summary>
  /// Represents a state for the SnapSpawnController. Used to revert spawn results.
  /// </summary>
  public class SnapSpawnMomentum
  {
    /// <summary>
    /// All objects from one spawn, call of SnapSpawnCommand in the class SnapSpawnController
    /// </summary>
    public GameObject[] SpawnedObjects { get; private set; }    


    public SnapSpawnMomentum(GameObject[] spawnedObjects)
      => SpawnedObjects = spawnedObjects;
  }
}
