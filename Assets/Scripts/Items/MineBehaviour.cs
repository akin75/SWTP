using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    private List<GameObject> enemiesInDamageArea = new List<GameObject>();
    public Collider2D trigger;
    public Collider2D damageArea;
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    public int damage = 50;
    public GameObject explosionSfx;
    private bool isCrit = false;
    private AchievementManager achievementManager;
    public bool _isLeveledUp = false;



    private void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager.GetMineLevelUp())
        {
            _isLeveledUp = true;
        }
        explosionParticles.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        smokeParticles.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (trigger.IsTouching(collision))
            {
                //Debug.Log("Trigger entered");
                Explode();
            }
        }
    }

    private void Explode()
    {
        //Debug.Log("Boom");
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Instantiate(explosionSfx, transform.position, Quaternion.identity);
        List<GameObject> enemiesToDamage = new List<GameObject>(enemiesInDamageArea);

        foreach (var enemy in enemiesToDamage)
        {
            if (enemy != null)
            {
                if (enemy.GetComponent<EnemyHealth>().getCurrentHealth() - damage <= 0)
                {
                    achievementManager.ZombieKilledByExplosion();
                }
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage, isCrit);
            }
        }

        enemiesInDamageArea.Clear();

        Destroy(gameObject);
    }


    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (damageArea.IsTouching(collision))
            {
                if (!enemiesInDamageArea.Contains(collision.gameObject))
                {
                    enemiesInDamageArea.Add(collision.gameObject);
                    //Debug.Log("Enemy entered DamageArea: " + collision.gameObject.name);
                }
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInDamageArea.Contains(collision.gameObject))
            {
                enemiesInDamageArea.Remove(collision.gameObject);
                //Debug.Log("Enemy exited DamageArea: " + collision.gameObject.name);
            }
        }
    }

    
    private void OnDestroy()
    {
        // Beim Zerstören der Mine den Status der Enemys im DamageArea-Kollider zurücksetzen
        foreach (var enemy in enemiesInDamageArea)
        {
            //Debug.Log("Enemy reset: " + enemy.name);
        }
        enemiesInDamageArea.Clear();
    }
}
