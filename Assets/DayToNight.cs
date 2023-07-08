using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayToNight : MonoBehaviour
{
    private Light2D light2D;
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
    }

    public void DecreaseLight(float value)
    {
        if (light2D.intensity > 0)
        {
            light2D.intensity -= value;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DecreaseLight(0.05f);
        }
    }
}
