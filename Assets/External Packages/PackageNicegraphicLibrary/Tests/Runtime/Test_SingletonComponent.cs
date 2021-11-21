using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;

namespace NiceGraphicLibrary.Tests.Runtime
{
  [TestFixture]
  public class Test_SingletonComponent
  {
    [Test]
    public void Test_InstanceProperty()
    {

      var lastFetchedInstance = DummySingleTon.Instance;

      Assert.IsNotNull(lastFetchedInstance, $"Singleton component should have been created.");

      DummySingleTon previousInstance = lastFetchedInstance;
      lastFetchedInstance = DummySingleTon.Instance;

      int instanceCount = GameObject.FindObjectsOfType<DummySingleTon>().Length;
      Assert.AreEqual(1, instanceCount, $"More than once instance of the singleton is present.");
      Assert.AreEqual(lastFetchedInstance, previousInstance, $"Instance was recreated !");

      GameObject.Destroy(lastFetchedInstance.gameObject);
    }

    private class DummySingleTon : SingletonComponent<DummySingleTon>
    {
      public int Counter = 0;
    }

    private class InValidSingeTon : SingletonComponent<DummySingleTon> { }
  }

  
}