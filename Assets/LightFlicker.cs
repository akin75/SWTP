using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    public float minFlickerSpeed = 0.02f;
    public float maxFlickerSpeed = 0.3f;
    // Start is called before the first frame update
    private WaveSpawner spawner;
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        spawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        StartCoroutine(StartFlickering(light2D));
        
    }
/// <summary>
/// starts flicker of environment light
/// </summary>
/// <param name="light2D">All light objects</param>
/// <returns></returns>
    private IEnumerator StartFlickering(Light2D light2D)
    {
        float intensity = light2D.intensity;
        while (true)
        {
            if (spawner.waveTracker > 1)
            {
                this.light2D.intensity = (light2D.intensity == 0) ? intensity : 0;
                float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
                yield return new WaitForSeconds(randomFlickerSpeed);
            } else if (spawner.waveTracker > 2)
            {
                light2D.intensity = 0.2f;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
