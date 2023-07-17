using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class Hud : MonoBehaviour
{   
    private PlayerSwitcher playerManager;
    private PlayerClass player;
    
    [System.Serializable]
    public class Weapon
    {    
        public GameObject pistol;
        public GameObject ar;
        public GameObject sg;

    }
    public Weapon weapon;

    private void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = playerManager.playerClass;
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerManager.GetCurrentPlayer().name)
        {
            case "CharacterPistol":
                weapon.ar.SetActive(false);
                weapon.sg.SetActive(false);
                weapon.pistol.SetActive(true);
                break;
            case "CharacterAR":
                weapon.pistol.SetActive(false);
                weapon.sg.SetActive(false);
                weapon.ar.SetActive(true);
                break;
            case "CharacterSG":
                weapon.pistol.SetActive(false);
                weapon.ar.SetActive(false);
                weapon.sg.SetActive(true);
                break;
            case "CharacterDualPistol":
                weapon.ar.SetActive(false);
                weapon.sg.SetActive(false);
                weapon.pistol.SetActive(true);
                break;
            default:
                Debug.Log("no valid character found");
                break;
        }
    }
}
