using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitObjectParticles;
    public GameObject zombieHitSfx;
    public GameObject zombieMissSfx;
    public GameObject woodHitSfx;
    public GameObject metalHitSfx;
    public GameObject damagePopupPrefab;
    private int _baseDamage;
    private int _adjustedDamage;
    private string _currentWeapon;

    public float critChance = 0.2f; // Crit Chance von 20%
    private bool isCrit = false;

    private void Start()
    {
        _currentWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetCurrentWeapon();
        _baseDamage = GetWeaponDamage();
        _adjustedDamage = Mathf.RoundToInt(ApplyDamageVariation(_baseDamage));
        
        // Überprüfe, ob der Treffer ein kritischer Treffer ist
        isCrit = Random.value <= critChance;
        if (isCrit)
        {
            _adjustedDamage = Mathf.RoundToInt(_adjustedDamage * 1.5f); // Kritischer Treffer erhöht den Schaden um 50%
        }
    }

    private int GetWeaponDamage()
    {
        _currentWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetCurrentWeapon();
        if (GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>().GetCurrentPlayer().name == "CharacterSG")
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSG>().damage;
        }
        else
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
        }
    }

    public bool GetIsCrit()
    {
        return isCrit;
    }

    public int GetAdjustedDamage()
    {
        return _adjustedDamage;
    }

    private float ApplyDamageVariation(float damage)
    {
        float minDamage = damage * 0.8f; // 80% of the original damage
        float maxDamage = damage * 1.2f; // 120% of the original damage
        return Random.Range(minDamage, maxDamage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GetAdjustedDamage(), isCrit);
            GameObject zombieHitSfxInstance = Instantiate(this.zombieHitSfx, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject woodHitSfxInstance = Instantiate(this.woodHitSfx, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Metal"))
        {
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject metalHitSfxInstance = Instantiate(this.metalHitSfx, transform.position, Quaternion.identity);
        }
        else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            GameObject zombieMissSfxInstance = Instantiate(this.zombieMissSfx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
