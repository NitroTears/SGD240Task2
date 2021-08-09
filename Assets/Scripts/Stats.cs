using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the dance stats of a character.
/// 
/// TODO: add a level up event
/// 
///     Nothing, but this class may be a good place to put some helper fuctions when dealing with xp to level conversion and the like.
/// </summary>
public class Stats : MonoBehaviour
{
    //[HideInInspector]
    public int level;
    [HideInInspector]
    public int xp;
    [HideInInspector]
    public int style, luck, rhythm;

    public bool isPlayer;

    private void Awake()
    {
        // assign initial stats
        isPlayer = GetComponent<Player>();
        Debug.Log("isPlayer = " + isPlayer);
        StatsGenerator.InitialStats(this, isPlayer);
        Debug.Log("Lv: " + level + " |  Style: " + style + " Luck: " + luck + " Rhythm: " + rhythm);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerGainXP += addXpToTotal;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerGainXP -= addXpToTotal;
    }
    public void addXpToTotal(int xp)
    {
        if (isPlayer) // XP and Levels are for the Player only.
        {
            Debug.Log($"{this.xp} + {xp} = {this.xp + xp}");
            this.xp += xp; // add gained xp to total.
            StatsGenerator.levelThresBlock.TryGetValue(level, out int xpThreshold); // get level requirement for next level.
            if (this.xp > xpThreshold)
            {
                level += 1;
                Debug.Log("Level Up!!! Level :" + level);
                StatsGenerator.statsBlock.TryGetValue(level, out List<int> NewStats);
                style = NewStats[0];
                luck = NewStats[1];
                rhythm = NewStats[2];
                GameEvents.PlayerLevelUp(this.level);
            }
        }
    }
}
