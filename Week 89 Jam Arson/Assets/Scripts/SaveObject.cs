using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct LevelData
{
    public int level;
    public int score;
    public bool isUnlocked;

    public LevelData(int levelParam, int scoreParam, bool isUnlockedParam)
    {
        level = levelParam;
        score = scoreParam;
        isUnlocked = isUnlockedParam;
    }
};

/// <summary>
/// A class holding all the essential save data.
/// </summary>
//[System.Serializable]
public class SaveObject
{
    public LevelData[] LevelDataArray = new LevelData[5];
}
