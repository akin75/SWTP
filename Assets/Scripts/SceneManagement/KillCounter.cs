/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class KillCounter : MonoBehaviour
{

    private int killsCounter = 0;

    [SerializeField] private TMP_Text killCounterText;

    void Start()
    {
        UpdateKillCount();
    }

    /// <summary>
    /// Increase the killcounter by one
    /// </summary>
    public void IncreaseKillCount()
    {
            killsCounter++;
            UpdateKillCount();
    }

    /// <summary>
    /// Set the killcounter text in gui
    /// </summary>
    public void UpdateKillCount()
    {
        killCounterText.SetText(""+killsCounter);
    }

    /// <summary>
    /// Get the current count of kills
    /// </summary>
    /// <returns>The number of current kills</returns>
    public int GetKills()
    {
        return killsCounter;
    }
}