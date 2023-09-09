/* created by: SWT-P_SS_23_akin75 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaveSpawner : MonoBehaviour
{

    public enum spawnState { SPAWNING, WAITING, COUNTING, COMPLETE };


    [System.Serializable]
    public class Wave
    {    
        public string waveName;     
        public List<Enemys> enemyList = new List<Enemys>();
        public float spawnRate;
        public int waveToSpawn;

    }

    [System.Serializable]
    public class Enemys
    {
        public Transform enemy;
        public int count;
    }

    [SerializeField] private Wave[] waves;
    private int nextWave = 0;

    [SerializeField] private Transform[] spawnPoints;
    
    public float timeBetweenWaves = 7f;
    private float waveCountdown;
    public int waveTracker = 0;
    private float searchCountdown = 1f;
    [SerializeField] private List<Enemys> bossEnemy;
    [SerializeField] private AnimationCurve waveSpawner;
    [SerializeField] private AnimationCurve damageMultiplier;
    [SerializeField] private AnimationCurve healthMultiplier;

    public spawnState state = spawnState.COUNTING;

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
        if (state == spawnState.COMPLETE)
        {
            return;
        }
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
            SetNearestSpawnerToActive();
            waveCountdown -= Time.deltaTime;
            
        }
    }

    /// <summary>
    /// Check if there are enemys alive
    /// </summary>
    /// <returns>true or false if still enemys alive</returns>
    public bool enemyIsAlive()
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

    /// <summary>
    ///  Spawn a wave of enemy, can also spawn Boss enemy.
    /// </summary>
    /// <param name="_wave"></param>
    /// <returns></returns>
    IEnumerator spawnWave(Wave _wave)
    {
        state = spawnState.SPAWNING;
        int i = 0;
        SetEnemyCount(_wave);
        int enemyToSpawn = Mathf.RoundToInt(waveSpawner.Evaluate(waveTracker));

        if (waveTracker % 1 == 0 && waveTracker != 0)
        {
            var spawner = GetActiveSpawnPoints();
            var boss = Random.Range(0, bossEnemy.Count);
            spawnEnemy(bossEnemy[i].enemy, spawner[Random.Range(0,spawner.Count)]);
            i++;
        }
        
        while (enemyToSpawn > 0)
        {
            var spawner = GetActiveSpawnPoints();
            var enemyList = _wave.enemyList.OrderBy(x => x.count != 0).ToList();
            foreach (var _sp in spawner)
            {
                var rand = Random.Range(0, enemyList.Count);
                spawnEnemy(enemyList[rand].enemy, _sp);
                var index = _wave.enemyList.FindIndex(x => x.enemy == enemyList[rand].enemy);
                _wave.enemyList[index].count--;
                enemyToSpawn--;
                if (enemyToSpawn <= 0) break;
            }

            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }
        state = spawnState.WAITING;
        yield break;
    }

    /// <summary>
    /// Check if wave is completed
    /// </summary>
    void waveCompleted()
    {
        state = spawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            state = spawnState.COMPLETE;
        }
        else
        {
            if (waves[nextWave].waveToSpawn != 0)
            {
                waves[nextWave].waveToSpawn--;
            }
            else
            {
                
                nextWave++;
            }
            
            waveTracker++;
        }
    }

    /// <summary>
    /// Check for the nearest Spawner to the Player and set it active.
    /// </summary>
    public void SetNearestSpawnerToActive()
    {
        GameObject player = GameObject.FindWithTag("Player");
        foreach (var spawn in spawnPoints)
        {
            float dist = Vector3.Distance(spawn.position, player.transform.position);
            if(dist <= 50f) spawn.gameObject.SetActive(true);
            else
            {
                spawn.gameObject.SetActive(false);
            }
        }

        if (!IsAnySpawnerActive())
        {
            List<(int, float)> dist = new List<(int, float)>();
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                dist.Add((i, Vector3.Distance(spawnPoints[i].position, player.transform.position)));
                
            }

            var t = dist.OrderBy(x => x.Item2).Take(3);
            var asList = t.ToList();
            foreach (var a in asList)
            {
                spawnPoints[a.Item1].gameObject.SetActive(true);
            }
        }
        
    }

    /// <summary>
    /// Check if any of the spawner is active
    /// </summary>
    /// <returns>boolean statement if any spawner is active</returns>
    public bool IsAnySpawnerActive()
    {
        return spawnPoints.ToList().FindAll(x => x.gameObject.activeSelf).Count != 0;
    }

    /// <summary>
    /// Spawn an enemy. Adjust health and damage according to the level
    /// </summary>
    /// <param name="_enemy"></param>
    /// <param name="_spawner"></param>
    void spawnEnemy(Transform _enemy, Transform _spawner)
    {
        Transform enemy = Instantiate(_enemy, _spawner.position, _spawner.rotation);
        enemy.gameObject.GetComponent<EnemyHealth>().SetDamage(Mathf.RoundToInt(damageMultiplier.Evaluate(waveTracker)));
        enemy.gameObject.GetComponent<EnemyHealth>().AddHealth(Mathf.RoundToInt(healthMultiplier.Evaluate(waveTracker)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
     public float GetWaveInfo (){
        return (int)waveCountdown;
    }
     
    /// <summary>
    /// Get the active Spawn points
    /// </summary>
    /// <returns></returns>
     public List<Transform> GetActiveSpawnPoints()
     {
         List<Transform> newSpawner = new List<Transform>();
         foreach (var spawn in spawnPoints)
         {
             if(spawn.gameObject.activeSelf) newSpawner.Add(spawn);
         }

         return newSpawner;
     }

    /// <summary>
    /// Set enemy count to spawn, this function will be necessarily for spawnWave
    /// </summary>
    /// <param name="_wave"></param>
     void SetEnemyCount(Wave _wave)
     {
         int enemyToSpawn = Mathf.RoundToInt(waveSpawner.Evaluate(waveTracker));
         var rand = new System.Random();
         for (int i = 0; i < enemyToSpawn; i++)
         {
             var index = rand.Next(0, _wave.enemyList.Count);
             _wave.enemyList[index].count++;
         }
     }
   
   /// <summary>
   /// 
   /// </summary>
   /// <returns></returns>
   public int GetWaveCounter()
   {
    return waveTracker + 1;
   }
}
