using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler onExperienceChanged;
    public event EventHandler onLevelChanged;

    private int level;
    private int experience;
    private int maxLevel = 99;

    private static readonly int[] experiencePerLevel = new int[100];

    public LevelSystem()
    {
        level = 0;
        experience = 0;
    }

    public void ExperienceIncreaseAmount()
    {
        int experienceIncreaseAmount = 0;
        for (float levelCycle = 1f; levelCycle <= maxLevel; levelCycle++)
        {
            experienceIncreaseAmount += (int)Mathf.Floor((levelCycle + 300) * Mathf.Pow(2, levelCycle / 7));
            experiencePerLevel[(int)levelCycle-1] = experienceIncreaseAmount / 4;
        }
    }

    public void AddExperience(int amount)
    {
        if (!MaxLevel(level))
        {
            experience += amount;
            Debug.Log("°æÇèÄ¡");
            while(!MaxLevel() && experience >= GetExperienceToNextLevel(level))
            {
                experience -= GetExperienceToNextLevel(level);
                level++;
                if (onLevelChanged != null)
                {
                    onLevelChanged(this, EventArgs.Empty);
                }
            }

            if(onExperienceChanged != null)
            {
                onExperienceChanged(this, EventArgs.Empty);
            }
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        if(MaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)experience / GetExperienceToNextLevel(level);
        }
    }

    public int GetExperienceToNextLevel(int level)
    {
        if(level < maxLevel)
        {
            return experiencePerLevel[level];
        }
        else
        {
            Debug.LogError("Level invald: " + level);
            return 1000;
        }
    }

    public bool MaxLevel()
    {
        return MaxLevel(level);
    }

    public bool MaxLevel(int level)
    {
        return level == maxLevel;
    }
}
