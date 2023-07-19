using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

   
    public Slider hpslider;
    public Gradient gradient;
    public Image fill;

    private void Awake() {
         
          if(SettingsMenuScript.HealthBarOn)
             transform.gameObject.SetActive(true);
             else
              transform.gameObject.SetActive(false);
    }
    public void SetMaxHealth(int health) {
        hpslider.maxValue = health;
        hpslider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        hpslider.value = health;

        fill.color = gradient.Evaluate(hpslider.normalizedValue);
    }

    public void DisableHealthBar(){


    }
}
