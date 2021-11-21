using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using TMPro;

using NiceGraphicLibrary.Component.GUI;

namespace NiceGraphicLibrary.Tests.Runtime
{
  [TestFixture]  
  public class Test_TextTemplate
  {
    private const int DEFAULT_HP = 100;
    private const int DEFAULT_KILLER_COUNT = 2;
    private const string DEFAULT_PLAYER_NAME = "Pattern";

    private GameObject _objectToTestOn;
    private TextMeshProUGUI _textMeshProComponent;
    private TextTemplate _componentToTestOn;

    [SetUp]
    public void SetUp()
    {
      _objectToTestOn = GameObject.Instantiate<GameObject>(
        Resources.Load<GameObject>("NiceGraphicLibrary/ObjectToTestForTextTemplate")
        );
      _textMeshProComponent = _objectToTestOn.GetComponentInChildren<TextMeshProUGUI>();
      _componentToTestOn = _objectToTestOn.GetComponentInChildren<TextTemplate>();
    }

    [TearDown]
    public void TearDown()
    {
      GameObject.Destroy(_objectToTestOn);
    }

    [UnityTest]
    public IEnumerator Test_PreviewText()
    {
      _componentToTestOn.Start();
      string previewTextFromTextTemplate = _textMeshProComponent.text;
      Assert.AreEqual(
        GetDefaultExpectedText(), 
        previewTextFromTextTemplate,
        $"At start of game the text is equal to the default text block."
        );

      yield break;
    }

    private string GetExepetedText(string playerName, int hp, int killerCount)
      => $"Player {playerName} has HP {hp} and was killed {killerCount}.";

    private string GetDefaultExpectedText()
      => GetExepetedText(DEFAULT_PLAYER_NAME, DEFAULT_HP, DEFAULT_KILLER_COUNT);

    [UnityTest]
    public IEnumerator Test_InsertAtValue()
    {
      const int EXPECTED_HP = 200;
      _componentToTestOn.InsertValueWithName("HP", EXPECTED_HP.ToString());
      Assert.AreEqual(
        GetExepetedText(DEFAULT_PLAYER_NAME, EXPECTED_HP, DEFAULT_KILLER_COUNT),
        _textMeshProComponent.text,
        $"Text is not adjusted by the text template component."
        );

      yield break;

    }
  }
}