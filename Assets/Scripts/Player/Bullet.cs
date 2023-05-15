using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(CharacterController.damage);
            // Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}

/*
Raycast fehlgeschlagen 2 Probleme:
    1. Raycast ging nur bis zur Maus
    2. Raycast ging instant, die Kugel braucht jedoch einen Moment bis zum Objekt
*/