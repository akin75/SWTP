using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public static int currency = 100;

    public HealthBar healthBar;
    public SpriteRenderer playerSprite;
    public GameObject deadPlayer;
    public PauseMenu pauseMenu;
    public Color hitColor = new Color(1f, 0.5f, 0.5f);
    public float hitDuration = 0.2f;
    public ParticleSystem smallBloodPuddle;
    public ParticleSystem bigBloodPuddle;
    public float impactForceMultiplier = 1f;

    private Rigidbody2D rb;
    private bool isDead = false;
    private bool impactForceBool = false;
    private Quaternion initialRotation; // Speichert die Rotation des ursprünglichen Objekts
    private PlayerSwitcher playerManager;
    

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        PlayerClass playerClass = playerManager.playerClass;
        currentHealth = playerClass.maxHealth;
        currency = playerClass.GetCurrency();
        Debug.Log("maxHealth " + maxHealth);
        healthBar.SetMaxHealth(maxHealth);
        if (currentHealth < maxHealth)
        {
            //Debug.Log("True");
            healthBar.SetHealth(currentHealth);
        }
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) 
    {
        currentHealth = currentHealth - damage;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);
            playerManager.playerClass.SetHealth(currentHealth);
            ApplyImpact(damage * impactForceMultiplier);
            StartCoroutine(HitFlash());


        Debug.Log("currentHealth " + currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            playerSprite.color = Color.red;
            initialRotation = transform.rotation; // Speichere die Rotation des ursprünglichen Objekts
            Instantiate(deadPlayer, transform.position, initialRotation); // Verwende die gespeicherte Rotation
            Destroy(gameObject);
            pauseMenu.GameOver();
            
        }
        impactForceBool = false;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    
     public int GetMaxHealth(){
        return maxHealth;
    }
    public int GetCoins()
    {
        return playerManager.playerClass.GetCurrency();
    }
    private IEnumerator HitFlash()
    {
        playerSprite.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        playerSprite.color = Color.white;
    }
    

    private void ApplyImpact(float force)
    {
        this.impactForceBool = true;
        //Debug.Log(this.impactForceBool);
        Vector2 impactDirection = -transform.up; // Richtung, in die der Player zurückgeworfen wird
        Vector2 impactForce = impactDirection * force;

        // Den absoluten Wert der Impact Force verwenden
        impactForce = new Vector2(Mathf.Abs(impactForce.x), Mathf.Abs(impactForce.y));

        //Debug.Log("Impact Force: " + impactForce);
        rb.AddForce(impactForce, ForceMode2D.Impulse);
    }

    public bool GetImpactForceBool()
    {
        return impactForceBool;
    }


    public void SetCurrentHealth(int currentHealth){
        this.currentHealth = currentHealth;
    }

    public void setCurrency(int value) {
        currency = currency + value;
        playerManager.playerClass.SetCurrency(currency);
        //Debug.Log("Currency: " + currency);
    }

    public void setMaxHealth(int value) {
        maxHealth = maxHealth + value;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = currentHealth + value;
        //Debug.Log("currentHealth: " + currentHealth + "\nmaxHealth: " + maxHealth);
    }
}
