using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireForce = 40f;
    public float timeBetweenShots = 0.02f;
    public float maxDeviation = 10f;
    private float timeSinceLastShot;


    void Update()
    {
        // Fire weapon
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots)
        {
            Fire();
            timeSinceLastShot = 0f;
        }
        
        // Weapon flip
        // Check if mouse is on left side of screen
        bool isMouseOnLeft = Input.mousePosition.x < Screen.width / 2f;

        // Flip weapon object if mouse is on left
        transform.localScale = new Vector3(0.4f, isMouseOnLeft ? -0.4f : 0.4f, 1f);
    }

    void Fire()
    {
        // Random deviation angle
        float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
        Quaternion deviation = Quaternion.Euler(0f, 0f, deviationAngle);

        // Create bullet and apply deviation to rotation
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation * deviation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.right * fireForce, ForceMode2D.Impulse);
    }

    public void Aim(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}