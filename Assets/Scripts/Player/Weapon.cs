/* created by: SWT-P_SS_23_akin75 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>
/// Class <c>Weapon</c> is the functionality how the weapon works
/// </summary>
public class Weapon : MonoBehaviour, IWeapon
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
    private ObjectUpgrade _objectUpgrade;
    public AudioSource draw;
    public AudioSource reloadSfx;
    public AudioSource shotSfx;
    public float delay;
    public bool doubleFireEnabled = false;
    public bool tripleFireEnabled = false;
    public bool perfectAccuracy = false;
    public bool infiniteAmmo = false;
    private IWeapon currentInstance;

    private void Awake()
    {
        if (currentInstance == null)
        {
            currentInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraController = Camera.main.GetComponent<CameraController>();
        weaponUpgrade = GameObject.Find("CartBox").GetComponent<WeaponUpgrade>();
        _objectUpgrade = GameObject.Find("ObjectShop").GetComponent<ObjectUpgrade>();
        delay = timeBetweenShots / 2;
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

        delay = timeBetweenShots / 2;
        StartCoroutine(DrawWeaponSound(delay));
        
        if (transform.gameObject.TryGetComponent<WeaponSG>(out WeaponSG instance))
        {
            currentInstance = instance;
            newInstance = instance;
            newInstance.muzzleParticles = muzzleParticles;
            newInstance.damage = damage;
            newInstance.timeBetweenShots = timeBetweenShots;
        }
        muzzleParticles.gameObject.SetActive(false);
        recoil = GetComponentInParent<Recoil>();
        maxAmmo = ammo;
    }


    

    private IEnumerator DrawWeaponSound(float delay)
    {
        if (weaponSide == WeaponSide.WeaponR)
        {
            yield return new WaitForSeconds(delay);
        }
        draw.Play();
    }

    public void DrawWeapon()
    {
        StartCoroutine(DrawWeaponSound(delay));
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots && !weaponUpgrade.shopState && !_objectUpgrade.shopState)
        {
            muzzleParticles.gameObject.SetActive(true);
            currentInstance.Shoot();
            timeSinceLastShot = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            state = WeaponState.Reloading;
            StartCoroutine(currentInstance.Reload());
        }

        if (perfectAccuracy)
        {
            maxDeviation = 0;
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
    
    
    /// <summary>
    /// <c>Shoot</c> methods, defines how weapon shoots 
    /// </summary>
    public void Shoot()
    {
        if (state == WeaponState.Reloading)
        {
            return;
        }

        StartCoroutine(DelayShooting());

        if (tripleFireEnabled)
        {
            doubleFireEnabled = false; // Deaktiviere doubleFire, wenn tripleFire aktiv ist
            StartCoroutine(TripleFire());
        }
        else if (doubleFireEnabled)
        {
            StartCoroutine(DoubleFire());
        }

        if (cameraController != null)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = (mousePosition - (Vector2)firePoint.position).normalized;
            cameraController.StartShaking(shotDirection);
        }

        if (ammo == 0 && !infiniteAmmo)
        {
            state = WeaponState.Reloading;
            StartCoroutine(Reload());
        }
    }


    IEnumerator DoubleFire()
    {
        if (ammo <= 0)
        {
            state = WeaponState.Reloading;
            StartCoroutine(currentInstance.Reload());
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(0.075f);
            StartCoroutine(DelayShooting());
        }
    }
    IEnumerator TripleFire()
    {
        if (ammo <= 0)
        {
            state = WeaponState.Reloading;
            StartCoroutine(Reload());
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(0.075f);
            StartCoroutine(DelayShooting());
            yield return new WaitForSeconds(0.075f);
            StartCoroutine(DelayShooting());
        }
    }

    public void EnableDoubleFire()
    {
        doubleFireEnabled = true;
    }

    public void EnableTripleFire()
    {
        tripleFireEnabled = true;
    }

    public void EnablePerfectAccuracy()
    {
        perfectAccuracy = true;
    }

    public void EnableInfiniteAmmo()
    {
        infiniteAmmo = true;
    }
    
    /// <summary>
    /// <c>Reload</c> methods, defines how a weapon reloads
    /// </summary>
    /// <returns>Nothing, since the method is a <c>IEnumerator</c></returns>
    public IEnumerator Reload()
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
    
    
    /// <summary>
    /// Sets the damage of the weapon
    /// </summary>
    /// <param name="value">Valute to sets the damage of the weapon</param>

    public void SetDamage(int value)
    {
        damage = damage + value;
        if (newInstance != null)
        {
            newInstance.damage = damage;
        }
    }
    
    /// <summary>
    /// Sets the time between shots for the weapon
    /// </summary>
    /// <param name="value">Value to sets the time between shots</param>

    public void SetTimeBetweenShots(float value)
    {
        timeBetweenShots -= value;
        if (newInstance != null)
        {
            newInstance.timeBetweenShots = timeBetweenShots;
        }
    }
    
    /// <summary>
    /// Add a level to a Weapon
    /// </summary>
    /// <param name="level">Value to add the level</param>

    public void AddLevel(int level)
    {
        this.level += level;
    }

    /// <summary>
    /// Get the level of the weapon
    /// </summary>
    /// <returns><c>level</c> of the weapon</returns>
    public int GetLevel()
    {
        return level;
    }
    
    
    /// <summary>
    /// Gets the ammo of the weapon
    /// </summary>
    /// <returns><c>ammo</c> of the weapon</returns>
    public int GetAmmo()
    {
        return ammo;
    }

    /// <summary>
    /// Gets the damage of the weapon
    /// </summary>
    /// <returns><c>damage</c> of the weapon</returns>
    public int GetDamage()
    {
        return damage;
    }
}