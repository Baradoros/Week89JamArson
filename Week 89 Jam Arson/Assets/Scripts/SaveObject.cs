using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class holding all the essential save data.
/// </summary>
public class SaveObject
{
    public int level;
    public int score;

    public SaveObject(int levelParam, int scoreParam)
    {
        level = levelParam;
        score = scoreParam;
    }
}
