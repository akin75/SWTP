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

    private Transform playerTransform; // Referenz auf den Spieler

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz erhalten
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 direction = playerTransform.position - firePoint.position;
        var toFilter = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, toFilter);

        if (hit.collider.IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()))
        {
            Debug.Log("Player Found");
            if (collision.CompareTag("Player") && player != null)
            {
                if (shootingRange.IsTouching(collision))
                {
                    Debug.Log("Shooting Range");
                    InvokeRepeating("ShootProjectile", shootingInterval, shootingInterval);
                }

                if (standingRange.IsTouching(collision) && !player.GetIsDead())
                {
                    Debug.Log("Standing Range");
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                }
            }
        }
        else
        {
            Debug.DrawRay(firePoint.position, direction.normalized * 10f, Color.red, 0.1f);
            //Debug.Log("Missed Player");
        }
    }

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

    private void ShootProjectile()
    {
        //Collider2D[] result = new Collider2D[] { GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>() };

        //Debug.Log(hit.collider.IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()));
        //Debug.Log(hit.collider.GetContacts(result));
        Vector2 direction = playerTransform.position - firePoint.position;
        //List<Collider2D> res = new List<Collider2D>();
        //filter.IsFilteringLayerMask(GameObject.FindGameObjectWithTag("Player"));
        //var test = hit.collider.OverlapCollider(filter, res);
        //Debug.Log(hit.collider.IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()));
        //Debug.Log(hit.collider);
        //    Debug.DrawLine(firePoint.position, hit.point, Color.green, 0.1f);

        if (player.GetCurrentHealth() > 0)
        {
            // Erstelle ein Projektil und initialisiere es
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            if (!player.GetIsDead())
            {
                // Richtung zum Spieler berechnen
                direction = (playerTransform.position - firePoint.position).normalized;
                // Projektil in die berechnete Richtung schießen
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.velocity = direction * projectileSpeed;
            }
        }
    }
}