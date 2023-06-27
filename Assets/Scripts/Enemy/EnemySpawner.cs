using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnRate = 2f;
    // spawnCount 0 = unendlich
    public int spawnCount = 0; 
    private bool endless = false;
    private float nextSpawnTime;
    private Player player;

    void Start()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (spawnCount == 0) 
        {
            spawnCount = 1;
            endless = true;
        }
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime && spawnCount > 0)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
            if (!endless) spawnCount = spawnCount - 1;
        }
    }

    private void SpawnEnemy()
    {
        //if (!player.GetIsDead())
        {
            // Position des Spawnpunkts (hier: das Zentrum des Spawners)
            Vector3 spawnPosition = transform.position;
    
            // Erzeugen des Feindes aus dem Prefab
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
