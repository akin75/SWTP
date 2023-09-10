using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    GameObject player;

    bool upgradeMenuActiv = false;

    public int healthValue;
    public int healthCost;
    public int damageValue;
    public int damageCost;
    public int moveValue;
    public int moveCost;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (upgradeMenuActiv)
            {
                resume();
            } 
            else
            {
                upgradeMenuUI.SetActive(true);
                Time.timeScale = 0f;
                upgradeMenuActiv = true;
            }
        }
    }

    void resume()
    {
        upgradeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        upgradeMenuActiv = false;
    }

    public void exitMenu()
    {
        resume();
    }

    public void HealthUpgrade() 
    {
        if (Player.currency >= healthCost) 
        {
            player.GetComponent<Player>().setMaxHealth(healthValue);
            player.GetComponent<Player>().setCurrency(-healthCost);
        }   
    }

    public void DamageUpgrade()
    {
        //if (Player.currency >= damageCost)
        //{
        //    player.GetComponent<Weapon>().SetDamage(damageValue);
        //    player.GetComponent<Player>().setCurrency(-damageCost);
        //}
    }

    public void MoveUpgrade()
    {
        if (Player.currency >= moveCost)
        {
            player.GetComponent<PlayerController>().SetMoveSpeed(10);
            player.GetComponent<Player>().setCurrency(-moveCost);
        }
    }
}
