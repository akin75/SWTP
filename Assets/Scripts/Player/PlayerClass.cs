/* created by: SWT-P_SS_23_akin75 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Nur eine Übergeordnete Klasse im Player findet der eigentliche attribute statt.
public class PlayerClass
{
    public int currentHealth;
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
    
    
    /// <summary>
    /// PlayerClass constructor
    /// </summary>
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
    
    
    /// <summary>
    /// Classical Setter function, sets the move speed of the playerclass
    /// </summary>
    /// <param name="value">Value of movement speed to set</param>

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }
    
    /// <summary>
    /// Classical Getter function, gets the move speed of the playerclass
    /// </summary>
    /// <returns>PlayerClass move speed</returns>
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    /// <summary>
    /// Classical Setter function, sets the critical chance of the playerclass
    /// </summary>
    /// <param name="value">Value of the critical chance to set</param>
    public void SetCritChance(float value)
    {
        playerCritChance = value;
    }

    /// <summary>
    /// Classical Getter function, gets the critical chance of the playerclass
    /// </summary>
    /// <returns>PlayerClass critical chance</returns>
    public float GetCritChance()
    {
        return playerCritChance;
    }
    /// <summary>
    /// Classical Setter function, sets the move speed of the playerclass
    /// </summary>
    /// <param name="value">Value of critical damage to set</param>
    public void SetCritDamage(float value)
    {
        playerCritDamage = value;
    }

    /// <summary>
    /// Classical Getter function, gets the critical damage of the playerclass
    /// </summary>
    /// <returns>PlayerClass critical damage</returns>
    public float GetCritDamage()
    {
        return playerCritDamage;
    }
    /// <summary>
    /// Classical Setter function, sets the position of the playerclass
    /// </summary>
    /// <param name="pos">Position to set</param>
    public void SetPosition(Vector3 pos)
    {
        position.transform.position = pos;
    }
    /// <summary>
    /// Classical Setter function, sets the health of the playerclass
    /// </summary>
    /// <param name="health">Value of the health to set</param>
    public void SetHealth(int health)
    {
        this.currentHealth = health;
    }

    /// <summary>
    /// Classical Getter function, gets the current health of the playerclass
    /// </summary>
    /// <returns>PlayerClass current health</returns>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    /// <summary>
    /// Classical Setter function, sets the level curve of the playerclass
    /// </summary>
    /// <param name="curve">Value of the curve to set</param>
    public void SetLevelCurve(int curve)
    {
        toLevelUp = curve;
    }
    /// <summary>
    /// Classical Setter function, sets currency of the playerclass
    /// </summary>
    /// <param name="currency">Value of the currency to set</param>
    public void SetCurrency(int currency)
    {
        this.currency = currency;
    }
    /// <summary>
    /// Classical Setter function, sets max health of the playerclass
    /// </summary>
    /// <param name="health">Value of the  max health to set</param>
    public void SetMaxHealth(int health)
    {
        this.maxHealth = health;
    }

    /// <summary>
    /// Classical Getter function, gets the experience points to level up of the playerclass
    /// </summary>
    /// <returns>PlayerClass experience points needed to level up</returns>
    public int GetMaximumExp()
    {
        return toLevelUp;
    }
    /// <summary>
    /// Classical Getter function, gets the maximum health of the playerclass
    /// </summary>
    /// <returns>PlayerClass maximum health</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    /// <summary>
    /// Classical Getter function, gets the current experience point of the playerclass
    /// </summary>
    /// <returns>PlayerClass current experience point</returns>

    public int GetCurrentExp()
    {
        return expPoints;
    }

    /// <summary>
    /// Classical Getter function, gets the currency of the playerclass
    /// </summary>
    /// <returns>PlayerClass currency</returns>
    public int GetCurrency()
    {
        return currency;
    }

    /// <summary>
    /// Add experience points to the attribute expPoints
    /// </summary>
    /// <param name="exp"> Value to add to the expPoints variable</param>
    public void AddExpPoints(int exp)
    {
        expPoints += exp;
    }
    /// <summary>
    /// Classical Setter function, sets the experience point of the playerclass
    /// </summary>
    /// <param name="exp">Value of the experience points to set</param>
    public void SetExpPoints(int exp)
    {
        expPoints = exp;
    }
    /// <summary>
    /// Classical Getter function, gets the level of the playerclass
    /// </summary>
    /// <returns>PlayerClass level</returns>

    public int GetLevel()
    {
        return playerLevel;
    }
    /// <summary>
    /// Classical Setter function, sets the level of the playerclass
    /// </summary>
    /// <param name="level">Value of the level to set</param>
    public void SetLevel(int level)
    {
        playerLevel = level;
    }
    
    /// <summary>
    ///     Check if the player has reached to experience points to level up
    ///     if so level up the player and sets the experience points to zero
    /// </summary>
    /// <returns>Boolean statement if the player experience points is bigger or equal to toLevelUp value</returns>
    
    public bool hasLeveledUp()
    {
        if (expPoints >= toLevelUp)
        {
            playerLevel++;
            SetExpPoints(0);
            SetLevelCurve(Mathf.RoundToInt(this.lvlCurve.Evaluate(GetLevel())));
            return true;
        }

        return false;
    }
}
