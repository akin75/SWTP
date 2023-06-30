using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public Collider2D baitArea;
    private List<GameObject> enemiesInBaitArea = new List<GameObject>();
    private EnemyController enemyController;
    public float baitTime = 3f;

    private Transform originalTarget; // Variable zur Speicherung des ursprünglichen Targets

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay(baitTime));
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
}
