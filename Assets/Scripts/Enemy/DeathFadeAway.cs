using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFadeAway : MonoBehaviour
{
    public float fadeDuration = 3.5f; // Dauer des Ausblendens
    [SerializeField] private ParticleSystem bloodPuddle;
    private Renderer objectRenderer; // Renderer-Komponente des Gameobjekts
    private Color originalColor; // Ursprüngliche Farbe des Gameobjekts
    private bool fading = false; // Gibt an, ob das Ausblenden im Gange ist

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        StartFade();
        Instantiate(bloodPuddle, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (fading)
        {
            // Reduziere die Alpha-Komponente der Farbe basierend auf die verstrichene Zeit
            float fadeAmount = Mathf.Clamp01(Time.deltaTime / fadeDuration);
            Color newColor = objectRenderer.material.color;
            newColor.a -= fadeAmount;
            objectRenderer.material.color = newColor;

            if (objectRenderer.material.color.a <= 0f)
            {
                // Das Gameobjekt ist vollständig ausgeblendet, zerstöre es
                Destroy(gameObject);
            }
        }
    }

    public void StartFade()
    {
        fading = true;
    }
}