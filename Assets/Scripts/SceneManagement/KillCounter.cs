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
        killCounterText.SetText(""+killsCounter);
    }

    public void IncreaseKillCount()
    {
            killsCounter++;
            UpdateKillCount();
    }

    public void UpdateKillCount()
    {
        killCounterText.SetText(""+killsCounter);
    }

    public int GetKills()
    {
        return killsCounter;
    }
}