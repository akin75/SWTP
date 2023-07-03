using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int maximum;
    public int current;
    public Image fill;
    private PlayerSwitcher playerManager;
    private PlayerClass player;
    
    void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = playerManager.playerClass;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }


    void GetCurrentFill()
    {
        maximum = player.GetMaximumExp();
        current = player.GetCurrentExp();
        float fillAmount = (float)current / (float)maximum;
        fill.fillAmount = fillAmount;
    }
    
}
