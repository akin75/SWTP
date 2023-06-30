using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitObjectParticles;
    public GameObject zombieHitSfx;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage);
            GameObject zombieHitSfx = Instantiate(this.zombieHitSfx, transform.position, Quaternion.identity);
        }
        else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitObjectParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}