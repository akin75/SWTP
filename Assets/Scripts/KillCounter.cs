using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class KillCounter : MonoBehaviour
{

    private int killsCounter = 0;

    [SerializeField]
    private TextMeshProUGUI killCounterText;

    private Transform mainCameraTransform;

    private EnemyHealth EnemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        killCounterText = GetComponent<TextMeshProUGUI>();
        UpdateKillCount();

        // Finde die Hauptkamera und speichere ihre Transform-Komponente
        mainCameraTransform = Camera.main.transform;

    }

    private void LateUpdate()
    {
        // Setze die Position des Textobjekts auf eine feste Position relativ zur Kamera
        transform.position = mainCameraTransform.position + mainCameraTransform.forward * 2f;
        transform.LookAt(mainCameraTransform);
    }

    public void IncreaseKillCount()
    {
            //Debug.Log("Increased Kill Count!");
            killsCounter++;
            UpdateKillCount();
    }

    public void UpdateKillCount()
    {
        //Debug.Log("Updated Kill Count!");
        killCounterText.text = "Kills: " + killsCounter.ToString();
    }
}
