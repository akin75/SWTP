using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    public Slider hpslider;
    public Gradient gradient;
    public Image fill;
    private PlayerSwitcher playerManager;
    private PlayerClass player;
    
    void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = playerManager.playerClass;
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        hpslider.maxValue = player.GetMaxHealth();
        hpslider.value = player.GetCurrentHealth();
        
        fill.color = gradient.Evaluate(hpslider.normalizedValue);
    }
}
    