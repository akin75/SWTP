using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Array der Player-Prefabs
    private GameObject currentPlayer; // Referenz auf den aktuellen Player
    private UnityEngine.Vector3 playerPosition;
    private Quaternion playerRotation;
    private CameraController camController;
    public PlayerClass playerClass;
    private float damageAR;
    public EnemyController enemController;

    private void Awake()
    {
        camController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        // Den ersten Player auswählen
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        //enemController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        playerClass = new PlayerClass(currentPlayer.GetComponent<Player>().maxHealth, currentPlayer, currentPlayer.GetComponent<PlayerController>().moveSpeed);
        camController.SetTarget(playerClass.position);
        
        //enemController.SetTarget(playerClass.position);
    }

    private void Update()
    {
        // Player wechseln basierend auf den Tasteneingaben
        playerClass.hasLeveledUp();
        if (currentPlayer != null)
        {
            playerClass.SetPosition(currentPlayer.transform.position);
            //Debug.Log("Position: " + playerClass.position.position);
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
/*
    private void SwitchPlayer(int index)
    {
        // Überprüfen, ob der Index gültig ist
        GameObject newPlayer;
        if (index >= 0 && index < playerPrefabs.Length)
        {
            // Den aktuellen Player zerstören, falls vorhanden
            if (currentPlayer != null)
            {
                playerPosition = currentPlayer.transform.position;
                playerRotation = currentPlayer.transform.rotation;
            }
            
            // Das neue Player-Prefab instanziieren und als aktuellen Player setzen
            currentPlayer.SetActive(false);
            int health = currentPlayer.GetComponent<Player>().currentHealth;
            currentPlayer = transform.GetComponentInChildren<Transform>().Find(playerPrefabs[index].name).gameObject;
            currentPlayer.SetActive(true);
            currentPlayer.transform.position = playerPosition;
            currentPlayer.transform.rotation = playerRotation;
            currentPlayer.GetComponent<Player>().currentHealth = health;
            currentPlayer.GetComponent<Player>().healthBar.SetHealth(health);

            //playerClass.SetPosition(currentPlayer.transform.position);
            //camController.SetTarget(currentPlayer.transform);
            //enemController.SetTarget(currentPlayer.transform);
        }
    }
    */
    private void SwitchPlayer(int index)
    {
        // Überprüfen, ob der Index gültig ist
        if (index >= 0 && index < playerPrefabs.Length)
        {
            if (currentPlayer.gameObject.name == playerPrefabs[index].name)
            {
                return;
            }
            // Das neue Player-Prefab instanziieren und als neuen Player setzen
            GameObject newPlayer = playerPrefabs[index];
            
            // Die Position und Rotation des aktuellen Players auf den neuen Player übertragen
            newPlayer.transform.position = currentPlayer.transform.position;
            newPlayer.transform.rotation = currentPlayer.transform.rotation;

            // Aktiviere den neuen Player und deaktiviere den alten Player
            newPlayer.SetActive(true);
            currentPlayer.SetActive(false);

            // Den neuen Player als den aktuellen Player setzen
            currentPlayer = newPlayer;
            UpdatePlayer();
            // Kameracontroller auf den neuen Player ausrichten
            camController.SetTarget(currentPlayer.transform);
        }
    }


    void UpdatePlayer()
    {
        var player = currentPlayer.GetComponent<Player>();
        currentPlayer.GetComponent<PlayerController>().moveSpeed = playerClass.GetMoveSpeed();
        player.SetMaxHealth(playerClass.GetMaxHealth());
        player.SetCurrentHealth(playerClass.GetCurrentHealth());
        
    }

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
    
}