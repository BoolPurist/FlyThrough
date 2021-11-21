using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary.Component.Movement;
using NiceGraphicLibrary.Tests.Editor.Stubs;

namespace NiceGraphicLibrary.Tests.Runtime
{
  [TestFixture]
  public class Test_FollowMouse
  {
    private GameObject _objectToBeMoved;
    private FakeGameInputProvider _fakeInputProvider;

    [SetUp]
    public void SetUp()
    {
      _objectToBeMoved = new GameObject(nameof(_objectToBeMoved));
      _fakeInputProvider = new FakeGameInputProvider();
      _objectToBeMoved.AddComponent<FollowMouse>().SetKeyButtonProvider(_fakeInputProvider);      
    }

    [UnityTest]
    public IEnumerator Test_IfObjectFollowsMouse()
    {
      Vector3 newMousePosition = new Vector3(200f, 200f, 100f);
      SetUpNextFrame();
      yield return new WaitForEndOfFrame();
      yield return new WaitForEndOfFrame();
      AssertOneMousePosition();

      newMousePosition = new Vector3(-100, 0f, 100f);
      SetUpNextFrame();
      yield return new WaitForEndOfFrame();
      yield return new WaitForEndOfFrame();
      AssertOneMousePosition();

      newMousePosition = new Vector3(-100, 0f, 100f);
      SetUpNextFrame();
      yield return new WaitForEndOfFrame();
      yield return new WaitForEndOfFrame();
      AssertOneMousePosition();

      void SetUpNextFrame()
      {
        _fakeInputProvider.FakeMousePosition = new Vector2(newMousePosition.x, newMousePosition.y);
        _objectToBeMoved.transform.position = new Vector3(
          _objectToBeMoved.transform.position.x, 
          _objectToBeMoved.transform.position.y, 
          newMousePosition.z
          );
      }

      void AssertOneMousePosition()
      {               
        Assert.AreEqual(
          newMousePosition,
          _objectToBeMoved.transform.position,
          $"Object is not following the mouse"
          );
      }
    }
  }
}