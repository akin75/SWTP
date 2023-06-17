using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Array der Player-Prefabs
    private GameObject currentPlayer; // Referenz auf den aktuellen Player
    private UnityEngine.Vector3 playerPosition;
    private Quaternion playerRotation;
    private CameraController camController;

    private float damageAR;
    //public EnemyController enemController;

    private void Start()
    {
        camController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        // Den ersten Player auswählen
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Player wechseln basierend auf den Tasteneingaben
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchPlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchPlayer(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchPlayer(3);
        }
    }

    private void SwitchPlayer(int index)
    {
        // Überprüfen, ob der Index gültig ist
        if (index >= 0 && index < playerPrefabs.Length)
        {
            // Den aktuellen Player zerstören, falls vorhanden
            if (currentPlayer != null)
            {
                playerPosition = currentPlayer.transform.position;
                playerRotation = currentPlayer.transform.rotation;
                Destroy(currentPlayer);
            }

            // Das neue Player-Prefab instanziieren und als aktuellen Player setzen
            currentPlayer = Instantiate(playerPrefabs[index], playerPosition, playerRotation);
            camController.SetTarget(currentPlayer.transform);
            //enemController.SetTarget(currentPlayer.transform);
        }
    }
}
