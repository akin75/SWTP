using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    private Transform target;
    public float camSize = 10;
    public float cameraOffsetFactor = 0.25f;
    
    public float shakeIntensity = 0.5f; // Intensität der Kamerabewegung
    public float shakeDuration = 0.02f; // Dauer der Kamerabewegung

    private Vector3 mousePosition;

    void Start()
    {
        cam.orthographicSize = camSize;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (target != null)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = target.position;
            Vector3 direction = (mousePosition - targetPosition).normalized; // Richtung von Target zur Maus
            Vector3 offset = direction * Vector3.Distance(targetPosition, mousePosition) * cameraOffsetFactor; // Verschiebung basierend auf dem Abstand
            Vector3 midpoint = targetPosition + offset; // Berechnung des Mittelpunkts mit Offset
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
    
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