using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlyThrough
{
  public class CreditEntryBuilder : MonoBehaviour
  {
    [SerializeField]
    private GameObject CreditHeadLineBlueprint;

    [SerializeField]
    private GameObject CreditEntryBluePrint;
    [SerializeField, Min(0)]
    private float SpaceBetweenChapters = 0f;
    [SerializeField]
    private GameObject Container;


    [SerializeField]
    private CreditRows RowsToBuild;

    private void Start()
    {
      RemoveChildren();
      var mainVerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
      mainVerticalLayoutGroup.spacing = SpaceBetweenChapters;

      foreach (Chapter chapterToBuild in RowsToBuild.GetChapters())
      {
        string chapterTitle = chapterToBuild.TitleOfEntries;
        GameObject container = Instantiate<GameObject>(Container);        

        container.transform.SetParent(transform);
        
        GameObject newCreditHeadline = Instantiate<GameObject>(CreditHeadLineBlueprint, transform);
        newCreditHeadline.GetComponent<TextMeshProUGUI>().text = chapterTitle;
        newCreditHeadline.transform.SetParent(container.transform);

        foreach (Contribution entry in chapterToBuild.GetEntries())
        {
          GameObject newEntry = Instantiate<GameObject>(CreditEntryBluePrint, transform);
          string contributionLine = $"\"{entry.GetAssetName()}\" by {entry.GetAuthorName()}\n" +
            $"licensed ({entry.GetAuthorLicence()}): {entry.GetAuthorLink()}";
          newEntry.GetComponent<TextMeshProUGUI>().text = contributionLine;
          newEntry.transform.SetParent(container.transform);
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