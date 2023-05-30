using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab des Projektils
    public Transform firePoint; // Punkt, an dem das Projektil erscheinen soll
    public float attackRange = 20f; // Distanz, in der der Gegner angreifen soll
    public float projectileSpeed = 10f; // Geschwindigkeit des Projektils
    public float shootingInterval = 1f; // Zeitabstand zwischen den Schüssen

    private Transform player; // Referenz auf den Spieler
    private bool isAttacking = false; // Flag, um den Angriffszustand zu überprüfen

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz erhalten
        InvokeRepeating("ShootProjectile", shootingInterval, shootingInterval);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Angriffsabstand überprüfen
        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
            }
        }
        else
        {
            isAttacking = false;
            // Normaler Bewegungsverhalten des Gegners, z.B. Pfadfolge oder anderes Verhalten
        }
    }

    private void ShootProjectile()
    {
        if (isAttacking)
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
}