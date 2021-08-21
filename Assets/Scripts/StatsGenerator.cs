using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A <see langword="static"/> class with methods (functions) for initialising or randomising the stats class.
/// </summary>
public static class StatsGenerator
{
    /// <summary>
    /// A dictionary of each level that characters can achieve, and the stats that are associated with them.
    /// </summary>
    public static Dictionary<int, List<int>> statsBlock = new Dictionary<int, List<int>>()
    {
    // Level Number -- Style / Luck / Rhythm
        { 1, new List<int>() {1, 1, 1}},
        { 2, new List<int>() {3, 1, 2}},
        { 3, new List<int>() {5, 1, 3}},
        { 4, new List<int>() {6, 1, 4}},
        { 5, new List<int>() {8, 1, 5}},
        { 6, new List<int>() {10, 2, 6}},
        { 7, new List<int>() {12, 2, 7}},
        { 8, new List<int>() {14, 2, 8}},
        { 9, new List<int>() {15, 3, 9}},
        { 10, new List<int>() {17, 3, 9}}
    };

    /// <summary>
    /// A dictionary of each level that characters can achieve, and the xp threshold required for leveling up.
    /// </summary>
    public static Dictionary<int, int> levelThresBlock = new Dictionary<int, int>()
    {
    // Level Number -- Style / Luck / Rhythm
        { 1, 200},
        { 2, 400},
        { 3, 600},
        { 4, 850},
        { 5, 1100},
        { 6, 1350},
        { 7, 1650},
        { 8, 1900},
        { 9, 2200},
        { 10, 2500}
    };

    /// <summary>
    /// Set ups the initial stats for this stats object, drawing from the statsBlock dictionary.
    /// Adds some random variance to stats if the stats are attached to an NPC instead of a player.
    /// </summary>
    public static void InitialStats(Stats stats, bool isPlayer)
    {
        // Enforce a level 1 or higher, and no higher then 10
        if (stats.level <= 0) { stats.level = 1; }
        else if (stats.level >= 10) { stats.level = 10; }
        // get stats from statsBlock.
        statsBlock.TryGetValue(stats.level, out List<int> initStats);

        // apply stats with modifiers to the stats object.
        // If newly generated stats are less then 0, revert to original number.
        levelThresBlock.TryGetValue(stats.level - 1, out int startingXP);
        stats.xp = startingXP;
        if (isPlayer)
        { // Player will not have any random modifiers in stats.
            stats.style = initStats[0];
            stats.luck = initStats[1];
            stats.rhythm = initStats[2];
        }
        else
        {
            // TODO: CHECK IF 'RANDOM.RANGE()' RETURNS INT OR FLOAT
            // generate random modifers for stats. ModiRanges between -2 and 2
            var randomValues = new List<int>
            {
                Mathf.RoundToInt(Random.Range(-2, 3)),
                Mathf.RoundToInt(Random.Range(-2, 3)),
                Mathf.RoundToInt(Random.Range(-2, 3))
            };
            stats.style = initStats[0] + randomValues[0] < 1 ? 1 : initStats[0];
            stats.luck = initStats[1] + randomValues[1] < 1 ? 1 : initStats[1];
            stats.rhythm = initStats[2] + randomValues[2] < 1 ? 1 : initStats[2];
        }
    }

    // This method is used to boost NPC's stats a little bit whenever the player levels up.
    public static void AssignUnusedPoints(Stats stats, int points)
    {
        // Alternate between adding points between style and rhythm. Does not add to luck.
        for (int i = 0; i < points; i++)
        {
            if (i % 2 == 0)
            {
                stats.style = +1;
            }
            else
            {
                stats.rhythm = +1;
            }
        }
    }
}
