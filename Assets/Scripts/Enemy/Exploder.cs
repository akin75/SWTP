using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    public SpriteRenderer enemySr;
    public Rigidbody2D exploder;
    public Collider2D explosionRadius;
    public Collider2D blinkingRadius;
    public Collider2D fastBlinkingRadius;
    public GameObject hB;
    public GameObject trail;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    public int explosionDamage = 40;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player found");
            if (blinkingRadius.IsTouching(collision))
            {
                //Debug.Log("Blinking Radius entered");
                isBlinking = true;
                StartBlinking();
            }
            if (fastBlinkingRadius.IsTouching(collision))
            {
                //Debug.Log("Blinking Radius entered");
                StartBlinking();
                StartFastBlinking();

                //StartCoroutine(Inflate());
            }
            if (explosionRadius.IsTouching(collision))
            {
                //Debug.Log("Explosion area entered");
                // Der Spieler ist im Explosionsradius, führe die Explosion aus
                Explode();
                collision.gameObject.GetComponent<Player>().TakeDamage(explosionDamage);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Blinking area exited");
        StopBlinking();
    }

    private void StartBlinking()
    {
        // Starte das Blinken, indem du die Farbe des Sprites änderst
        StartCoroutine(BlinkCoroutine());
    }

    private void StartFastBlinking()
    {
        StartCoroutine(FastBlinkCoroutine());
    }

    private void StopBlinking()
    {
        // Stoppe das Blinken, indem du die Koroutine beendest und die Farbe des Sprites zurücksetzt
        StopCoroutine(BlinkCoroutine());
        StopCoroutine(FastBlinkCoroutine());
        //spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking && spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
        
            if (spriteRenderer == null) // Überprüfe, ob das SpriteRenderer-Objekt zerstört wurde
                yield break; // Beende die Koroutine, wenn das Objekt zerstört wurde

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FastBlinkCoroutine()
    {
        while (isBlinking && spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
        
            if (spriteRenderer == null) // Überprüfe, ob das SpriteRenderer-Objekt zerstört wurde
                yield break; // Beende die Koroutine, wenn das Objekt zerstört wurde

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.02f);
        }
    }


    private void Explode()
    {
        StopBlinking();

        exploder.simulated = false;

        Debug.Log(explosionParticles, smokeParticles);
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        DestroyObj();
        //Destroy(gameObject);
    }

    void DestroyObj()
    {
        GetComponent<EnemyHealth>().currentHealth = 0;
        Destroy(gameObject);
    }
}