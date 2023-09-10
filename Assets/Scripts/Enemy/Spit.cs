using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject hitSfx;
    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Ignoriere Kollisionen mit allen Objekten, die den Tag "Enemy" haben
        foreach (GameObject enemy in enemies)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider);
            }
        }
    }
/// <summary>
/// Handles the Collision of the spit object with another collider
/// </summary>
/// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit");
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Instantiate(hitSfx, transform.position, Quaternion.identity);
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyHealth>().damage);
            //Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}