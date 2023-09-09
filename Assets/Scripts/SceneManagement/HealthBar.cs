/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

   
    [SerializeField] private Slider hpslider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    private void Awake() 
    {
        if(SettingsMenuScript.HealthBarOn)
            transform.gameObject.SetActive(true);
        else
            transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Set the healthbar to a new maximum health value.
    /// </summary>
    /// <param name="health">new maximum health</param>
    public void SetMaxHealth(int health) {
        hpslider.maxValue = health;
        hpslider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    /// <summary>
    /// Set the healthbar slider to the current health value.
    /// </summary>
    /// <param name="health">current health</param>
    public void SetHealth(int health) {
        hpslider.value = health;

        fill.color = gradient.Evaluate(hpslider.normalizedValue);
    }
}
