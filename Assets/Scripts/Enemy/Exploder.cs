using UnityEngine;

public class Exploder : MonoBehaviour
{
    private Transform player; // Spielerreferenz
    public SpriteRenderer spriteRenderer; // SpriteRenderer des Zombies

    public float blinkRate1 = 1f; // Blinkrate in Sekunden bei 15 Abstand
    public float blinkRate2 = 0.5f; // Blinkrate in Sekunden bei 10 Abstand

    public float destroyDistance = 5f; // Abstand, bei dem das Objekt zerstört werden soll

    private float currentBlinkRate; // Aktuelle Blinkrate

    private bool isBlinking = false; // Flag für Blinkzustand
    private bool isDestroyed = false; // Flag für Zerstörungszustand

    private void Start()
    {
        currentBlinkRate = blinkRate1; // Initialisiere Blinkrate
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Suchen des Spielers als Ziel
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isDestroyed)
        {
            // Überprüfe Abstand zum Spieler
            if (distanceToPlayer <= destroyDistance)
            {
                DestroyZombie();
            }
            else if (distanceToPlayer <= 5f)
            {
                // Entfernung von 5 erreicht - Zerstöre das Objekt
                Destroy(gameObject);
            }
            else if (distanceToPlayer <= 10f)
            {
                // Entfernung von 10 erreicht - Blinkrate erhöhen
                currentBlinkRate = blinkRate2;
                StartBlinking();
            }
            else if (distanceToPlayer <= 15f)
            {
                // Entfernung von 15 erreicht - Blinkrate zurücksetzen
                currentBlinkRate = blinkRate1;
                StartBlinking();
            }
            else
            {
                StopBlinking();
            }
        }
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleSpriteVisibility", currentBlinkRate, currentBlinkRate);
        }
    }

    private void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            CancelInvoke("ToggleSpriteVisibility");
            spriteRenderer.enabled = true; // Stelle sicher, dass das Sprite sichtbar ist
        }
    }

    private void ToggleSpriteVisibility()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    private void DestroyZombie()
    {
        isDestroyed = true;
        // Hier kannst du die Logik für die Zombie-Explosion oder andere Effekte einfügen
        // Zum Beispiel: Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
