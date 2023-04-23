using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnRate = 2f;
    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        // Position des Spawnpunkts (hier: das Zentrum des Spawners)
        Vector3 spawnPosition = transform.position;

        // Erzeugen des Feindes aus dem Prefab
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
}
