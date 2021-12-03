using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FlyThrough
{
  public class CreditEntryBuilder : MonoBehaviour
  {
    [SerializeField]
    private GameObject CreditHeadLineBlueprint;

    [SerializeField]
    private GameObject CreditEntryBluePrint;


    [SerializeField]
    private CreditRows RowsToBuild;

    private void Start()
    {
      RemoveChildren();

      foreach (Chapter chapterToBuild in RowsToBuild.GetChapters())
      {
        GameObject newCreditHeadline = Instantiate<GameObject>(CreditHeadLineBlueprint, transform);
        newCreditHeadline.GetComponent<TextMeshProUGUI>().text = chapterToBuild.TitleOfEntries;

        foreach (string entry in chapterToBuild.GetEntries())
        {
          GameObject newEntry = Instantiate<GameObject>(CreditEntryBluePrint, transform);
          newEntry.GetComponent<TextMeshProUGUI>().text = entry;
        }
      }
    }

    private void RemoveChildren()
    {
      foreach (Transform child in transform)
      {
        Destroy(child.gameObject);
      }
    }


  }

}