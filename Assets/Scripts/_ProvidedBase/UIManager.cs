using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// Splashes simple ui elements when certain game events are triggered
/// 
/// Provided with framework, no modification required
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject npcLevelUI;
    public GameObject playerXPUI;
    public TextMeshProUGUI playerlevelUI;
    public TextMeshProUGUI playerXPThresUI;
    public TextMeshProUGUI playerCurrentXPUI;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        Debug.Log("UI Manger Enabled");
        GameEvents.OnPlayerLevelUp += ShowNPCLevelUI;
        GameEvents.OnPlayerGainXP += ShowPlayerXPUI;
    }

    private void OnDisable()
    {
        
        GameEvents.OnPlayerLevelUp -= ShowNPCLevelUI;
        GameEvents.OnPlayerGainXP -= ShowPlayerXPUI;
    }

    void ShowNPCLevelUI(int level)
    {
        StartCoroutine(NPCLevelUI());
    }

    void ShowPlayerXPUI(int xp)
    {
        StartCoroutine(PlayerXPUI(xp));
    }

    IEnumerator NPCLevelUI()
    {
        npcLevelUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        npcLevelUI.SetActive(false);
    }

    public void UpdateUI(int currentXP, int xpThreshold, int level) 
    {
        Debug.Log("Update UI Called - " + currentXP + " " + xpThreshold + " " + level);
        playerlevelUI.text = level.ToString();
        playerCurrentXPUI.text = currentXP.ToString();
        playerXPThresUI.text = xpThreshold.ToString();
    }

    IEnumerator PlayerXPUI(int xp)
    {
        int currentXP = int.Parse(playerCurrentXPUI.text);
        int xpDisplay = 1;
        playerXPUI.SetActive(true);
        while (xpDisplay < xp)
        {
            xpDisplay++;
            currentXP++;
            //playerCurrentXPUI.text = currentXP.ToString();
            playerXPUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "+" + xpDisplay.ToString() + "XP";
            yield return null;
        }        

        yield return new WaitForSeconds(1f);
        playerXPUI.SetActive(false);
    }
}
