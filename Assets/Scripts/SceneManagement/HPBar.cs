/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    [SerializeField] private Slider hpslider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
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

    /// <summary>
    /// Set the player healthbar slider to his current health value.
    /// </summary>
    void GetCurrentFill()
    {
        hpslider.maxValue = player.GetMaxHealth();
        hpslider.value = player.GetCurrentHealth();
        
        fill.color = gradient.Evaluate(hpslider.normalizedValue);
    }
}
    