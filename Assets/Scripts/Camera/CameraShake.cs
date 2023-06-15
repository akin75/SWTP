using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.5f; // Intensität der Kamerabewegung
    public float shakeDuration = 0.02f; // Dauer der Kamerabewegung

    public void StartShaking(Vector2 shotDirection)
    {
        StartCoroutine(ShakeCamera(shotDirection));
    }
    private IEnumerator ShakeCamera(Vector2 shotDirection)
    {
        Vector3 originalPosition = transform.localPosition; // Ursprüngliche Position der Kamera

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Berechne die Verschiebung basierend auf der Schussrichtung und Intensität
            Vector2 offset = -shotDirection * shakeIntensity;

            transform.localPosition = originalPosition + new Vector3(offset.x, offset.y, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition; // Zurücksetzen der Kameraposition
    }

}