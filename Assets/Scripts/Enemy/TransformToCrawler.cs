using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformToCrawler : MonoBehaviour
{
    public GameObject crawlerPrefab;
    public GameObject deadEnemyPrefab;
    public float transformationChance = 10;
    private Quaternion enemyRotation;
    private Vector3 deathScale;
    
    /// <summary>
    /// Enemy has a small chance to transform into a crawler when destroyed
    /// </summary>
    public void Transformation()
    {
        enemyRotation = transform.rotation;
        if (transformationChance >= Random.Range(0, 100)) 
        {
            Instantiate(crawlerPrefab, transform.position, enemyRotation);
        }
        else
        {
            GameObject deadZombie = Instantiate(deadEnemyPrefab, transform.position, enemyRotation);
            deadZombie.transform.localScale = deathScale;
        }
    }
}
