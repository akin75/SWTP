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

    private int _missedShots;
    private int _hitShots;
    private float _hitPercentage;
    private string _weaponType = "";

    public int requiredPistolKills = 10;
    public int requiredSgKills = 10;
    public int requiredArKills = 10;
    public int requiredDpKills = 10;
    public int requiredExplosionKills = 10;

    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _weaponType = _player.GetCurrentWeapon();
        _hitPercentage = CalculateHitPercentage();
        Debug.Log("accuracy percentage: " + (_hitPercentage * 100).ToString("0.00") + "%");
    }

    public void ZombieKilled()
    {
        _weaponType = GameObject.FindWithTag("Player").GetComponent<Player>().GetCurrentWeapon();
        if (_weaponType == "Pistol")
        {
            _zombiesKilledWithPistol++;
            CheckAchievement("Pistol_Killer", _zombiesKilledWithPistol, requiredPistolKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (_weaponType == "Shotgun")
        {
            _zombiesKilledWithSg++;
            CheckAchievement("Shotgun_Slayer", _zombiesKilledWithSg, requiredSgKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (_weaponType == "AssaultRifle")
        {
            _zombiesKilledWithAr++;
            CheckAchievement("AR_Master", _zombiesKilledWithAr, requiredArKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (_weaponType == "PistolR")
        {
            _zombiesKilledWithDp++;
            CheckAchievement("DualPistol_Destroyer", _zombiesKilledWithDp, requiredDpKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        Debug.Log("Achievement++: " + _weaponType);
    }

    public void ZombieKilledByExplosion()
    {
        _zombiesKilledWithExplosions++;
        CheckAchievement("Explosion_Expert", _zombiesKilledWithExplosions, requiredExplosionKills);
        Debug.Log("Achievement++: Explosion");
    }

    public void SpecialItemPickedUp(string specialitem)
    {
        if (specialitem == "A")
        {
            _pickedUpSpecialA++;
            CheckAchievement("Minigun", _pickedUpSpecialA, 4);
        }

        if (specialitem == "B")
        {
            _pickedUpSpecialB++;
            CheckAchievement("GranadeLauncher", _pickedUpSpecialB, 4);
        }
    }
    
    private void CheckAchievement(string achievementName, int progress, int requiredAmount)
    {
        if (progress >= requiredAmount)
        {
            // Hier kannst du den Spieler belohnen oder eine Benachrichtigung anzeigen, dass er das Achievement erhalten hat
            Debug.Log("Achievement unlocked: " + achievementName);
        }
    }

    public void shotMissed()
    {
        _missedShots++;
    }

    public void shotHit()
    {
        _hitShots++;
    }
    
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