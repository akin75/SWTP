using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireForce = 40f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Camera cam;
    public float timeBetweenShots = 0.02f;
    public float maxDeviation = 10f;
    public int damage = 20;
    private float timeSinceLastShot = 0f;
    private CameraShake cameraShake;
    private ParticleSystem muzzleParticles;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        Transform firePointChild = transform.Find("Firepoint");
        if (firePointChild != null)
        {
            muzzleParticles = firePointChild.GetComponentInChildren<ParticleSystem>();
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void Shoot()
    {
        float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
        Vector2 bulletDirection = Quaternion.Euler(0f, 0f, deviationAngle) * firePoint.up;

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.transform.right = bulletDirection;
        newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * fireForce, ForceMode2D.Impulse);

        if (muzzleParticles != null)
        {
            muzzleParticles.Play();
        }

        if (cameraShake != null)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = (mousePosition - (Vector2)firePoint.position).normalized;
            cameraShake.StartShaking(shotDirection);
        }
    }
    public void SetDamage(int value)
    {
        damage = damage + value;
    }
}