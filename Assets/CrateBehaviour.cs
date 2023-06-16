using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public float health;
    private float playerDamage;
    private SpriteRenderer sprite;
    public ParticleSystem crateHit;
    public ParticleSystem crateDestroy;
    public GameObject coin;
    public GameObject heart;

    private void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().damage;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health = health - playerDamage;
            Instantiate(crateHit, transform.position, Quaternion.identity);
            StartCoroutine(HitEffect());

            if (health <= 0)
            {
                float randomValue = Random.value;
                if (randomValue <= 0.5f)
                {
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(heart, transform.position, Quaternion.identity);
                }
                Instantiate(crateDestroy, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator HitEffect()
    {
        Color originalColor = sprite.color; // Speichere die ursprüngliche Farbe des Sprites
        sprite.color = Color.grey;
        yield return new WaitForSeconds(0.05f);
        sprite.color = originalColor;
    }
}