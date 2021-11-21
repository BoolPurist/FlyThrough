using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NiceGraphicLibrary;
using NiceGraphicLibrary.Component;
using NiceGraphicLibrary.Component.Sound;

namespace NiceGraphicLibrary.Tests.Runtime
{
  [TestFixture]
  public class TestAudioClipManager
  {
    private GameObject _objectToTestWith;
    private AudioClip[] _testClips;

    [OneTimeSetUp]
    public void ConstructTestClips()
    {
      const int constSample = 1000;

      _testClips = new AudioClip[3];
      _testClips[0] = AudioClip.Create("test sound a", 1, 1, constSample, false);
      _testClips[1] = AudioClip.Create("test sound b", 1, 1, constSample, false);
      _testClips[2] = AudioClip.Create("test sound c", 1, 1, constSample, false);
    }

    [SetUp]
    public  void ConstructNeededGameObject()
    {
      _objectToTestWith = new GameObject("Audio Clip Manager");
      _objectToTestWith.AddComponent<AudioSource>();
      _objectToTestWith.AddComponent<SoundClipManager>();

      var audioSource = _objectToTestWith.GetComponent<AudioSource>();
      audioSource.playOnAwake = false;
      audioSource.Stop();
    }

    [Test]
    public void TestChangeToAudioClip()
    {
      // Set up
      var clipManager = _objectToTestWith.GetComponent<SoundClipManager>();
      var audioSource = _objectToTestWith.GetComponent<AudioSource>();

      var dictionary = new Dictionary<string, AudioClip>
      {
        { "sound a", _testClips[0] },
        { "sound b", _testClips[1] },
        { "sound c", _testClips[2] }
      };

      clipManager.UnityTest_ReplaceDictionary(dictionary);

      // Act
      clipManager.ChangeToAudioClip("sound a", true);

      // Assert
      Assert.IsTrue(audioSource.isPlaying);
      Assert.AreEqual(_testClips[0], audioSource.clip);

      // Act
      audioSource.Stop();
      clipManager.ChangeToAudioClip("sound b", true);

      // Assert
      Assert.IsTrue(audioSource.isPlaying);
      Assert.AreEqual(_testClips[1], audioSource.clip);

    }

    [Test]
    public void Test_ForInvalidName()
    {
      const string INVALID_SOUND_NAME = "no sound";

      // Set up
      var clipManager = _objectToTestWith.GetComponent<SoundClipManager>();
      var audioSource = _objectToTestWith.GetComponent<AudioSource>();


      var dictionary = new Dictionary<string, AudioClip>
      {
        { "sound a", _testClips[0] }
      };

      clipManager.UnityTest_ReplaceDictionary(dictionary);

      clipManager.ChangeToAudioClip(INVALID_SOUND_NAME, true);

      // Assert
      Assert.IsFalse(audioSource.isPlaying);
      Assert.AreEqual(null, audioSource.clip);

      LogAssert.Expect(LogType.Warning, $"For nameOfAudioClip = [{INVALID_SOUND_NAME}] was no entry found !");
    }

    [TearDown]
    public void DestroyNeededGameObject()
    {
      GameObject.Destroy(_objectToTestWith);
    }
  }
}
