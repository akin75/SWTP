using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public Collider2D baitArea;
    private List<GameObject> enemiesInBaitArea = new List<GameObject>();
    private EnemyController enemyController;
    public float baitTime = 3f;
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    public GameObject explosionSfx;
    public int damage = 50;
    private bool isCrit = false;
    private AchievementManager achievementManager;
    public bool _isLeveledUp = false;



    private Transform originalTarget; // Variable zur Speicherung des ursprünglichen Targets

    private void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager.GetBaitLevelUp())
        {
            _isLeveledUp = true;
        }
        if (_isLeveledUp)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
            // Vergrößern des GameObjects
            Vector3 newScale = transform.localScale * 1.5f;
            transform.localScale = newScale;
            // Vergrößern der Partikel
            explosionParticles.transform.localScale *= 1.5f;
            smokeParticles.transform.localScale *= 1.5f;
        }
        StartCoroutine(ExplodeAfterDelay(baitTime));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (baitArea.IsTouching(collision))
            {
                if (!enemiesInBaitArea.Contains(collision.gameObject))
                {
                    enemiesInBaitArea.Add(collision.gameObject);
                    Debug.Log("Enemy entered BaitArea: " + collision.gameObject.name);

                    // Reference the enemyController if not already referenced
                    if (enemyController == null)
                    {
                        enemyController = collision.gameObject.GetComponent<EnemyController>();
                        if (enemyController == null)
                        {
                            Debug.LogError("EnemyController not found!");
                        }
                        else
                        {
                            originalTarget = enemyController.GetTarget(); // Speichern des ursprünglichen Targets
                        }
                    }

                    BaitBehaviour();
                }
            }
        }
    }

    private void BaitBehaviour()
    {
        Transform tempTarget = originalTarget; // Temporäre Variable zur Speicherung des ursprünglichen Targets

        foreach (var enemy in enemiesInBaitArea)
        {
            if (enemyController != null && enemy != null)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                enemyController.SetTarget(gameObject.transform); // Verwende das temporäre Target für jeden Gegner
            }
        }
    }




    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Wiederherstellen des ursprünglichen Targets
        if (enemyController != null)
        {
            foreach (var enemy in enemiesInBaitArea)
            {
                if (enemyController != null && enemy != null)
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.SetTarget(originalTarget);
                }
            }
            Debug.Log("Target wieder player");
        }
        Destroy(gameObject);
    }
    
    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Instantiate(explosionSfx, transform.position, Quaternion.identity);

        List<GameObject> enemiesToDamage = new List<GameObject>(enemiesInBaitArea);

        
        // Wiederherstellen des ursprünglichen Targets
        if (enemyController != null)
        {
            foreach (var enemy in enemiesInBaitArea)
            {
                if (enemyController != null && enemy != null)
                {
                    if (enemy.GetComponent<EnemyHealth>().getCurrentHealth() - damage <= 0)
                    {
                        achievementManager.ZombieKilledByExplosion();
                    }
                    enemy.GetComponent<EnemyHealth>().TakeDamage(damage, isCrit);
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.SetTarget(originalTarget);
                }
            }
            Debug.Log("Target wieder player");
        }
        Destroy(gameObject);
    }
}
