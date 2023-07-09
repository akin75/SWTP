using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayToNight : MonoBehaviour
{
    private Light2D light2D;
    private WaveSpawner spawner;
    [SerializeField] private AnimationCurve intesityCurve;
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        spawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
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
        if (spawner.state == WaveSpawner.spawnState.WAITING)
        {
            SetIntensity(intesityCurve.Evaluate(spawner.waveTracker));
        }
    }

    private void SetIntensity(float value)
    {
        light2D.intensity = value;
    }
}
