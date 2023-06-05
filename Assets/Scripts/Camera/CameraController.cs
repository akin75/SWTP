using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float camSize = 10;
    public float cameraOffsetFactor = 0.25f;

    private Vector3 mousePosition;

    void Start()
    {
        cam.orthographicSize = camSize;
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
}