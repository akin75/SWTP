using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public enum UpgradeType
    {
        damage,
        health,
        movespeed
    };
    public UpgradeType upgradeType;
    public int value;
    public int cost;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            switch (upgradeType)
            {
                case UpgradeType.movespeed:
                    if (Player.currency >= cost)
                    {
                        trigger.gameObject.GetComponent<PlayerController>().setMoveSpeed(value);
                        trigger.gameObject.GetComponent<Player>().setCurrency(-cost);
                    }
                    break;
                case UpgradeType.damage:
                    if (Player.currency >= cost)
                    {
                        trigger.gameObject.GetComponent<PlayerController>().setDamage(value);
                        trigger.gameObject.GetComponent<Player>().setCurrency(-cost);
                    }
                    break;
                case UpgradeType.health:
                    if (Player.currency >= cost) 
                    {
                        trigger.gameObject.GetComponent<Player>().setMaxHealth(value);
                        trigger.gameObject.GetComponent<Player>().setCurrency(-cost);
                    }   
                    break;
                default:
                    break;
            } 
        }
    }        
}
