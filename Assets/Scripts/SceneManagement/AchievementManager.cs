using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private int zombiesKilledWithPistol = 0;
    private int zombiesKilledWithSg = 0;
    private int zombiesKilledWithAr = 0;
    private int zombiesKilledWithDp = 0;
    private int zombiesKilledWithExplosions = 0; // Neues Achievement für Zombies getötet durch Explosionen

    public int requiredPistolKills = 10;
    public int requiredSgKills = 10;
    public int requiredArKills = 10;
    public int requiredDpKills = 10;


    // Funktion, die aufgerufen wird, wenn ein Zombie getötet wird
    public void ZombieKilled(string weaponType)
    {
        if (weaponType == "Pistol")
        {
            zombiesKilledWithPistol++;
            CheckAchievement("Pistol_Killer", zombiesKilledWithPistol, requiredPistolKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (weaponType == "Shotgun")
        {
            zombiesKilledWithSg++;
            CheckAchievement("Shotgun_Slayer", zombiesKilledWithSg, requiredSgKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (weaponType == "AR")
        {
            zombiesKilledWithAr++;
            CheckAchievement("AR_Master", zombiesKilledWithAr, requiredArKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        else if (weaponType == "DP")
        {
            zombiesKilledWithDp++;
            CheckAchievement("DualPistol_Destroyer", zombiesKilledWithDp, requiredDpKills); // Name des Achievements, Fortschritt, benötigte Anzahl
        }
        Debug.Log("Achievement ++");
    }

    public void ZombieKilledByExplosion()
    {
        zombiesKilledWithExplosions++;
        CheckAchievement("Explosion_Expert", zombiesKilledWithExplosions, 10); // Name des Achievements, Fortschritt, benötigte Anzahl
    }
    
    // Funktion, die überprüft, ob ein Achievement erreicht wurde
    private void CheckAchievement(string achievementName, int progress, int requiredAmount)
    {
        if (progress >= requiredAmount)
        {
            // Hier kannst du den Spieler belohnen oder eine Benachrichtigung anzeigen, dass er das Achievement erhalten hat
            Debug.Log("Achievement unlocked: " + achievementName);
        }
    }
}