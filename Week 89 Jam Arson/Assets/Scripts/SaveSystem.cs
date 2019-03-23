using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// The main class for the Save/Load System.
/// </summary>
public class SaveSystem
{
    // Game Save folder path
    public static readonly string saveFolder = Application.dataPath + "/GameSaves/";
    public static readonly string saveFilePrefix = "Save_";
    public static readonly string saveFileExtension = ".sav";

    /// <summary>
    /// Check if the Save Directory exist else create one.
    /// </summary>
    private static void CheckAndCreateSaveDirectory()
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    /// <summary>
    /// Saves the save object passed through as argument
    /// </summary>
    /// <param name="saveObject"> the save object containing the save data.</param>
    /// <returns></returns>
    public static bool Save(SaveObject saveObject)
    {
        CheckAndCreateSaveDirectory();
        int lastSaveFileIndex = 0;
        DirectoryInfo directoryInfo = new DirectoryInfo(saveFolder);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        foreach (FileInfo file in fileInfos)
        {
            if (file.Name.EndsWith(saveFileExtension))
            {
                int tempIndex = 0;
                if (System.Int32.TryParse((file.Name.Substring(saveFilePrefix.Length, (file.Name.Length - saveFilePrefix.Length - saveFileExtension.Length))), out tempIndex))
                {
                    if (tempIndex > lastSaveFileIndex)
                    {
                        lastSaveFileIndex = tempIndex;
                    }
                }
            }
        }

        string serializedString = JsonUtility.ToJson(saveObject);
        if (string.IsNullOrEmpty(serializedString))
        {
            return false;
        }
        File.WriteAllText(saveFolder + saveFilePrefix + ++lastSaveFileIndex + saveFileExtension, serializedString);
        return true;
    }

    /// <summary>
    /// Loads the last saved data to the out parameter loadedSaveObject
    /// </summary>
    /// <param name="loadedSaveObject">The Loaded Save Data</param>
    /// <returns></returns>
    public static bool Load( out SaveObject loadedSaveObject)
    {
        CheckAndCreateSaveDirectory();
        DirectoryInfo directoryInfo = new DirectoryInfo(saveFolder);
        FileInfo[] fileInfos = directoryInfo.GetFiles("*" + saveFileExtension);
        FileInfo latestFile = null;
        foreach(FileInfo file in fileInfos)
        {
            if (file.Name.EndsWith(saveFileExtension))
            {
                if (latestFile == null)
                {
                    latestFile = file;
                }
                else
                {
                    if (file.LastWriteTime > latestFile.LastWriteTime)
                    {
                        latestFile = file;
                    }
                }
            }
        }

        string serializedString;
        if(latestFile != null)
        {
            serializedString = File.ReadAllText(saveFolder + latestFile.Name);
            if (!string.IsNullOrEmpty(serializedString))
            {
                loadedSaveObject = JsonUtility.FromJson<SaveObject>(serializedString);
                return (loadedSaveObject != null);
            }
        }

        loadedSaveObject = null;

        return false;
    }
}
