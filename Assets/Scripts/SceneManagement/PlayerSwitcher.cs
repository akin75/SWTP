/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PlayerSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    private GameObject currentPlayer;
    private UnityEngine.Vector3 playerPosition;
    private Quaternion playerRotation;
    private CameraController camController;
    public PlayerClass playerClass;
    private float damageAR;
    private Player player;
    [SerializeField] private AnimationCurve expController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        camController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerClass = new PlayerClass(currentPlayer.GetComponent<Player>().maxHealth, currentPlayer, currentPlayer.GetComponent<PlayerController>().moveSpeed, expController);
        camController.SetTarget(playerClass.position);
    }

    private void Update()
    {
        if (currentPlayer != null)
        {
            playerClass.SetPosition(currentPlayer.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&& playerClass.GetLevel() >= 2)
        {
            SwitchPlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerClass.GetLevel() >= 3)
        {
            SwitchPlayer(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchPlayer(3);
        }
    }
    
    
    /// <summary>
    /// Switch Player to another Prefab. 4 Different Player Prefabs to choose. Also Update the Player attribute to the Prefab
    /// </summary>
    /// <param name="index">Number to choose the Player Prefab</param>

    private void SwitchPlayer(int index)
    {
        if (index >= 0 && index < playerPrefabs.Length)
        {
            if (currentPlayer.gameObject.name == playerPrefabs[index].name)
            {
                return;
            }
            GameObject newPlayer = playerPrefabs[index];
            
            newPlayer.transform.position = currentPlayer.transform.position;
            newPlayer.transform.rotation = currentPlayer.transform.rotation;
            
            newPlayer.SetActive(true);
            currentPlayer.SetActive(false);
            
            currentPlayer = newPlayer;
            UpdatePlayer();
            var weaponName = newPlayer.GetComponentInChildren<Weapon>().gameObject.name.ToString();
            currentPlayer.GetComponent<Player>().ChangeWeapon(weaponName);
            camController.SetTarget(currentPlayer.transform);
        }
    }


    /// <summary>
    /// Update the player status. Necessarily for <c>SwitchPlayer</c> methods
    /// </summary>
    void UpdatePlayer()
    {
        var player = currentPlayer.GetComponent<Player>();
        currentPlayer.GetComponent<PlayerController>().moveSpeed = playerClass.GetMoveSpeed();
        player.SetMaxHealth(playerClass.GetMaxHealth());
        player.SetCurrentHealth(playerClass.GetCurrentHealth());
    }
    
    /// <summary>
    /// Gets the current Player
    /// </summary>
    /// <returns>The current Player</returns>

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
    
}