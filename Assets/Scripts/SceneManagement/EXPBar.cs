/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPBar : MonoBehaviour
{
    [SerializeField] private int maximum;
    [SerializeField] private int current;
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
    /// Set the experience bar to the current value
    /// </summary>
    void GetCurrentFill()
    {
        maximum = player.GetMaximumExp();
        current = player.GetCurrentExp();
        float fillAmount = (float)current / (float)maximum;
        fill.fillAmount = fillAmount;
    }
    
}
