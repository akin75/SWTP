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
    public int damage = 50;

    void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit Barrel");
            playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
            health = health - playerDamage;
            if (health <= 0)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);

        List<GameObject> enemiesToDamage = new List<GameObject>(enemiesInDamageArea);

        foreach (var enemy in enemiesToDamage)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        enemiesInDamageArea.Clear();

        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (damageArea.IsTouching(collision))
            {            
                if (!enemiesInDamageArea.Contains(collision.gameObject))
                {
                    enemiesInDamageArea.Add(collision.gameObject);
                    Debug.Log(enemiesInDamageArea);
                    Debug.Log("Enemy entered DamageArea: " + collision.gameObject.name);
                }
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInDamageArea.Contains(collision.gameObject))
            {
                enemiesInDamageArea.Remove(collision.gameObject);
                Debug.Log("Enemy exited DamageArea: " + collision.gameObject.name);
            }
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with Enemy");
        }
    }
}
