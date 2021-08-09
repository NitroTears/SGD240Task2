using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class with method (function) to determine the outcome of a dance battle based on the player and NPC that are 
///     dancing off against each other.
///     
/// TODO:
///     Battle needs to use stats and random to determine the winner of the dance off
///       - outcome value to be a float value between 1 and negative 1. 1 being the biggest possible player win over NPC, 
///         through to -1 being the most decimating defeat of the player possible.
/// </summary>
public static class BattleHandler
{
    public static void Battle(BattleEventData data)
    {
        float outcome;
        var playerTotal = data.player.style + data.player.luck + data.player.rhythm * (Random.Range(0f, data.player.luck / 10) + 1);
        var npcTotal = data.npc.style + data.npc.luck + data.npc.rhythm * (Random.Range(0f, data.npc.luck / 10) + 1);
        // decide the outcome value based on the difference between player and npc total values
        var combatTotal = playerTotal - npcTotal;
        Debug.Log(playerTotal + " - " + npcTotal + " = " + combatTotal);
        if (combatTotal > 0)
        {
            outcome = combatTotal / 4 > 1 ? 1 : combatTotal / 4; //If more then 1, round back to 1.
            Debug.Log(outcome);
        }
        else if (combatTotal < 0)
        {
            outcome = combatTotal / 4 < 1 ? -1 : combatTotal / 4; //If less then -1, round back to -1.
            Debug.Log(outcome);
        }
        else { outcome = 0; }

        var results = new BattleResultEventData(data.player, data.npc, outcome);

        GameEvents.FinishedBattle(results);
    }
}
