using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyThrough
{
  [CreateAssetMenu(fileName = "new credit rows", menuName = "Data/CreditsRows")]
  public class CreditRows : ScriptableObject
  {
    [SerializeField]
    private List<Chapter> Chapters;

    public IEnumerable<Chapter> GetChapters()
    {
      foreach (Chapter chapter in Chapters)
      {
        yield return chapter;
      }
    }
  }

  [System.Serializable]
  public class Chapter
  {
    [SerializeField]
    private string Title;
    [SerializeField]
    private List<string> Contributors;


    public string TitleOfEntries => Title;

    public IEnumerable<string> GetEntries()
    {
      foreach (string entry in Contributors)
      {
        yield return entry;
      }
    }
  }

}

