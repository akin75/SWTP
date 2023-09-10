using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightFlicker : MonoBehaviour
{
    public float minFlickerSpeed = 0.02f;
    public float maxFlickerSpeed = 0.3f;
    private float intensity;

    private Light2D lightComponent;
    private WaveSpawner spawner;
    private static bool scriptCalled = false;
    private GameObject player;

    private void Awake()
    {
        // Starten Sie das Flackern des Lichts
        spawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        scriptCalled = true;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartFlicker());
    }
/// <summary>
/// Starts the flicker of Flashlight
/// </summary>
/// <returns></returns>
    private IEnumerator StartFlicker()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var instance = player.GetComponentInChildren<Light2D>();
        if (instance == null)
        {
            yield break;
        }
        intensity = instance.intensity;
        lightComponent = instance;

        
        while (true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            var newInstance = player.GetComponentInChildren<Light2D>();
            if (spawner.waveTracker >= 7)
            {
                
                // Intensität auf 0 oder 1 setzen
                lightComponent.intensity = (lightComponent.intensity == 0) ? intensity : 0;
                // Zufälligen FlickerSpeed innerhalb des angegebenen Bereichs auswählen
                float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
                
                lightComponent = newInstance;
                // Warten für die angegebene Zeit
                yield return new WaitForSeconds(randomFlickerSpeed);
            }

            if (spawner.waveTracker >= 3 && spawner.waveTracker <= 6)
            {
                lightComponent = newInstance;
                lightComponent.intensity = 0.5f;
                yield return null;
            }
            if (spawner.waveTracker <= 2)
            {
                lightComponent = newInstance;
                lightComponent.intensity = 0f; //
                yield return null;
            }
            yield return null;
        }
    }
}