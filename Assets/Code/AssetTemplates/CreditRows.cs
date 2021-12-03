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
    private List<Contribution> Contributors;


    public string TitleOfEntries => Title;

    public IEnumerable<Contribution> GetEntries()
    {
      foreach (Contribution entry in Contributors)
      {
        yield return entry;
      }
    }
  }

  [System.Serializable]
  public class Contribution
  {
    [SerializeField]
    private string AuthoName;
    [SerializeField]
    private string AssetName;
    [SerializeField]
    private string Link;
    [SerializeField]
    private string Licence;

    public string GetAuthorName() => AuthoName;
    public string GetAssetName() => AssetName;
    public string GetAuthorLink() => Link;
    public string GetAuthorLicence() => Licence;
  }

}

