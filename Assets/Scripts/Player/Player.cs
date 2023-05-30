using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public static int currency;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) 
    {
        currentHealth = currentHealth - damage;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);

        //Debug.Log("currentHealth " + currentHealth);
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void setCurrency(int value) {
        currency = currency + value;
        //Debug.Log("Currency: " + currency);
    }

    public void setMaxHealth(int value) {
        maxHealth = maxHealth + value;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = currentHealth + value;
        //Debug.Log("currentHealth: " + currentHealth + "\nmaxHealth: " + maxHealth);
    }
}
