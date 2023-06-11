using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSG : MonoBehaviour
{
    public ParticleSystem hitObjectParticles;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log(collision.gameObject);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSG>().damage);
            }
            else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
            {
                Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}