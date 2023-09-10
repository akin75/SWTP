using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    private List<GameObject> enemiesInDamageArea = new List<GameObject>();
    [SerializeField] private Collider2D damageArea;
    [SerializeField] private float health;
    private float playerDamage;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    private bool hasExploded = false;
    [SerializeField] private int damage = 100;
    [SerializeField] private GameObject explosionSfx;
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
            //Debug.Log("Hit Barrel");
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
                    //Debug.Log(enemiesInDamageArea);
                    //Debug.Log("Enemy entered DamageArea: " + collision.gameObject.name);
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
                //Debug.Log("Enemy exited DamageArea: " + collision.gameObject.name);
            }
        }
    }
}
