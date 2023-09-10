using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitObjectParticles;
    [SerializeField] private GameObject zombieHitSfx;
    [SerializeField] private GameObject zombieMissSfx;
    [SerializeField] private GameObject woodHitSfx;
    [SerializeField] private GameObject metalHitSfx;
    [SerializeField] private GameObject damagePopupPrefab;
    private int _baseDamage;
    private int _adjustedDamage;
    private AchievementManager _achievementManager;
    private bool _boolZombieKilled;
    private string _weaponType = "";
    private GameObject _player;
    private PlayerClass _playerManager;

    [SerializeField] private float critChance = 0.2f; // Crit Chance von 20%
    private bool isCrit = false;
    private float critDamage = 1.5f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>().playerClass;
        _achievementManager = FindObjectOfType<AchievementManager>();
        _baseDamage = GetWeaponDamage();
        _adjustedDamage = Mathf.RoundToInt(ApplyDamageVariation(_baseDamage));
        
        // Überprüfe, ob der Treffer ein kritischer Treffer ist
        isCrit = Random.value <= _playerManager.GetCritChance();
        if (isCrit)
        {
            _adjustedDamage = Mathf.RoundToInt(_adjustedDamage * _playerManager.GetCritDamage()); // Kritischer Treffer erhöht den Schaden um 50%
        }
    }

    private void Update()
    {
        _weaponType = _player.GetComponent<Player>().GetCurrentWeapon();
    }

    /// <summary>
    /// gets weapon damage
    /// </summary>
    /// <returns>weapon damage</returns>
    private int GetWeaponDamage()
    {
        _weaponType = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetCurrentWeapon();
        if (GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>().GetCurrentPlayer().name == "CharacterSG")
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSG>().damage;
        }
        else
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
        }
    }

    /// <summary>
    /// gets crit bool
    /// </summary>
    /// <returns></returns>
    public bool GetIsCrit()
    {
        return isCrit;
    }
    
/// <summary>
/// gets adjusted damage
/// </summary>
/// <returns>ajdusted damage</returns>
    public int GetAdjustedDamage()
    {
        return _adjustedDamage;
    }
/// <summary>
/// Applies damage variation
/// </summary>
/// <param name="damage">base damage</param>
/// <returns>adjusted damage</returns>
    private float ApplyDamageVariation(float damage)
    {
        float minDamage = damage * 0.8f; // 80% of the original damage
        float maxDamage = damage * 1.2f; // 120% of the original damage
        return Random.Range(minDamage, maxDamage);
    }
/// <summary>
/// defines what happens when bullet hits an enemy or an object
/// </summary>
/// <param name="collision">object that has been hit</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _achievementManager.shotHit();
            if (collision.gameObject.GetComponent<EnemyHealth>().getCurrentHealth() - GetAdjustedDamage() <= 0)
            {
                _achievementManager.ZombieKilled();
            }
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GetAdjustedDamage(), isCrit);
            GameObject zombieHitSfxInstance = Instantiate(this.zombieHitSfx, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            _achievementManager.shotMissed();
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject woodHitSfxInstance = Instantiate(this.woodHitSfx, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Metal"))
        {
            _achievementManager.shotMissed();
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject metalHitSfxInstance = Instantiate(this.metalHitSfx, transform.position, Quaternion.identity);
        }
        else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
        {
            _achievementManager.shotMissed();
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject zombieMissSfxInstance = Instantiate(this.zombieMissSfx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
