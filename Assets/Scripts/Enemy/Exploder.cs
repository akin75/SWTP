using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private SpriteRenderer enemySr;
    [SerializeField] private Rigidbody2D exploder;
    [SerializeField] private Collider2D explosionRadius;
    [SerializeField] private Collider2D blinkingRadius;
    [SerializeField] private Collider2D fastBlinkingRadius;
    [SerializeField] private GameObject hB;
    [SerializeField] private GameObject trail;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    [SerializeField] private int explosionDamage = 40;
    [SerializeField] private GameObject explosionSfx;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// defines what happens at collisions
    /// </summary>
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

    /// <summary>
    /// stops blinking when player exits the danger zone
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Blinking area exited");
        StopBlinking();
    }

    /// <summary>
    /// starts blinking when player exits the danger zone
    /// </summary>
    private void StartBlinking()
    {
        // Starte das Blinken, indem du die Farbe des Sprites änderst
        StartCoroutine(BlinkCoroutine());
    }

    /// <summary>
    /// starts blinking faster when player exits the danger zone
    /// </summary>
    private void StartFastBlinking()
    {
        StartCoroutine(FastBlinkCoroutine());
    }

    /// <summary>
    /// stops blinking when player exits the danger zone
    /// </summary>
    private void StopBlinking()
    {
        // Stoppe das Blinken, indem du die Koroutine beendest und die Farbe des Sprites zurücksetzt
        StopCoroutine(BlinkCoroutine());
        StopCoroutine(FastBlinkCoroutine());
        //spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// handles the Coroutine of the blinking
    /// </summary>
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
    
    /// <summary>
    /// handles the Coroutine of the fast blinking
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Handles what happens, when the enemy explodes
    /// </summary>
    private void Explode()
    {
        StopBlinking();

        exploder.simulated = false;

        //Debug.Log(explosionParticles, smokeParticles);
        Instantiate(explosionSfx, transform.position, Quaternion.identity);
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        DestroyObj();
        //Destroy(gameObject);
    }
    
    /// <summary>
    /// Destroys enemy
    /// </summary>
    void DestroyObj()
    {
        GetComponent<EnemyHealth>().currentHealth = 0;
        Destroy(gameObject);
    }
}