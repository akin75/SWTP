using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    public float minFlickerSpeed = 0.02f;
    public float maxFlickerSpeed = 0.3f;
    public float intensity = 0.4f;

    private Light2D lightComponent;
    

    private void OnEnable()
    {
        lightComponent = GetComponent<Light2D>();
        StartFlicker();
    }

    public void StartFlicker()
    {
        StartCoroutine(StartFlickerCoroutine());
    }
    

    private IEnumerator StartFlickerCoroutine()
    {
        Debug.Log("Flicker activated");
        while (true)
        {
            lightComponent.intensity = (lightComponent.intensity == 0) ? intensity : 0;

            float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);

            yield return new WaitForSeconds(randomFlickerSpeed);
        }
    }
}