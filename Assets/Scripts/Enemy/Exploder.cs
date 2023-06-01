using System;
using System.Collections;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    public SpriteRenderer enemySr;
    public Rigidbody2D exploder;
    public Collider2D explosionRadius;
    public Collider2D blinkingRadius;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    public int damage = 100;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player found");
            if (collision.gameObject == blinkingRadius.gameObject)
            {
                isBlinking = true;
                StartBlinking();
            }
            /*if (collision.gameObject == blinkingRadius.gameObject)
            {
                isBlinking = true;
                StartBlinking();
            }
            else */
            /*
            if (collision.gameObject == explosionRadius.gameObject)
            {
                Debug.Log("Explosion area entered");
                // Der Spieler ist im Explosionsradius, führe die Explosion aus
                Explode();
            }
            */
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Player") && collision == blinkingRadius)
        {
            isBlinking = false;
            StopBlinking();
        }
        */
    }
    
    private void StartBlinking()
    {
        // Starte das Blinken, indem du die Farbe des Sprites änderst
        StartCoroutine(BlinkCoroutine());
    }

    private void StopBlinking()
    {
        // Stoppe das Blinken, indem du die Koroutine beendest und die Farbe des Sprites zurücksetzt
        StopCoroutine(BlinkCoroutine());
        spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Explode()
    {
        Debug.Log("Boom");
        if (smokeParticles != null)
        {
            var mainModule = smokeParticles.main;
            var durE = mainModule.duration;
            var emE = smokeParticles.emission;

            exploder.simulated = false;

            
            explosionParticles.Play();
            smokeParticles.Play();
            
            
            
            /*
            Destroy(enemySr);
            Invoke("DestroyObj", durE);
            */
        }

        // Füge dem Spieler Schaden hinzu oder führe andere Aktionen aus

        
        // Zerstöre den Zombie
    }

    void DestroyObj()
    {
        GetComponent<EnemyHealth>().currentHealth = 0;
        Destroy(gameObject);
    }
}