using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public static int currency = 100;

    public HealthBar healthBar;
    public SpriteRenderer playerBody;
    public SpriteRenderer playerHead;
    public SpriteRenderer playerRau;
    public SpriteRenderer playerRal;
    public SpriteRenderer playerLau;
    public SpriteRenderer playerLal;
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


    private void Awake()
    {
        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        PlayerClass playerClass = playerManager.playerClass;
        SetMaxHealth(GetMaxHealth());
        SetCurrentHealth(GetCurrentHealth());
        //Debug.Log($"Max {GetMaxHealth()}  Current {GetCurrentHealth()}");
        currency = playerClass.GetCurrency();
        transform.GetComponent<PlayerController>().moveSpeed = playerClass.GetMoveSpeed();
        //Debug.Log("maxHealth " + maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) 
    {
        SetCurrentHealth(GetCurrentHealth() - damage);
        Debug.Log("currentHealth " + GetCurrentHealth());
        ApplyImpact(damage * impactForceMultiplier);
        StartCoroutine(HitFlash());


        
        if (GetCurrentHealth() <= 0)
        {
            isDead = true;
            playerBody.color = Color.red;
            initialRotation = transform.rotation; // Speichere die Rotation des ursprünglichen Objekts
            Instantiate(deadPlayer, transform.position, initialRotation); // Verwende die gespeicherte Rotation
            Destroy(gameObject);
            pauseMenu.GameOver();
            
        }
        impactForceBool = false;
    }
    public void heal(int value) 
    {
        SetCurrentHealth(GetCurrentHealth() + value);
        StartCoroutine(HealFlash());
    }


    

    public bool GetIsDead()
    {
        return isDead;
    }

    public int GetCurrentHealth()
    {
        return playerManager.playerClass.GetCurrentHealth();
    }
    
     public int GetMaxHealth(){
        return playerManager.playerClass.GetMaxHealth();
    }
    public int GetCoins()
    {
        return playerManager.playerClass.GetCurrency();
    }
    private IEnumerator HitFlash()
    {
        playerHead.color = hitColor;
        playerBody.color = hitColor;
        playerRau.color = hitColor;
        playerRal.color = hitColor;
        playerLau.color = hitColor;
        playerLal.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        playerHead.color = Color.white;
        playerRau.color = Color.white;
        playerRal.color = Color.white;
        playerLau.color = Color.white;
        playerLal.color = Color.white;
        playerBody.color = Color.white;
    }
        private IEnumerator HealFlash()
    {
        playerHead.color = Color.green;
        playerBody.color = Color.green;
        playerRau.color = Color.green;
        playerRal.color = Color.green;
        playerLau.color = Color.green;
        playerLal.color = Color.green;
        yield return new WaitForSeconds(hitDuration);
        playerHead.color = Color.white;
        playerRau.color = Color.white;
        playerRal.color = Color.white;
        playerLau.color = Color.white;
        playerLal.color = Color.white;
        playerBody.color = Color.white;
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
        
        playerManager.playerClass.SetHealth(currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void setCurrency(int value) {
        currency = currency + value;
        playerManager.playerClass.SetCurrency(currency);
        //Debug.Log("Currency: " + currency);
    }

    public void setMaxHealth(int value) {
        healthBar.SetMaxHealth(maxHealth);
        SetMaxHealth(GetMaxHealth() + value);
        SetCurrentHealth(GetCurrentHealth() + value);
        Debug.Log($"CurrentHealth is {GetCurrentHealth()}");
    }

    public void SetMaxHealth(int maxHealth)
    {
        playerManager.playerClass.SetMaxHealth(maxHealth);
        healthBar.SetMaxHealth(playerManager.playerClass.GetMaxHealth());
    }
}
