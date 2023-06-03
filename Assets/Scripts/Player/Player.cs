using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public static int currency;

    public HealthBar healthBar;

    public float impactForceMultiplier = 1f;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        rb = GetComponent<Rigidbody2D>();
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
        ApplyImpact(damage * impactForceMultiplier);
    }

    private void ApplyImpact(float force)
    {
        Vector2 impactDirection = -transform.up; // Richtung, in die der Player zurückgeworfen wird
        Vector2 impactForce = impactDirection * force;

        // Den absoluten Wert der Impact Force verwenden
        impactForce = new Vector2(Mathf.Abs(impactForce.x), Mathf.Abs(impactForce.y));

        //Debug.Log("Impact Force: " + impactForce);
        rb.AddForce(impactForce, ForceMode2D.Impulse);
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
