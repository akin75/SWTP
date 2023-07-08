using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightFlicker : MonoBehaviour
{
    public float minFlickerSpeed = 0.02f;
    public float maxFlickerSpeed = 0.3f;
    private float intensity;

    private Light2D lightComponent;

    private void Awake()
    {
        // Starten Sie das Flackern des Lichts
        StartCoroutine(StartFlicker());
    }

    private IEnumerator StartFlicker()
    {
        intensity = gameObject.GetComponent<Light2D>().intensity;
        lightComponent = GetComponent<Light2D>();

        Debug.Log("Flicker activated");
        while (true)
        {
            
            // Intensität auf 0 oder 1 setzen
            lightComponent.intensity = (lightComponent.intensity == 0) ? intensity : 0;

            // Zufälligen FlickerSpeed innerhalb des angegebenen Bereichs auswählen
            float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);

            // Warten für die angegebene Zeit
            yield return new WaitForSeconds(randomFlickerSpeed);
        }
    }
}