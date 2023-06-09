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
        Debug.Log("Flicker activated");
        scriptCalled = true;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartFlicker());
    }

    private IEnumerator StartFlicker()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var instance = player.GetComponentInChildren<Light2D>();
        if (instance == null)
        {
            Debug.Log("Failed");
            yield break;
        }
        intensity = instance.intensity;
        lightComponent = instance;

        
        while (true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            var newInstance = player.GetComponentInChildren<Light2D>();
            if (spawner.waveTracker >= 25)
            {
                
                // Intensität auf 0 oder 1 setzen
                lightComponent.intensity = (lightComponent.intensity == 0) ? intensity : 0;
                // Zufälligen FlickerSpeed innerhalb des angegebenen Bereichs auswählen
                float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
                
                lightComponent = newInstance;
                // Warten für die angegebene Zeit
                yield return new WaitForSeconds(randomFlickerSpeed);
            }

            if (spawner.waveTracker >= 20 && spawner.waveTracker <= 24)
            {
                lightComponent = newInstance;
                lightComponent.intensity = 1f;
                yield return new WaitForSeconds(1f);
            }
            if (spawner.waveTracker < 20)
            {
                lightComponent = newInstance;
                lightComponent.intensity = 0f;
                yield return new WaitForSeconds(0.01f);
            }

            
        }
    }
}