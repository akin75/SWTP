using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class FlashlightFlicker : MonoBehaviour
{
    public float minFlickerSpeed = 0.02f;
    public float maxFlickerSpeed = 0.3f;
    private float intensity;

    private Light2D lightComponent;
    private bool isFlickering = false;
    
    private void Awake()
    {
        // Starten Sie das Flackern des Lichts
        //StartFlicker();
    }

    private void Start()
    {
        Debug.Log("Flashlight Flicker Script started");
        if (!isFlickering)
        {
            StartFlicker();
        }
    }

    private void Update()
    {
        Debug.Log("testFlicker");
    }

    private IEnumerator StartFlickerCoroutine()
    {
        intensity = gameObject.GetComponent<Light2D>().intensity;
        lightComponent = GetComponent<Light2D>();

        Debug.Log("Flicker activated");
        while (true)
        {
            isFlickering = true;
            // Intensität auf 0 oder 1 setzen
            lightComponent.intensity = (lightComponent.intensity == 0) ? intensity : 0;

            // Zufälligen FlickerSpeed innerhalb des angegebenen Bereichs auswählen
            float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);

            // Warten für die angegebene Zeit
            yield return new WaitForSeconds(randomFlickerSpeed);
        }
    }

    public void StartFlicker()
    {
        StartCoroutine(StartFlickerCoroutine());
    }
}