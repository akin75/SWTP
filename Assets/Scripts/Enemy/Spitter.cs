using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab des Projektils
    public Transform firePoint; // Punkt, an dem das Projektil erscheinen soll
    public float projectileSpeed = 10f; // Geschwindigkeit des Projektils
    public float shootingInterval = 1f; // Zeitabstand zwischen den Schüssen
    public Rigidbody2D rb;
    
    private Transform player; // Referenz auf den Spieler

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz erhalten
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            InvokeRepeating("ShootProjectile", shootingInterval, shootingInterval);
            Debug.Log("Player entered Shooting Range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            CancelInvoke("ShootProjectile");
            Debug.Log("Player exited Shooting Range");
        }
    }

    private void ShootProjectile()
    {
        // Erstelle ein Projektil und initialisiere es
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Richtung zum Spieler berechnen
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Projektil in die berechnete Richtung schießen
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;
    }
}