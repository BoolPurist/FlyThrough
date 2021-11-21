using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Abstractions;


namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Utility functions to save game data permanently. All operations, like read and write are done relative to the path Appplication.persistentDataPath.
  /// </summary>
  public static class PersistentDataUtility
  {
    private static IFileSystem _fileSystem = new FileSystem();

    /// <summary>
    /// Changes the file system to save game data.
    /// </summary>
    /// <param name="fileSystem">
    /// If null the inner file system is not changed
    /// </param>
    /// <remarks>
    /// Useful to inject fake file system for unit testing.
    /// </remarks>
    public static void SetFileSystem(IFileSystem fileSystem)
    {
      if (fileSystem != null)
      {
        _fileSystem = fileSystem;
      }
    }

    /// <summary>
    /// Saves text content into a file. If the file does not exists then a new file with the given content is created.
    /// </summary>
    /// <param name="path">
    /// Path where to store the content
    /// If empty or null nothing happens
    /// </param>
    /// <param name="content">
    /// Content as text to save.
    /// </param>
    public static void Write(in string path, in string content)
      => ManipulateFile(path, content, false);

    /// <summary>
    /// Appends text content to the text content of a file. If the file does not exists then a new file with the given content is created.
    /// </summary>
    /// <param name="path">
    /// Path where to store the content
    /// If empty or null nothing happens
    /// </param>
    /// <param name="content">
    /// Content as text to append.
    /// </param>
    public static void Append(in string path, in string content)
      => ManipulateFile(path, content, true);

    /// <summary>
    /// Appends text content to the text content of a file in a new line. If the file does not exists then a new file with the given content is created.
    /// </summary>
    /// <param name="path">
    /// Path where to store the content
    /// If empty or null nothing happens
    /// </param>
    /// <param name="content">
    /// Content as text to append in a new line.
    /// </param>
    public static void AppendWithNewLine(in string path, in string content)
      => ManipulateFile(path, $"{Environment.NewLine}{content}", true);

    /// <summary>
    /// Internal routine to write data to a file.
    /// </summary>
    /// <param name="isAppend">
    /// If true the text content is added to the existing content instead of overwriting it.
    /// If empty or null nothing happens
    /// </param>
    private static void ManipulateFile(in string path, in string content, bool isAppend)
    {
      if (string.IsNullOrWhiteSpace(path)) return;
      var paths = new PersistentPathRecord(path);
    
      _fileSystem.Directory.CreateDirectory(paths.FullPathToFile);

      // If file does not exits then change appending to writing.
      isAppend = isAppend && !File.Exists(paths.FullPath) ? !isAppend : isAppend;

      if (isAppend)
      {
        _fileSystem.File.AppendAllText(paths.FullPath, content);
      }
      else
      {
        _fileSystem.File.WriteAllText(paths.FullPath, content);
      }
    }

    /// <summary>
    /// Creates file without any content. If the files already exits it deletes its content
    /// </summary>
    /// <param name="path">
    /// Where to create the empty file.
    /// If empty or null nothing happens
    /// </param>
    public static void CreateEmptyFile(in string path)
      => ManipulateFile(path, "", false);

    /// <summary>
    /// Creates a directory.
    /// </summary>
    /// <param name="path">
    /// Where to Creates a directory.
    /// </param>
    public static void CreateDirectory(in string path)
    {
      if (string.IsNullOrWhiteSpace(path)) return;
      var paths = new PersistentPathRecord(path);
      _fileSystem.Directory.CreateDirectory(paths.FullPath);
    }


    /// <param name="path">
    /// Where read the content from.
    /// If null or empty null is returned.
    /// </param>
    /// <returns>
    /// Returns content of a file.
    /// Returns null if this file does not exits.
    /// </returns>
    public static string ReadFrom(in string path)
    {
      if (string.IsNullOrWhiteSpace(path)) return null;
      string fullPath = $"{Application.persistentDataPath}/{path}";

      if (!File.Exists(fullPath))
      {
        return null;
      }
      
      using (StreamReader fileReader = _fileSystem.File.OpenText(fullPath))
      {
        return fileReader.ReadToEnd();
      }

    }

    /// <summary>
    /// Checks if  a file exits.
    /// </summary>
    /// <param name="fullPath">
    /// Where to check
    /// </param>
    /// <returns>
    /// If true the file located at the given path
    /// Returns false if this file does not exits.
    /// </returns>
    public static bool FileExits(in string fullPath)
    {
      if (string.IsNullOrWhiteSpace(fullPath)) return false;
      var paths = new PersistentPathRecord(fullPath);
      return _fileSystem.File.Exists(paths.FullPath);
    }

    /// <summary>
    /// Checks if a directory exits.
    /// </summary>
    /// <param name="fullPath">
    /// Where to check
    /// </param>
    /// <returns>
    /// If true the file located at the given path
    /// Returns true if this file does not exits.
    /// </returns>
    public static bool DirectoryExits(in string fullPath)
    {
      if (string.IsNullOrWhiteSpace(fullPath)) return false;
      var paths = new PersistentPathRecord(fullPath);
      return _fileSystem.Directory.Exists(paths.FullPath);
    }

    /// <summary>
    /// Deletes a file. if the file does not exits nothing will be deleted.
    /// </summary>
    /// <param name="path">
    /// If null or empty nothing will be deleted.
    /// </param>
    public static void DeleteFile (in string path)
    {
      if (string.IsNullOrWhiteSpace(path)) return;
      var paths = new PersistentPathRecord(path);

      if (FileExits(paths.FullPath))
      {
        _fileSystem.File.Delete(paths.FullPath);

      }
    }

    /// <summary>
    /// Deletes a directory. if the directory does not exits nothing will be deleted.
    /// </summary>
    /// <param name="path">
    /// If null or empty nothing will be deleted.
    /// </param>
    public static void DeleteDirectory(in string path)
    {
      if (string.IsNullOrWhiteSpace(path)) return;
      var paths = new PersistentPathRecord(path);
      if (_fileSystem.Directory.Exists(paths.FullPath))
      {
        _fileSystem.Directory.Delete(paths.FullPath);
      }
    }

    /// <summary>
    /// Uses / for 2.0 net standard to combine.
    /// </summary>
    private static string CombinePaths(params string[] paths)
    {
      string result = paths[0];
      for (int i = 1; i < paths.Length; i++)
      {
        result += $"/{paths[i]}";
      }
      return result;
    }

    /// <summary>
    /// Used to provide relevant parts of given path relative to Application.persistentDataPath
    /// </summary>
    private class PersistentPathRecord
    {
      public string FileName { get; private set; }
      public string FullPath { get; private set; }
      public string FullPathToFile { get; private set; }
      public string RelativePathToFile { get; private set; }

      public string RelativeFullPath { get; private set; }

      public PersistentPathRecord(string fullPath)
      {
        RelativeFullPath = fullPath;
        FileName = _fileSystem.Path.GetFileName(RelativeFullPath);
        RelativePathToFile = _fileSystem.Path.GetDirectoryName(RelativeFullPath);

        FullPathToFile = CombinePaths(Application.persistentDataPath, RelativePathToFile);
        FullPath = CombinePaths(FullPathToFile, FileName);
      }
    }
    
    
  } 
}