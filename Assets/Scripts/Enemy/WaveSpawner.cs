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

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;
    
    public float timeBetweenWaves = 7f;
    private float waveCountdown;
    public int waveTracker = 0;
    private float searchCountdown = 1f;
    public List<Enemys> bossEnemy;
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

    IEnumerator spawnWave(Wave _wave)
    {
        Debug.Log("Spawn Wave: " + _wave.waveName);
        state = spawnState.SPAWNING;
        int i = 0;
        SetEnemyCount(_wave);
        if (waveTracker % 10 == 0 && waveTracker != 0)
        {
            var rand = new System.Random();
            spawnEnemy(bossEnemy[rand.Next(0,bossEnemy.Count)].enemy);
        }
        
        while (i < _wave.enemyList.Count)
        {
            spawnEnemy(_wave.enemyList[i].enemy);
            _wave.enemyList[i].count -= 1;
            yield return new WaitForSeconds(1f/_wave.spawnRate);
            if (_wave.enemyList[i].count == 0)
            {
                i += 1;
            }
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
            state = spawnState.COMPLETE;
            Debug.Log("All Waves Complete!");
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

    public void SetNearestSpawnerToActive()
    {
        GameObject player = GameObject.FindWithTag("Player");
        foreach (var spawn in spawnPoints)
        {
            float dist = Vector3.Distance(spawn.position, player.transform.position);
            if(dist <= 20f) spawn.gameObject.SetActive(true);
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

    public bool IsAnySpawnerActive()
    {
        return spawnPoints.ToList().FindAll(x => x.gameObject.activeSelf).Count != 0;
    }

    void spawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);

        var spawner = GetActiveSpawnPoints();
        Transform _sp = spawner[Random.Range(0,spawner.Count)];
        Transform enemy = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemy.gameObject.GetComponent<EnemyHealth>().SetDamage(Mathf.RoundToInt(damageMultiplier.Evaluate(waveTracker)));
        enemy.gameObject.GetComponent<EnemyHealth>().AddHealth(Mathf.RoundToInt(healthMultiplier.Evaluate(waveTracker)));
        Debug.Log($"Enemy Health {enemy.gameObject.GetComponent<EnemyHealth>().maxHealth}");
    }
     public float GetWaveInfo (){
        return (int)waveCountdown;
    }


     public List<Transform> GetActiveSpawnPoints()
     {
         List<Transform> newSpawner = new List<Transform>();
         foreach (var spawn in spawnPoints)
         {
             if(spawn.gameObject.activeSelf) newSpawner.Add(spawn);
         }

         return newSpawner;
     }


     void SetEnemyCount(Wave _wave)
     {
         int enemyToSpawn = Mathf.RoundToInt(waveSpawner.Evaluate(waveTracker));
         Debug.Log($"Enemy Count {enemyToSpawn}");
         var rand = new System.Random();
         for (int i = 0; i < enemyToSpawn; i++)
         {
             var index = rand.Next(0, _wave.enemyList.Count());
             _wave.enemyList[index].count++;
         }
     }
   
}
