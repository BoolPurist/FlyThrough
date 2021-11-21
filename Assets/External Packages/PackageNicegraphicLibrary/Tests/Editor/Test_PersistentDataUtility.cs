using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

using NiceGraphicLibrary.Utility;

namespace NiceGraphicLibrary.Tests.Editor
{
  [TestFixture]
  public class Test_PersistentDataUtility
  {
    private IFileSystem _fakeFileSystem;
    private string _persistenPath;

    [SetUp]
    public void SetUpSystem()
    {
      _persistenPath = Application.persistentDataPath;
      _fakeFileSystem = new MockFileSystem();
      PersistentDataUtility.SetFileSystem(_fakeFileSystem);
    }

    [TearDown]
    public void TearDownSystem()
    {
      PersistentDataUtility.SetFileSystem(new FileSystem());

    }

    [TestCaseSource(nameof(TestCases_Write))]
    public void Test_Write(string testPath, string expectedContent)
    {
      string actualContentOfFile = "";
      TestDelegate acting = () => PersistentDataUtility.Write(testPath, expectedContent); ;
      Assert.DoesNotThrow(acting);

      actualContentOfFile = GetActualContentOfMockFile(testPath);

      Assert.AreEqual(expectedContent, actualContentOfFile, $"Wrong content was written into the file");
    }

    [Test]
    public void Test_Read_WithNotFoundFile()
    {
      string canNotFindMePath = "canFindMePath";

      string actualContent = "";
      TestDelegate acting = () => actualContent = PersistentDataUtility.ReadFrom(canNotFindMePath); ;
      Assert.DoesNotThrow(acting, $"Should cause an exception for not found file.");

      Assert.Null(actualContent, $"Null should be returned if the file is not found !");
    }

    [Test]
    public void Test_Read()
    {
      string pathToWrittenFile = "test.txt";
      string expectedContent = "some content";

      string fullPath = GetPresistenFullPath(pathToWrittenFile);
      _fakeFileSystem.Directory.CreateDirectory(Application.persistentDataPath);
      _fakeFileSystem.File.WriteAllText(fullPath, expectedContent);

      string actualContent = "";

      TestDelegate acting = () => actualContent = PersistentDataUtility.ReadFrom(pathToWrittenFile); ;
      Assert.DoesNotThrow(acting, $"Should cause an exception for existing file.");

      Assert.AreEqual(expectedContent, actualContent, $"Wrong content was read.");
    }

    [Test]
    public void Test_Append()
    {
      var pathToWrittenFile = "test.txt";
      var previousContent = "previous line";
      var lines = new string[] { "1. line", "2. line", "3. line" };

      string fullPath = GetPresistenFullPath(pathToWrittenFile);
      _fakeFileSystem.Directory.CreateDirectory(Application.persistentDataPath);

      _fakeFileSystem.File.WriteAllText(fullPath, previousContent);
      

      foreach (string oneLine in lines)
      {
        TestDelegate acting = () => PersistentDataUtility.Append(pathToWrittenFile, oneLine);
        Assert.DoesNotThrow(acting, $"Should cause an exception for existing file.");
      }

      string expectedContent = previousContent;
      expectedContent += lines.Aggregate("", (acc, line) => acc + line);
      string actualContent = _fakeFileSystem.File.ReadAllText(fullPath);
      Assert.AreEqual(expectedContent, actualContent, $"Content was not appended correctly.");

    }

    [Test]
    public void Test_CreateDirectory()
    {
      var pathToTest = "bin/folder";
      PersistentDataUtility.CreateDirectory(pathToTest);
      var fullPathToTest = GetPresistenFullPath(pathToTest);

      Assert.IsTrue(_fakeFileSystem.Directory.Exists(fullPathToTest), $"[{fullPathToTest}] as directory was created !");
    }

    [Test]
    public void Test_CreateEmptyFile()
    {
      var pathToTest = "bin/file.txt";
      PersistentDataUtility.CreateEmptyFile(pathToTest);
      var fullPathToTest = GetPresistenFullPath(pathToTest);

      Assert.IsTrue(_fakeFileSystem.File.Exists(fullPathToTest));
      Assert.IsEmpty(_fakeFileSystem.File.ReadAllText(fullPathToTest));
    }

    [Test]
    public void Test_FileExists()
    {
      var pathToTest = "bin/folder.txt";
      
      Assert.IsFalse(PersistentDataUtility.FileExits(pathToTest), $"{pathToTest} as path does not exits yet");
      var fullPathToTest = GetPresistenFullPath(pathToTest);

      _fakeFileSystem.Directory.CreateDirectory(_fakeFileSystem.Path.GetDirectoryName(fullPathToTest));
      using (var file = _fakeFileSystem.File.Create(fullPathToTest)) { }

      Assert.IsTrue(PersistentDataUtility.FileExits(pathToTest), $"{pathToTest} as path should now exits.");

    }

    [Test]
    public void Test_DirectoryExists()
    {
      var pathToTest = "bin/folder";

      Assert.IsFalse(PersistentDataUtility.DirectoryExits(pathToTest), $"{pathToTest} as path does not exits yet");
      var fullPathToTest = GetPresistenFullPath(pathToTest);

      _fakeFileSystem.Directory.CreateDirectory(fullPathToTest);     
      Assert.IsTrue(PersistentDataUtility.DirectoryExits(pathToTest), $"{pathToTest} as path should now exits.");
    }

    [Test]
    public void Test_DirectoryDelete()
    {
      var pathToTest = "bin/folder";

      var fullPathToTest = GetPresistenFullPath(pathToTest);
      _fakeFileSystem.Directory.CreateDirectory(fullPathToTest);

      PersistentDataUtility.DeleteDirectory(pathToTest);
      Assert.IsFalse(PersistentDataUtility.DirectoryExits(pathToTest), $"{pathToTest} as folder was deleted.");
    }

    private string GetActualContentOfMockFile(string path)
      => _fakeFileSystem.File.ReadAllText(GetPresistenFullPath(path));

    private string GetPresistenFullPath(string path) => $"{_persistenPath}/{path}";
   

    private static object[] TestCases_Write
      => new object[]
      {
        new object[] 
        {
          "test.txt",
          "Hello from inside"
        },
        new object[]
        {
          "bin/test",
          "Hello from inside asdfs"
        }
      };

  }
}