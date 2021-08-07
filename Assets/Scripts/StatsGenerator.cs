using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A <see langword="static"/> class with methods (functions) for initialising or randomising the stats class.
///  
/// TODO:
///     Initialise a stats instance with generated starting stats
///     Handle the assignment of extra points or xp into an existing stats of a character
///         - this is expected to be used by NPCs leveling up to match the player.
/// </summary>
public static class StatsGenerator
{
    /// <summary>
    /// A dictionary of each level that characters can acheive, and the stats that are associated with them.
    /// </summary>
    public static Dictionary<int, List<int>> statsBlock = new Dictionary<int, List<int>>()
    {
    // Level Number -- Style / Luck / Rhythm
        { 1, new List<int>() {1, 1, 1}},
        { 2, new List<int>() {2, 2, 1}},
        { 3, new List<int>() {3, 3, 1}},
        { 4, new List<int>() {4, 3, 2}},
        { 5, new List<int>() {5, 4, 2}},
        { 6, new List<int>() {6, 4, 3}},
        { 7, new List<int>() {7, 5, 3}},
        { 8, new List<int>() {8, 5, 4}},
        { 9, new List<int>() {9, 6, 4}},
        { 10, new List<int>() {10, 7, 5}}
    };

    public static void InitialStats(Stats stats, bool isPlayer)
    {
        // Enforce a level 1 or higher, and no higher then 10
        if (stats.level <= 0) { stats.level = 1; }
        else if (stats.level >= 10) { stats.level = 10; }
        // get stats from statsBlock.
        statsBlock.TryGetValue(stats.level, out List<int> initStats);
        // apply stats with modifiers to the stats object.
        // If newly generated stats are less then 0, revert to original number.
        if (isPlayer)
        { // Player will not have any random modifiers in stats.
            stats.style = initStats[0];
            stats.luck = initStats[1];
            stats.rhythm = initStats[2];
        }
        else
        {
            // generate random modifers for stats.
            var randomValues = new List<int>
            {
                Mathf.RoundToInt(Random.Range(-2, 2)),
                Mathf.RoundToInt(Random.Range(-2, 2)),
                Mathf.RoundToInt(Random.Range(-2, 2))
            };
            stats.style = initStats[0] + randomValues[0] < 1 ? 1 : initStats[0];
            stats.luck = initStats[1] + randomValues[1] < 1 ? 1 : initStats[1];
            stats.rhythm = initStats[2] + randomValues[2] < 1 ? 1 : initStats[2];
        }

    }

    public static void AssignUnusedPoints(Stats stats, int points)
    {

    }
}
