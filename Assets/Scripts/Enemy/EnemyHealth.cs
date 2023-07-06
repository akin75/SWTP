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

    private bool playerCanTakeDamage = true;

    public HealthBar healthBar;
    public TransformToCrawler transformToCrawler;
    private CursorFeedback cursorFeedback;
    public ParticleSystem bloodPuddleHit;
    public GameObject deathSfx;
    
    private SpriteRenderer sprite;
    private Quaternion deathRotation; // Speichert die Rotation des Objekts vor der Zerstörung
    private Vector3 deathScale; // Speichert die Skalierung des Objekts vor der Zerstörung
    private KillCounter killCounter;
    public int experiencePoint;
    private PlayerSwitcher playerManager;
    private Player player;

    private bool isDestroyed;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        sprite = GetComponent<SpriteRenderer>();
        killCounter = FindObjectOfType<KillCounter>();
        cursorFeedback = FindObjectOfType<CursorFeedback>();
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            if (player != null)
            {
                StartCoroutine(PlayerTakesDamage(collision));
            }
        }
    }
    
    private IEnumerator PlayerTakesDamage(Collision2D collision)
    {
        if (playerCanTakeDamage && player != null)
        {
            playerCanTakeDamage = false;
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            yield return new WaitForSeconds(0.5f);
            playerCanTakeDamage = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        //Debug.Log(gameObject);
        StartCoroutine(HitEffect());
        Instantiate(bloodPuddleHit, transform.position, Quaternion.identity);

        if (currentHealth <= 0)
        {
            if (deadZombiePrefab != null)
            {
                cursorFeedback.StartCursorFeedback(); // Starte die Coroutine für die Todesszene
                GameObject deathSfx = Instantiate(this.deathSfx, transform.position, Quaternion.identity);
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
            isDestroyed = true;
            if (killCounter != null)
            {
                killCounter.IncreaseKillCount();
            }
            if (dropChance >= Random.Range(0, 100) && itemDrop != null) 
            {
                Instantiate(itemDrop, transform.position, Quaternion.identity);
            }
//            Debug.Log("Experience Points given: " + experiencePoint);
            playerManager.playerClass.AddExpPoints(experiencePoint);
        }
        else
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator HitEffect()
    {
        Color originalColor = sprite.color; // Speichere die ursprüngliche Farbe des Sprites
        sprite.color = Color.grey;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }

    public void setKillCounter(KillCounter counter)
    {
        killCounter = counter;
    }

    public void AddHealth(int health)
    {
        maxHealth += health;
    }
}
