using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 2;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
