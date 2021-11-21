using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Component.Sound
{
  /// <summary>
  /// Component to swap different audio clips for an audio source during play time.
  /// Audio clips with their name id is given by fields in the inspector.
  /// </summary>
  [RequireComponent(typeof(AudioSource))]
  public class SoundClipManager : MonoBehaviour
  {
    /// <summary>
    /// Class to type an audio clip with a certain name to find it later during play time
    /// </summary>
    [System.Serializable]
    public class ClipEntry
    {
      /// <summary>
      /// Name as parameter in the method <see cref="ChangeToAudioClip"/> to search for the specific audio clip in 
      /// </summary>     
      public string _Name;
      /// <summary>
      /// A clip which can be changed to during the play time with the method <see cref="ChangeToAudioClip"/> 
      /// </summary>
      public AudioClip _Clip;
    }

    [SerializeField]
    [Tooltip("List of clips which can be changed to during play time. The name is key to provide as argument for the public function ChangeToAudioClip")]
    private List<ClipEntry> _ClipEntries = new List<ClipEntry>();

    private ListToDictionaryConverter<string, AudioClip, ClipEntry> _entryToDictionaryConverter = 
      new ListToDictionaryConverter<string, AudioClip, ClipEntry>(nameof(ClipEntry._Name), nameof(ClipEntry._Clip));
    private Dictionary<string, AudioClip> _DictionaryForEntries = new Dictionary<string, AudioClip>();        
    private AudioSource _MusicSource;

    public int CountAudioClip => _DictionaryForEntries.Count;

    /// <summary>
    /// Changes the clip audio of attached source audio and plays this clip now.
    /// </summary>
    /// <param name="nameOfAudioClip">
    /// Name to use for searching for the clip to swap in
    /// </param>
    /// <param name="playOnLoop">
    /// If true sound will be played in a loop
    /// </param>
    /// <remarks>
    /// If nameOfAudioClip is null, empty or no clip was stored under the given name [nameOfAudioClip], nothing happens
    /// </remarks>
    public void ChangeToAudioClip(string nameOfAudioClip, bool playOnLoop = false)
    {

      if (_DictionaryForEntries.ContainsKey(nameOfAudioClip))
      {
        ChangeClip(_DictionaryForEntries[nameOfAudioClip], playOnLoop);
      }
      else
      {
        Debug.LogWarning($"For {nameof(nameOfAudioClip)} = [{nameOfAudioClip}] was no entry found !");
      }

    }

    // Stops player and changes clip and starts this new clip.
    private void ChangeClip(AudioClip newClip, bool playOnLoop)
    {
      _MusicSource.Stop();
      _MusicSource.loop = playOnLoop;
      _MusicSource.clip = newClip;
      _MusicSource.Play();
    }

    private void Awake() => _MusicSource = GetComponent<AudioSource>();

    private void OnValidate()
      => _DictionaryForEntries = _entryToDictionaryConverter.CreateDictionaryFrom(_ClipEntries);

#if UNITY_INCLUDE_TESTS

    public void UnityTest_ReplaceDictionary(Dictionary<string, AudioClip> dict)
    {
      _DictionaryForEntries.Clear();
      foreach (KeyValuePair<string, AudioClip> keyPair in dict)
      {
        _DictionaryForEntries.Add(keyPair.Key, keyPair.Value);
      }
    }

#endif

  }
}