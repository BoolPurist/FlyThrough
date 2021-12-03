using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace FlyThrough
{
  
  public class BuildCreditsWindow : EditorWindow
  {
    private const string MARKDONW_FILE_NAME = "CREDITS.md";
    private const string WINDOW_TITLE = "Snap placing";

    private const float SPACE_SIZE = 20f;

    private bool _isWriting = false;

    private CreditRows _creditDataToWorkWith;

    [MenuItem("Integration/Credits")]
    private static void CreateWindow()
    {
      var window = GetWindow<BuildCreditsWindow>();
      window.titleContent = new GUIContent(WINDOW_TITLE);
      window.minSize = new Vector2(400f, 250f);

      window.Show();
    }

    private void OnGUI()
    {
      GUILayout.Label("Credits");
      _creditDataToWorkWith = (CreditRows)EditorGUILayout.ObjectField(_creditDataToWorkWith, typeof(CreditRows), false);

      

      if (_creditDataToWorkWith != null)
      {
        if (_isWriting)
        {
          GUILayout.Space(SPACE_SIZE);
          EditorGUILayout.HelpBox($"Creating File ({MARKDONW_FILE_NAME}) at {GetFullTargetPath()}", MessageType.Info);
        }
        else
        {
          DrawCreaetButton();
        }

        
      }
    }

    private void DrawCreaetButton()
    {
      GUILayout.Space(SPACE_SIZE);
      if (GUILayout.Button("Create Markdown file from credits"))
      {
        _isWriting = true;


        string fullTargetPath = GetFullTargetPath();

        File.WriteAllText(fullTargetPath, CreateContentOfMarkdonwFile());
        
        _isWriting = false;
      }
    }

    private string GetFullTargetPath()
    {
      string asssetPath = Path.Combine(Application.dataPath.Replace('/', Path.DirectorySeparatorChar), "..");
      string targetFolder = Path.GetFullPath(asssetPath);
      return Path.Combine(targetFolder, MARKDONW_FILE_NAME);
    }

    private  string CreateContentOfMarkdonwFile()
    {
      var contentBuilder = new StringBuilder();

      contentBuilder.AppendLine("# Credits");
      contentBuilder.AppendLine();

      foreach (Chapter chapter in _creditDataToWorkWith.GetChapters())
      {
        const string SEPERATOR = "---";
        contentBuilder.AppendLine($"## {chapter.TitleOfEntries}\n");
        contentBuilder.AppendLine(SEPERATOR);

        foreach (Contribution entry in chapter.GetEntries())
        {
          contentBuilder.AppendLine($"Asset Name: **{entry.GetAssetName()}**\n");
          contentBuilder.AppendLine($"Author: **{entry.GetAuthorName()}**\n");
          contentBuilder.AppendLine($"License: **{entry.GetAuthorLicence()}**\n");
          contentBuilder.AppendLine($"Link: **{entry.GetAuthorLink()}**\n");
          contentBuilder.AppendLine(SEPERATOR);
        }
      }

      return contentBuilder.ToString();
    }


  }
}
