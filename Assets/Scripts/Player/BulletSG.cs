using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSG : MonoBehaviour
{
    public ParticleSystem hitObjectParticles;
    
    private PlayerClass _playerManager;

    private int _baseDamage;
    private int _adjustedDamage;

    public float critChance = 0.2f; // Crit Chance von 20%
    private bool isCrit = false;
    private float critDamage = 1.5f;

    private void Start()
    {
        _playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>().playerClass;
        _baseDamage = GetWeaponDamage();
        _adjustedDamage = Mathf.RoundToInt(ApplyDamageVariation(_baseDamage));

        // Überprüfe, ob der Treffer ein kritischer Treffer ist
        isCrit = Random.value <= _playerManager.GetCritChance();
        if (isCrit)
        {
            _adjustedDamage = Mathf.RoundToInt(_adjustedDamage * _playerManager.GetCritDamage()); // Kritischer Treffer erhöht den Schaden um 50%
        }
    }
    /// <summary>
    /// returns weapon damage
    /// </summary>
    /// <returns>weapon damage</returns>
    private int GetWeaponDamage()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSG>().damage;
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
    /// returns adjusted damage
    /// </summary>
    /// <returns>adjusted damage</returns>
    public int GetAdjustedDamage()
    {
        return _adjustedDamage;
    }
    
    /// <summary>
    /// defines what happens when bullet hits an enemy or an object
    /// </summary>
    /// <param name="collision">object that has been hit</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log(collision.gameObject);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<EnemyHealth>();
                if (enemy.currentHealth > 0)
                {
                    enemy.TakeDamage(_adjustedDamage, isCrit);
                }
            }
            else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
            {
                Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}