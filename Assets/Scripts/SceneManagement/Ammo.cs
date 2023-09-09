/* created by: SWT-P_SS_23_akin75 */

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

    void Update()
    {
        SetAmmoText();        
    }

    /// <summary>
    /// Check the used Weapontype and set the ammo text in the gui.
    /// </summary>
    public void SetAmmoText()
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
