using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponSide { WeaponL, WeaponR }
    public WeaponSide weaponSide;

    private enum WeaponState { Ready, Reloading };
    private WeaponState state = WeaponState.Ready;
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
    private int level = 0;
    private WeaponSG newInstance;
    private WeaponUpgrade weaponUpgrade;
    public AudioSource draw;
    public AudioSource reloadSfx;
    public AudioSource shotSfx;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraController = Camera.main.GetComponent<CameraController>();
        weaponUpgrade = GameObject.Find("CartBox").GetComponent<WeaponUpgrade>();
        float delay = timeBetweenShots / 2;
        Transform firePointChild = transform.Find("FirePoint");
        level = GetLevel();
        reloadSfx = GetComponent<AudioSource>();
        if (firePointChild != null)
        {
            muzzleParticles = firePointChild.GetComponentInChildren<ParticleSystem>();
        }
        else
        {
            Debug.Log("Muzzle null");
        }

        StartCoroutine(DrawWeaponSound());
        
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

    private IEnumerator DrawWeaponSound()
    {
        float delay = timeBetweenShots / 2;
        if (weaponSide == WeaponSide.WeaponR)
        {
            yield return new WaitForSeconds(delay);
            draw.Play();
        }
        else
        {
            draw.Play();
        }
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
            state = WeaponState.Reloading;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator DelayShooting()
    {
        float delay = timeBetweenShots / 2;
        if (weaponSide == WeaponSide.WeaponR)
        {
            yield return new WaitForSeconds(delay);
            float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, deviationAngle) * firePoint.up;
            shotSfx.Play();
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
            
            if (recoil != null)
            {
                recoil.StartRecoil();
            }
            else
            {
                Debug.Log("Recoil Missing");
            }
            ammo -= 1;
        }
        else
        {
            float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, deviationAngle) * firePoint.up;
            shotSfx.Play();
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
            if (recoil != null)
            {
                recoil.StartRecoil();
            }
            else
            {
                Debug.Log("Recoil Missing");
            }
            ammo -= 1;
            yield return null;
        }
    }
    private void Shoot()
    {
        if (state == WeaponState.Reloading)
        {
            return;
        }

        StartCoroutine(DelayShooting());

        if (cameraController != null)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = (mousePosition - (Vector2)firePoint.position).normalized;
            cameraController.StartShaking(shotDirection);
        }
        
        if (ammo == 0)
        {
            state = WeaponState.Reloading;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        //Debug.Log("Reloading!");
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
        state = WeaponState.Ready;
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