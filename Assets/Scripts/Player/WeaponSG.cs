using UnityEngine;

public class WeaponSG : MonoBehaviour
{
    public float fireForce = 25f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Camera cam;
    public float timeBetweenShots = 0.5f;
    public float maxDeviation = 20f;
    public int damage = 10;
    private float timeSinceLastShot = 0f;
    private CameraShake cameraShake;
    private ParticleSystem muzzleParticles;
    public int FirePointCount = 5;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        for (int i = 0; i < FirePointCount; i++)
        {
            float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, deviationAngle) * firePoint.up;
            float randomOffset = Random.Range(-0.2f, 0.2f);
            Vector3 spawnPosition = new Vector3(firePoint.position.x + randomOffset, firePoint.position.y, firePoint.position.z);
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            newBullet.transform.right = bulletDirection;
            newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * fireForce, ForceMode2D.Impulse);
        }


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