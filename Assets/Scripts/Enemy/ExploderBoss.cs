using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ExploderBoss : MonoBehaviour
{
    public ParticleSystem explosionParticlesBoss;
    public ParticleSystem smokeParticlesBoss;
    public SpriteRenderer enemySr;
    public Rigidbody2D exploder;
    public Collider2D explosionRadius;
    public Collider2D blinkingRadius;
    public GameObject hB;
    public GameObject trail;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    public GameObject zombieMini;
    public int spawnCount;
    public int explosionDamage = 40;
    private Rigidbody2D rb;
    public GameObject explosionSfx;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
            if (explosionRadius.IsTouching(collision))
            {
                //Debug.Log("Explosion area entered");
                // Der Spieler ist im Explosionsradius, führe die Explosion aus
                Explode();
                collision.gameObject.GetComponent<Player>().TakeDamage(explosionDamage);
            }
        }
    }

    private void SpawningMinis()
    {
        float spawnRadius = 1f; // Radius des Spawnbereichs
        for (int i = 0; i < spawnCount; i++)
        {
            //Debug.Log("Zombie " + i + " is Spawning:");
            Vector2 spawnPosition = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * spawnRadius;
            Instantiate(zombieMini, spawnPosition, Quaternion.identity);
        }
    }
    private void StartBlinking()
    {
        // Starte das Blinken, indem du die Farbe des Sprites änderst
        StartCoroutine(BlinkCoroutine());
    }
    

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking && spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.2f);
        
            if (spriteRenderer == null) // Überprüfe, ob das SpriteRenderer-Objekt zerstört wurde
                yield break; // Beende die Koroutine, wenn das Objekt zerstört wurde

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
        }
    }



    private void Explode()
    {
        //Debug.Log("Boom");
        exploder.simulated = false;
        Instantiate(explosionSfx, transform.position, Quaternion.identity);
        Instantiate(smokeParticlesBoss, transform.position, Quaternion.identity);
        Instantiate(explosionParticlesBoss, transform.position, Quaternion.identity);
        SpawningMinis();
        DestroyObj();
    }

    void DestroyObj()
    {
        GetComponent<EnemyHealth>().currentHealth = 0;
        Destroy(gameObject);
    }
}