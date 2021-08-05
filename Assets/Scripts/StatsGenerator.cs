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
    public static void InitialStats(Stats stats)
    {
        if (stats.level <= 0) { stats.level = 1; }
        statsBlock.TryGetValue(stats.level, out List<int> initStats);
        stats.style = initStats[0];
        stats.luck = initStats[1];
        stats.rhythm = initStats[2];
    }

    public static void AssignUnusedPoints(Stats stats, int points)
    {

    }
}
