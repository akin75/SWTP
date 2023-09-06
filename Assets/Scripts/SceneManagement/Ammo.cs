using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    private PlayerSwitcher playerManager;
    private GameObject player;
    private Weapon weapon;
    private WeaponSG weaponSG;
 
    [SerializeField] private TMP_Text ammo;

    private void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (playerManager.GetCurrentPlayer().name == "CharacterSG")
        {
            weaponSG = player.GetComponentInChildren<WeaponSG>();
            ammo.SetText(""+weaponSG.GetAmmo());
        }
        else
        {
            weapon = player.GetComponentInChildren<Weapon>();
            ammo.SetText(""+weapon.GetAmmo());
        }
        
    }
}
