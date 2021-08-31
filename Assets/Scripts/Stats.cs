using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the dance stats of a character.
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
        // Debug.Log("isPlayer = " + isPlayer);
        StatsGenerator.InitialStats(this, isPlayer);
        // Debug.Log("Lv: " + level + " |  Style: " + style + " Luck: " + luck + " Rhythm: " + rhythm);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerGainXP += addXpToTotal;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerGainXP -= addXpToTotal;
    }
    /// <summary>
    /// Addeds the given xp to the total xp of this stats object.
    /// Will increase the level if the xp threshold is met.
    /// </summary>
    public void addXpToTotal(int xp)
    {
        if (isPlayer) // XP and Levels are for the Player only.
        {
            // Debug.Log($"{this.xp} + {xp} = {this.xp + xp}");
            this.xp += xp; // add gained xp to total.
            StatsGenerator.levelThresBlock.TryGetValue(level, out int xpThreshold); // get level requirement for next level.
            var UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            if (UIManager != null)
            {
                Debug.Log("UI Manager Found");
                UIManager.UpdateUI(this.xp, xpThreshold, this.level);
            }
            // Check if player can level up
            if (this.xp > xpThreshold)
            {
                level += 1;
                Debug.Log("Level Up!!! Level :" + level);
                StatsGenerator.statsBlock.TryGetValue(level, out List<int> NewStats);
                style = NewStats[0];
                luck = NewStats[1];
                rhythm = NewStats[2];
                GameEvents.PlayerLevelUp(this.level);
                if (UIManager != null)
                {
                    StatsGenerator.levelThresBlock.TryGetValue(level, out int newXPThreshold); // get level requirement for next level.
                    UIManager.UpdateUI(this.xp, newXPThreshold, this.level);
                }
            }
        }
    }
}