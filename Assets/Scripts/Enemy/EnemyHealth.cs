using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 2;

    public ParticleSystem bloodSplatter;
    public Rigidbody2D rb;

    public GameObject itemDrop;
    public int dropChance;

    public HealthBar healthBar;

    KillCounter killCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) {
            Destroy(gameObject);
            killCounter.IncreaseKillCount();
            if (dropChance >= Random.Range(0, 100)) 
            {
                Instantiate(itemDrop, transform.position, Quaternion.identity);
            }
        }
    }
}
