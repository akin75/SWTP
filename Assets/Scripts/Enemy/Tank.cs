using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    private GameObject armor;

    // Start is called before the first frame update
    void Start()
    {
        armor = transform.Find("Armor").gameObject; // Finde das Child-Objekt "Armor"
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.currentHealth < enemyHealth.maxHealth * 0.5f)
        {
            Destroy(armor); // Zerstöre das gesamte Child-Objekt "Armor"
        }
    }
}