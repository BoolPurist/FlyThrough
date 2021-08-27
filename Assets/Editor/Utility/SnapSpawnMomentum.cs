using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FlyThrough.Tools
{
  public class SnapSpawnMomentum
  {
    public GameObject[] SpawnedObjects { get; private set; }    


    public SnapSpawnMomentum(GameObject[] spawnedObjects)
      => SpawnedObjects = spawnedObjects;
  }
}
