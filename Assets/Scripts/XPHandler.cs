﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for converting a battle result into xp to be awarded to the player.
/// 
/// TODO:
///     Respond to battle outcome with xp calculation based on;
///         player win 
///         how strong the win was
///         stats/levels of the dancers involved
///     Award the calculated XP to the player stats
///     Raise the player level up event if needed
/// </summary>
public class XPHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnBattleConclude += GainXP;
    }

    private void OnDisable()
    {
        GameEvents.OnBattleConclude -= GainXP;
    }

    public void GainXP(BattleResultEventData data)
    {
        // If the player won the battle.
        if (data.outcome > 0)
        {
            // XP calculation using level comparisons and win value in formula.
            // Beating higher level NPCs will reward more XP.
            var lvDiff = data.npc.level - data.player.level;
            float firstCalculation = 1 + (lvDiff * 0.1f);
            float firstMultiplier = firstCalculation <= 0.1f ? 0.1f : firstCalculation;
            float secondCalculation = 1 + (0.25f * data.outcome);
            float secondMultiplier = secondCalculation <= 0.1f ? 0.1f : secondCalculation;
            var xp = Mathf.RoundToInt(firstMultiplier * secondMultiplier * 100);
            if (xp >= 15) //Guarantees at least some XP will be awarded on winning.
            {
                xp = 15;
            }
            GameEvents.PlayerXPGain(xp);
        }
    }
}
