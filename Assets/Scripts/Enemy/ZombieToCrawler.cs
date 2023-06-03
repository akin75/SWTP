using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieToCrawler : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    void Start()
    {
        
    }

    private void Resurrection()
    {
        enemyHealth.currentHealth = 50;
    }

}
