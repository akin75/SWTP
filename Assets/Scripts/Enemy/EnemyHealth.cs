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
    public TransformToCrawler transformToCrawler;
    private CursorFeedback cursorFeedback;
    
    private SpriteRenderer sprite;
    private Quaternion deathRotation; // Speichert die Rotation des Objekts vor der Zerstörung
    private Vector3 deathScale; // Speichert die Skalierung des Objekts vor der Zerstörung
    private KillCounter killCounter;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        sprite = GetComponent<SpriteRenderer>();
        killCounter = FindObjectOfType<KillCounter>();
        cursorFeedback = FindObjectOfType<CursorFeedback>();
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
        Debug.Log(gameObject);
        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            if (deadZombiePrefab != null)
            {
                cursorFeedback.StartCursorFeedback(); // Starte die Coroutine für die Todesszene

                deathRotation = transform.rotation;
                deathScale = transform.localScale;

                GameObject deadZombie = Instantiate(deadZombiePrefab, transform.position, deathRotation);
                deadZombie.transform.localScale = deathScale;
                //transformToCrawler.Transformation();
            }

            if (deathParticles != null)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            killCounter.IncreaseKillCount();
            if (dropChance >= Random.Range(0, 100) && itemDrop != null) 
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
