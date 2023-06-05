using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 2;

    public ParticleSystem bloodSplatter;
    public ParticleSystem deathParticles;
    public Rigidbody2D rb;
    public GameObject deadZombiePrefab;

    public GameObject itemDrop;
    public int dropChance;

    public HealthBar healthBar;

    private SpriteRenderer sprite;
    private Quaternion deathRotation; // Speichert die Rotation des Objekts vor der Zerstörung
    private Vector3 deathScale; // Speichert die Skalierung des Objekts vor der Zerstörung
    KillCounter killCounter;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        sprite = GetComponent<SpriteRenderer>();
        killCounter = FindObjectOfType<KillCounter>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            if (deadZombiePrefab != null)
            {
                // Speichere die Rotation und Skalierung des Objekts
                deathRotation = transform.rotation;
                deathScale = transform.localScale;

                GameObject deadZombie = Instantiate(deadZombiePrefab, transform.position, deathRotation);
                deadZombie.transform.localScale = deathScale;
            }

            if (deathParticles != null)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);

            if (dropChance >= Random.Range(0, 100))
            killCounter.IncreaseKillCount();
            if (dropChance >= Random.Range(0, 100)) 
            {
                Instantiate(itemDrop, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator HitEffect()
    {
        sprite.color = Color.grey;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
