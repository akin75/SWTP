using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyHealth>().damage);
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}