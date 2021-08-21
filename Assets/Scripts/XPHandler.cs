using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for converting a battle result into xp to be awarded to the player.
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
        // XP calculation using level comparisons and win value in formula.
        // Beating higher level NPCs will reward more XP.
        var lvDiff = data.npc.level - data.player.level;
        float firstCalculation = 1 + (lvDiff * 0.1f);
        float firstMultiplier = firstCalculation <= 0.1f ? 0.1f : firstCalculation;
        float secondCalculation = 1 + (0.25f * data.outcome);
        float secondMultiplier = secondCalculation <= 0.1f ? 0.1f : secondCalculation;
        var xp = Mathf.RoundToInt(firstMultiplier * secondMultiplier * 100);
        // If the player won the battle.
        if (data.outcome > 0)
        {
            Debug.Log("XP: " + xp);
            if (xp <= 50) //Guarantees at least some XP will be awarded on winning.
            {
                xp = 50;
            }
            GameEvents.PlayerXPGain(xp);
        }
        // if the NPC won the battle
        else if (data.outcome < 0)
        {
            Debug.Log("XP: " + xp);
            GameEvents.PlayerXPGain(25 + (xp / 10));
        }
    }
}
