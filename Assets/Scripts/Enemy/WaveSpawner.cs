using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum spawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int spawnCount;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private spawnState state = spawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points!");
        }
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == spawnState.WAITING) 
        {
            if (!enemyIsAlive())
            {
                waveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0) 
        {
            if (state != spawnState.SPAWNING)
            {
                StartCoroutine(spawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool enemyIsAlive()
    {   
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator spawnWave(Wave _wave)
    {
        Debug.Log("Spawn Wave: " + _wave.waveName);
        state = spawnState.SPAWNING;

        for (int i=0; i < _wave.spawnCount; i++)
        {
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.spawnRate);
        }

        state = spawnState.WAITING;
        yield break;
    }

    void waveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = spawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Complete! Looping...");
        }
        else
        {
            nextWave++;
        }
    }

    void spawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
