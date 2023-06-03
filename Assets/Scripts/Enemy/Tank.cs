using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    private SpriteRenderer armorSprite;

    // Start is called before the first frame update
    void Start()
    {
        Transform armorTransform = transform.Find("Armor"); // Finde das Kindobjekt "Armor"
        if (armorTransform != null)
        {
            armorSprite = armorTransform.GetComponent<SpriteRenderer>(); // Erhalte den SpriteRenderer des Kindobjekts "Armor"
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (armorSprite != null && enemyHealth.currentHealth < enemyHealth.maxHealth * 0.5f)
        {
            Destroy(armorSprite);
        }
    }
}
