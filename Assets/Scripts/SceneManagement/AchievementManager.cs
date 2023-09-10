using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Player _player;
    
    private int _zombiesKilledWithPistol = 0;
    private int _zombiesKilledWithSg = 0;
    private int _zombiesKilledWithAr = 0;
    private int _zombiesKilledWithDp = 0;
    private int _zombiesKilledWithExplosions = 0;
    private int _pickedUpSpecialA = 0;    
    private int _pickedUpSpecialB = 0;
    private bool _mineLeveledUp = false;
    private bool _baitLeveledUp = false;

    private int _missedShots;
    private int _hitShots;
    private float _hitPercentage;
    private string _weaponType = "";
    
    private float _zombieKillTimer = 0f;

    [SerializeField] private int requiredPistolKills = 10;
    [SerializeField] private int requiredPistolKills2 = 20;
    [SerializeField] private int requiredPistolKills3 = 30;
    [SerializeField] private int requiredSgKills = 10;
    [SerializeField] private int requiredArKills = 10;
    [SerializeField] private int requiredDpKills = 10;
    [SerializeField] private int requiredExplosionKills = 10;

    private float[] progPercent = new float[11];

    public float[] ComputeProgPercent(){
        progPercent[1] = _zombiesKilledWithPistol;
        progPercent[2] = _zombiesKilledWithPistol;
        progPercent[3] = _zombiesKilledWithPistol;
        progPercent[4] = _zombiesKilledWithExplosions;
        progPercent[5] = _zombiesKilledWithSg;
        progPercent[6] = _zombiesKilledWithAr;
        progPercent[7] = _zombiesKilledWithDp;
        progPercent[8] =_hitPercentage;
        progPercent[9] = _pickedUpSpecialA;
        progPercent[10] = _pickedUpSpecialB;      

        return progPercent;
    }

    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _weaponType = _player.GetCurrentWeapon();
        _hitPercentage = CalculateHitPercentage();
        if (_hitPercentage >= 90)
        {
            _player.GetComponentInChildren<Weapon>().EnablePerfectAccuracy();
        }
    }

    /// <summary>
    /// Handles the zombie kill achievements
    /// </summary>
    public void ZombieKilled()
    {
        _weaponType = GameObject.FindWithTag("Player").GetComponent<Player>().GetCurrentWeapon();
        if (_weaponType == "Pistol")
        {
            _zombiesKilledWithPistol++;
            CheckAchievement("Pistol_Killer", _zombiesKilledWithPistol, requiredPistolKills);
            CheckAchievement("Pistol_Killer_x2", _zombiesKilledWithPistol, requiredPistolKills2);
            CheckAchievement("Pistol_Killer_x3", _zombiesKilledWithPistol, requiredPistolKills3);
        }
        else if (_weaponType == "Shotgun")
        {
            _zombiesKilledWithSg++;
            CheckAchievement("Shotgun_Slayer", _zombiesKilledWithSg, requiredSgKills);
        }
        else if (_weaponType == "AssaultRifle")
        {
            _zombiesKilledWithAr++;
            CheckAchievement("AR_Master", _zombiesKilledWithAr, requiredArKills);
        }
        else if (_weaponType == "PistolR")
        {
            _zombiesKilledWithDp++;
            CheckAchievement("DualPistol_Destroyer", _zombiesKilledWithDp, requiredDpKills);
        }
    }

/// <summary>
/// handles the zombie kill with explosion achievements
/// </summary>
    public void ZombieKilledByExplosion()
    {
        _zombiesKilledWithExplosions++;
        CheckAchievement("Explosion_Expert", _zombiesKilledWithExplosions, requiredExplosionKills);
    }
    
/// <summary>
/// checks the achievements
/// </summary>
/// <param name="achievementName">checks which achievement has been finished</param>
/// <param name="progress">progress counter for achievement</param>
/// <param name="requiredAmount">required amount</param>
    private void CheckAchievement(string achievementName, int progress, int requiredAmount)
    {
        if (progress >= requiredAmount)
        {
            // Hier kannst du den Spieler belohnen oder eine Benachrichtigung anzeigen, dass er das Achievement erhalten hat
        }        
        if (achievementName == "Pistol_Killer")
        {
            // Dual Pistol freischalten
        } 
        else if (achievementName == "Pistol_Killer_x2")
        {
            _player.GetComponentInChildren<Weapon>().EnableDoubleFire();
        } 
        else if (achievementName == "Pistol_Killer_x3")
        {
            _player.GetComponentInChildren<Weapon>().EnableTripleFire();
        }
        else if (achievementName == "AR_Master")
        {
            _player.GetComponentInChildren<Weapon>().EnableInfiniteAmmo();
        }
        else if (achievementName == "Shotgun_Slayer")
        {
            _player.GetComponentInChildren<WeaponSG>().SetFirePointCount(8);
        }
        else if (achievementName == "DualPistol_Destroyer")
        {
            foreach (Weapon weaponComponent in _player.GetComponentsInChildren<Weapon>())
            {
                weaponComponent.EnableDoubleFire();
            }
        }
        else if (achievementName == "Explosion_Expert")
        {
            _mineLeveledUp = true;
            _baitLeveledUp = true;
        }
    }
/// <summary>
/// returns mine level
/// </summary>
/// <returns>mine level bool</returns>
    public bool GetMineLevelUp()
    {
        return _mineLeveledUp;
    }
/// <summary>
/// returns bait level
/// </summary>
/// <returns>bait level bool</returns>
    public bool GetBaitLevelUp()
    {
        return _baitLeveledUp;
    }

/// <summary>
/// incresed missedShots counter when bullet doesnt hit enemy
/// </summary>
    public void shotMissed()
    {
        _missedShots++;
    }
/// <summary>
/// increses hitShots timer when bullet hits enemy
/// </summary>
    public void shotHit()
    {
        _hitShots++;
    }
/// <summary>
/// calculates the percentage of hits
/// </summary>
/// <returns>hit accuracy</returns>
    private float CalculateHitPercentage()
    {
        int totalShots = _hitShots + _missedShots;
        if (totalShots > 0)
        {
            return (float)_hitShots / totalShots;
        }
        return 0f; // Wenn keine Schüsse abgefeuert wurden, ist die Trefferquote 0%
    }
}