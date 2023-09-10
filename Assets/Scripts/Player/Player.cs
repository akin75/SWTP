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
    public AudioSource playerHitSfx;
    
    private string currentWeapon = "Pistol"; // Setze hier den initialen Waffentyp
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool impactForceBool = false;
    private Quaternion initialRotation; // Speichert die Rotation des ursprünglichen Objekts
    private PlayerSwitcher playerManager;

    private GameObject hud;

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

        //hud = GameObject.Find("HUD").GetComponent<KillCounter>()
    }

    private void Update()
    {

    }

    /// <summary>
    /// defines what happens when player takes damage
    /// </summary>
    /// <param name="damage">damage that has been dealt</param>
    public void TakeDamage(int damage) 
    {
        SetCurrentHealth(GetCurrentHealth() - damage);
        Debug.Log("currentHealth " + GetCurrentHealth());
        StartCoroutine(HitFlash());
        playerHitSfx.Play();
        if (GetCurrentHealth() <= 0)
        {
            isDead = true;
            playerBody.color = Color.red;
            initialRotation = transform.rotation; // Speichere die Rotation des ursprünglichen Objekts
            Instantiate(deadPlayer, transform.position, initialRotation); // Verwende die gespeicherte Rotation
            Time.timeScale = 0.0f;
            Destroy(gameObject);
            GameObject.Find("HUD").GetComponent<Highscore>().SetHighscore();
            pauseMenu.GameOver();
            
        }
        impactForceBool = false;
    }
    /// <summary>
    /// increases the health
    /// </summary>
    /// <param name="value">health increase value</param>
    public void heal(int value) 
    {
        SetCurrentHealth(GetCurrentHealth() + value);
        StartCoroutine(HealFlash());
    }

    /// <summary>
    /// changes the weapon
    /// </summary>
    /// <param name="newWeapon">new weapon</param>
    public void ChangeWeapon(string newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Weapon changed to: " + currentWeapon);
    }
    
    /// <summary>
    /// returns current weapon
    /// </summary>
    /// <returns>current weapon</returns>
    public string GetCurrentWeapon()
    {
        return currentWeapon;
    }
    
/// <summary>
/// returns whether player is dead or not
/// </summary>
/// <returns></returns>
    public bool GetIsDead()
    {
        return isDead;
    }

/// <summary>
/// returns current health
/// </summary>
/// <returns>current health</returns>
    public int GetCurrentHealth()
    {
        return playerManager.playerClass.GetCurrentHealth();
    }
    /// <summary>
    /// returns max health
    /// </summary>
    /// <returns>max health</returns>
     public int GetMaxHealth(){
        return playerManager.playerClass.GetMaxHealth();
    }
    
    /// <summary>
    /// returns number of coins
    /// </summary>
    /// <returns>coins</returns>
    public int GetCoins()
    {
        return playerManager.playerClass.GetCurrency();
    }
    /// <summary>
    /// changes color of player when being hit
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// changes color of player when healing
    /// </summary>
    /// <returns></returns>
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
    

/// <summary>
/// sets the current health of player
/// </summary>
/// <param name="currentHealth">player health</param>
    public void SetCurrentHealth(int currentHealth){
        
        playerManager.playerClass.SetHealth(currentHealth);
        healthBar.SetHealth(currentHealth);
    }
/// <summary>
/// sets the current currency of the player
/// </summary>
/// <param name="value">number of coins</param>
    public void setCurrency(int value) {
        currency = currency + value;
        playerManager.playerClass.SetCurrency(currency);
        //Debug.Log("Currency: " + currency);
    }
/// <summary>
/// sets the max health of the player
/// </summary>
/// <param name="value">max health</param>
    public void setMaxHealth(int value) {
        healthBar.SetMaxHealth(maxHealth);
        SetMaxHealth(GetMaxHealth() + value);
        SetCurrentHealth(GetCurrentHealth() + value);
        Debug.Log($"CurrentHealth is {GetCurrentHealth()}");
    }
/// <summary>
/// sets the max health of the player
/// </summary>
/// <param name="value">max health</param>
    public void SetMaxHealth(int maxHealth)
    {
        playerManager.playerClass.SetMaxHealth(maxHealth);
        healthBar.SetMaxHealth(playerManager.playerClass.GetMaxHealth());
    }
}
