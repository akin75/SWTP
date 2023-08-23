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
    
    private float _zombieKillTimer = 0f;
    public int _requiredZombiesKilledInTime = 10;
    public float requiredZombieKillTime = 10f;
    private bool _zombieKillTimeAchievementCompleted = false;

    public int requiredPistolKills = 10;
    public int requiredPistolKills2 = 20;
    public int requiredPistolKills3 = 30;
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

        if (_zombieKillTimer > 0)
        {
            _zombieKillTimer -= Time.deltaTime;
            if (_zombieKillTimer <= 0)
            {
                // Zeit abgelaufen, setze den Timer zurück
                _zombieKillTimer = 0;
                _requiredZombiesKilledInTime = 10; // Setze die Anzahl der benötigten Zombies für das Achievement zurück
            }
        }
    }

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
        
        // Aktualisiere den Timer und die Anzahl der benötigten Zombies
        _zombieKillTimer = requiredZombieKillTime;
        _requiredZombiesKilledInTime--;

        // Überprüfe, ob das Achievement erfüllt ist
        CheckZombieKillTimeAchievement();
        
        Debug.Log("Achievement++: " + _weaponType);
    }
    
    private void CheckZombieKillTimeAchievement()
    {
        if (!_zombieKillTimeAchievementCompleted && _requiredZombiesKilledInTime <= 0)
        {
            // Hier kannst du den Spieler belohnen oder eine Benachrichtigung anzeigen, dass er das Achievement erhalten hat
            Debug.Log("Achievement unlocked: Zombie_Kill_Time");
        
            // Markiere das Achievement als abgeschlossen, damit es nicht erneut freigeschaltet werden kann
            _zombieKillTimeAchievementCompleted = true;
        }
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