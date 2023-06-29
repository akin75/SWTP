using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum weaponState { READY, RELOADING };
    private weaponState state = weaponState.READY;
    public float fireForce = 40f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Camera cam;
    public float timeBetweenShots = 0.02f;
    public float maxDeviation = 10f;
    public int damage = 20;
    public int ammo = 20;
    private int maxAmmo;
    public int reloadTime = 2;
    private float timeSinceLastShot = 0f;
    private CameraController cameraController;
    private Recoil recoil;
    private ParticleSystem muzzleParticles;
    private int level = 1;
    private WeaponSG newInstance;
    private WeaponUpgrade weaponUpgrade;
    private AudioSource reloadSfx;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraController = Camera.main.GetComponent<CameraController>();
        weaponUpgrade = GameObject.Find("CartBox").GetComponent<WeaponUpgrade>();
        Transform firePointChild = transform.Find("FirePoint");
        level = 1;
        reloadSfx = GetComponent<AudioSource>();
        if (firePointChild != null)
        {
            muzzleParticles = firePointChild.GetComponentInChildren<ParticleSystem>();
        }
        else
        {
            Debug.Log("Muzzle null");
        }

        if (transform.gameObject.TryGetComponent<WeaponSG>(out WeaponSG instance))
        {
            newInstance = instance;
            newInstance.muzzleParticles = muzzleParticles;
            newInstance.damage = damage;
            newInstance.timeBetweenShots = timeBetweenShots;
        }
        muzzleParticles.gameObject.SetActive(false);
        recoil = GetComponentInParent<Recoil>();
        maxAmmo = ammo;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots && !weaponUpgrade.shopState)
        {
            muzzleParticles.gameObject.SetActive(true);
            if (newInstance != null && transform.gameObject.name == "Shotgun")
            {
                newInstance.Shoot();
            }
            else
            {
                Shoot();
            }
            
            timeSinceLastShot = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            state = weaponState.RELOADING;
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {  
        if (state == weaponState.RELOADING)
        {
                return;
        }
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

        if (cameraController != null)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = (mousePosition - (Vector2)firePoint.position).normalized;
            cameraController.StartShaking(shotDirection);
        }

        if (recoil != null)
        {
            recoil.StartRecoil();
        }
        else
        {
            Debug.Log("Recoil Missing");
        }

        ammo -= 1;
        if (ammo == 0)
        {
            state = weaponState.RELOADING;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading!");
        if (reloadSfx != null)
        {
            reloadSfx.Play();
        }
        else
        {
            Debug.Log("SFX missing");
        }
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        state = weaponState.READY;
        yield break;
    }

    public void SetDamage(int value)
    {
        damage = damage + value;
        if (newInstance != null)
        {
            newInstance.damage = damage;
        }
    }

    public void SetTimeBetweenShots(float value)
    {
        timeBetweenShots -= value;
        if (newInstance != null)
        {
            newInstance.timeBetweenShots = timeBetweenShots;
        }
    }

    public void AddLevel(int level)
    {
        this.level += level;
    }

    public int GetLevel()
    {
        return level;
    }
}
