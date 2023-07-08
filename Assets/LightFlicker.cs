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
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        StartCoroutine(StartFlickering(light2D));
    }

    private IEnumerator StartFlickering(Light2D light2D)
    {
        float intensity = light2D.intensity;
        while (true)
        {
            this.light2D.intensity = (light2D.intensity == 0) ? intensity : 0;
            float randomFlickerSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
            yield return new WaitForSeconds(randomFlickerSpeed);
        }
    }
}
