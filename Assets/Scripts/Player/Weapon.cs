using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireForce = 40f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Camera cam;
    public float timeBetweenShots = 0.02f;
    public float maxDeviation = 10f;
    public int damage = 20;
    private float timeSinceLastShot = 0f;
    private CameraShake cameraShake;
    private Recoil recoil;
    private ParticleSystem muzzleParticles;
    private bool canShoot = false; // Variable zur Steuerung des Schussstatus

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        Transform firePointChild = transform.Find("FirePoint");
        if (firePointChild != null)
        {
            muzzleParticles = firePointChild.GetComponentInChildren<ParticleSystem>();
        }
        else
        {
            Debug.Log("Muzzle null");
        }
        muzzleParticles.gameObject.SetActive(false);

        recoil = GetComponentInParent<Recoil>();
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots)
        {
            muzzleParticles.gameObject.SetActive(true);
            canShoot = true;
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
        else
        {
            Debug.Log("Muzzle Particles Missing");
        }

        if (cameraShake != null)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = (mousePosition - (Vector2)firePoint.position).normalized;
            cameraShake.StartShaking(shotDirection);
        }

        if (recoil != null)
        {
            recoil.StartRecoil();
        }
        else
        {
            Debug.Log("Recoil Missing");
        }
    }

    public void SetDamage(int value)
    {
        damage = damage + value;
    }

    // Methode zum Aktivieren des Schießens
    public void EnableShooting()
    {
        canShoot = true;
    }
}
