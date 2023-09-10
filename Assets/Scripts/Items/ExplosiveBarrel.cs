using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    private List<GameObject> enemiesInDamageArea = new List<GameObject>();
    public Collider2D damageArea;
    public float health;
    private float playerDamage;
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    private bool hasExploded = false;
    public int damage = 100;
    public GameObject explosionSfx;
    private bool isCrit = false;
    private AchievementManager achievementManager;


    void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
    }

    /// <summary>
    /// defines what happens when player shots on barrel
    /// </summary>
    /// <param name="collision">collision with bullet</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
            health = health - playerDamage;
            if (health <= 0)
            {
                Explode();
            }
        }
    }

    /// <summary>
    /// handles the explosion of the barrel and makes damage
    /// </summary>
    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Instantiate(explosionSfx, transform.position, Quaternion.identity);

        List<GameObject> enemiesToDamage = new List<GameObject>(enemiesInDamageArea);

        foreach (var enemy in enemiesToDamage)
        {
            if (enemy != null)
            {
                if (enemy.GetComponent<EnemyHealth>().getCurrentHealth() - damage <= 0)
                {
                    achievementManager.ZombieKilledByExplosion();
                }
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage, isCrit);
            }
        }

        enemiesInDamageArea.Clear();

        Destroy(gameObject);
    }
    
    /// <summary>
    /// handles which enemys to damage
    /// </summary>
    /// <param name="collision">enemys in the trigger</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (damageArea.IsTouching(collision))
            {            
                if (!enemiesInDamageArea.Contains(collision.gameObject))
                {
                    enemiesInDamageArea.Add(collision.gameObject);
                  
                }
            }
        }
    }

    /// <summary>
    /// enemys leaving the area
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInDamageArea.Contains(collision.gameObject))
            {
                enemiesInDamageArea.Remove(collision.gameObject);
            }
        }
    }
}
