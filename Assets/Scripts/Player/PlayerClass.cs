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
    private AnimationCurve lvlCurve;
    private float playerCritChance;
    private float playerCritDamage;
    public PlayerClass(int maxHealth, GameObject prefab, float moveSpeed, AnimationCurve lvlCurve)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.prefab = prefab;
        position = prefab.transform;
        this.lvlCurve = lvlCurve;
        playerLevel = 1;
        toLevelUp = Mathf.RoundToInt(this.lvlCurve.Evaluate(playerLevel));
        expPoints = 0;
        currency = 100;
        playerCritChance = 0.2f;
        playerCritDamage = 1.5f;
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
    
    public void SetCritChance(float value)
    {
        playerCritChance = value;
    }

    public float GetCritChance()
    {
        return playerCritChance;
    }
    
    public void SetCritDamage(float value)
    {
        playerCritDamage = value;
    }

    public float GetCritDamage()
    {
        return playerCritDamage;
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

    public void SetLevelCurve(int curve)
    {
        toLevelUp = curve;
    }

    public void SetCurrency(int currency)
    {
        this.currency = currency;
    }

    public void SetMaxHealth(int health)
    {
        this.maxHealth = health;
    }

    public int GetMaximumExp()
    {
        return toLevelUp;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentExp()
    {
        return expPoints;
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

    public void SetLevel(int level)
    {
        playerLevel = level;
    }
    
    public bool hasLeveledUp()
    {
        // Check if player has reached the targetExperience points
        
        if (expPoints >= toLevelUp)
        {
            playerLevel++;
            Debug.Log($"Level: {playerLevel}");
            SetExpPoints(0);
            SetLevelCurve(Mathf.RoundToInt(this.lvlCurve.Evaluate(GetLevel())));
            Debug.Log($"Debug  levelUp: {toLevelUp} expPoints: {expPoints} ");
            return true;
        }

        return false;
    }
}
