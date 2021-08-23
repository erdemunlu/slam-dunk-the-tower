using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment
{
    private static GameEnvironment instance;

    public static GameEnvironment singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new GameEnvironment();
                PlayerPrefs.GetInt("Level", 1);
                PlayerPrefs.GetInt("HighestLevel", 1);
            }
            return instance;
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("Level", 1);
    }
    public void LevelUp()
    {
        PlayerPrefs.SetInt("Level", GetCurrentLevel() + 1);
    }

    public int GetHighestLevel()
    {
        return PlayerPrefs.GetInt("HighestLevel",1);
    }
    public void SetHighestLevel(int level)
    {
        PlayerPrefs.SetInt("HighestLevel", level);
    }

    public void ResetHighestLevel()
    {
        PlayerPrefs.SetInt("HighestLevel", 1);
    }

    
}
