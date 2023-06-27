using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Nur eine Übergeordnete Klasse im Player findet der eigentliche attribute statt.
public class PlayerClass
{
    public int currentHealth;
    //public List<GameObject> prefabList;
    private GameObject prefab;
    public Transform position;
    private int expPoints;
    private int playerLevel;
    private int toLevelUp;
    private int currency;
    public int maxHealth;
    private float moveSpeed;
    public PlayerClass(int maxHealth, GameObject prefab, float moveSpeed)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.prefab = prefab;
        position = prefab.transform;
        playerLevel = 1;
        toLevelUp = 100;
        expPoints = 0;
        currency = 100;
        this.moveSpeed = moveSpeed;
    }


    void SwitchPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetPosition(Vector3 pos)
    {
        position.transform.position = pos;
    }

    public void SetHealth(int health)
    {
        this.currentHealth = health;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrency(int currency)
    {
        this.currency = currency;
    }

    public void SetMaxHealth(int health)
    {
        this.maxHealth = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void AddExpPoints(int exp)
    {
        expPoints += exp;
    }

    public void SetExpPoints(int exp)
    {
        expPoints = exp;
    }

    public int GetLevel()
    {
        return playerLevel;
    }
    
    public void hasLeveledUp()
    {
        // Check if player has reached the targetExperience points
        
        if (expPoints >= toLevelUp)
        {
            playerLevel++;
            Debug.Log($"Level: {playerLevel}");
            SetExpPoints(0);
            decimal multiplier = playerLevel / 10m;
            decimal toAdd = multiplier * toLevelUp;
            toLevelUp += (int)Math.Round(toAdd);
            Debug.Log($"Debug  Multiplier: {multiplier}  toAdd: {toAdd} levelUp: {toLevelUp} expPoints: {expPoints} ");
        }
    }
    
}
