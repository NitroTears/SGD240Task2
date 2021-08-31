using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles core of player dance interactions with NPCs
/// 
/// Provided with framework, no modification required
/// </summary>
public class Player : MonoBehaviour
{
    private NPC currentOpponent;
    private Stats myStats;
    private Rigidbody body;
    private PlayerController controller;
    private Animator anim;

    private bool isDancing;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        myStats = GetComponent<Stats>();
        var UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (UIManager != null)
        {
            Debug.Log("UI Manager in Player Found");
            StatsGenerator.levelThresBlock.TryGetValue(myStats.level, out int xpThreshold);
            myStats = GetComponent<Stats>();
            // Debug.Log(myStats.xp + "" + xpThreshold + "" + myStats.level);
            // UIManager.UpdateUI(myStats.xp, xpThreshold, myStats.level);
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentOpponent != null && !isDancing)
        {
            //currently colliding with an NPC
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DanceOff()); 
            }
        }
    }

    IEnumerator DanceOff()
    {
        isDancing = true;
        BattleEventData data = new BattleEventData(myStats, currentOpponent.myStats);
        GameEvents.BeginBattle(data);
        controller.enabled = false;
        currentOpponent.transform.LookAt(transform.position);
        body.velocity = Vector3.zero;
        currentOpponent.uiCanvas.SetActive(false);

        yield return new WaitForSeconds(3f);

        currentOpponent.uiCanvas.SetActive(true);
        BattleHandler.Battle(data);
        controller.enabled = true;
        currentOpponent.transform.LookAt(transform.position + Vector3.forward);
        isDancing = false;
    }

    //Check for colliding with NPC, can then interact, 
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<NPC>())
        {
            currentOpponent = other.GetComponent<NPC>();
            currentOpponent.uiCanvas.SetActive(true);
            GameEvents.EnteredBattleRange(new BattleEventData(myStats, currentOpponent.myStats));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<NPC>())
        {
            if(other.GetComponent<NPC>() == currentOpponent)
            {
                GameEvents.ExitedBattleRange(new BattleEventData(myStats, currentOpponent.myStats));
                currentOpponent.uiCanvas.SetActive(false);
                currentOpponent = null;
            }
        }
    }
}
