using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab des Projektils
    public Transform firePoint; // Punkt, an dem das Projektil erscheinen soll
    public float projectileSpeed = 10f; // Geschwindigkeit des Projektils
    public float shootingInterval = 1f; // Zeitabstand zwischen den Schüssen
    public int damage;
    public Rigidbody2D rb;
    public Collider2D shootingRange;
    public Collider2D standingRange;
    public Player player;
    public GameObject spitSfx;

    private Transform playerTransform; // Referenz auf den Spieler
    private float lastShotTime; // Variable, um den letzten Zeitpunkt des Schusses zu speichern

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz erhalten
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        if (player != null)
        {
            InvokeRepeating("CheckShootingConditions", 0f, 0.1f); // Überprüfung der Schusslinie und des Schießens alle 0.1 Sekunden
        }
    }

    /// <summary>
    /// Checks wether enemy can shoot or not
    /// </summary>
    private void CheckShootingConditions()
    {
        if (!player.GetIsDead())
        {
          Vector2 direction = playerTransform.position - firePoint.position;
    
            LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
            enemyLayerMask = ~enemyLayerMask;
    
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, Mathf.Infinity, enemyLayerMask);
    
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                //Debug.DrawLine(firePoint.position, hit.point, Color.green, 0.1f);
    
                if (shootingRange.IsTouching(player.GetComponent<Collider2D>()) && Time.time - lastShotTime >= shootingInterval)
                {
                    //Debug.Log("Shooting Range");
                    ShootProjectile();
                    lastShotTime = Time.time;
                }
    
                if (standingRange.IsTouching(player.GetComponent<Collider2D>()) && !player.GetIsDead())
                {
                    //Debug.Log("Standing Range");
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    
/// <summary>
/// enemy cant shoot when player is not in range
/// </summary>
/// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shootingRange.IsTouching(other))
            {
                rb.constraints = RigidbodyConstraints2D.None;
                CancelInvoke("ShootProjectile");
                //Debug.Log("Player exited Shooting Range");
            }
        }
    }
/// <summary>
/// Shoots the projectile in the player direction
/// </summary>
    private void ShootProjectile()
    {
        //Collider2D[] result = new Collider2D[] { GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>() };

        //Debug.Log(hit.collider.IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()));
        //Debug.Log(hit.collider.GetContacts(result));
        Vector2 direction = playerTransform.position - firePoint.position;

        if (player.GetCurrentHealth() > 0)
        {
            // Erstelle ein Projektil und initialisiere es
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            if (!player.GetIsDead())
            {
                // Richtung zum Spieler berechnen
                direction = (playerTransform.position - firePoint.position).normalized;
                // Projektil in die berechnete Richtung schießen
                Instantiate(spitSfx, transform.position, Quaternion.identity);
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.velocity = direction * projectileSpeed;
            }
        }
    }
}