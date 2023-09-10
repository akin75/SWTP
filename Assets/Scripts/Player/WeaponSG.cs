using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSG : MonoBehaviour, IWeapon
{
    public enum weaponState { READY, RELOADING };
    private weaponState state = weaponState.READY;
    [SerializeField] private float fireForce = 25f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private Camera cam;
    public float timeBetweenShots = 0.5f;
    [SerializeField] private float maxDeviation = 20f;
    public int damage = 10;
    [SerializeField] private int ammo = 8;
    private int maxAmmo = 8;
    [SerializeField] private int reloadTime = 2;
    private float timeSinceLastShot = Mathf.Infinity; // Anfangswert auf unendlich setzen
    private CameraController cameraController;
    private Recoil recoil;
    public ParticleSystem muzzleParticles;
    [SerializeField] private int firePointCount = 4;
    [SerializeField] private int level = 0;
    [SerializeField] private AudioSource reloadSfx;
    [SerializeField] private AudioSource shotSfx;
    [SerializeField] private AudioSource shotgunPumpSfx;
    
    private void Start()
    {
        recoil = GetComponentInParent<Recoil>();
    }

    /// <summary>
    /// handles the shooting of the Shotgun
    /// </summary>
    public void Shoot()
    {
        if (state == weaponState.RELOADING)
        {
            return;
        }

        float totalDeviation = maxDeviation * 2; // Gesamtwinkel, den du abdecken möchtest

        // Berechne den Winkel zwischen den Schüssen
        float angleBetweenShots = totalDeviation / (firePointCount - 1);

        for (int i = 0; i < firePointCount; i++)
        {
            // Berechne den Winkel des aktuellen Schusses
            float currentAngle = -maxDeviation + i * angleBetweenShots;

            // Drehe die Schussrichtung um den aktuellen Winkel
            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, currentAngle) * firePoint.up;

            float randomOffset = Random.Range(-0.2f, 0.2f);
            Vector3 spawnPosition = new Vector3(firePoint.position.x + randomOffset, firePoint.position.y, firePoint.position.z);
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            newBullet.transform.right = bulletDirection;
            newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * fireForce, ForceMode2D.Impulse);
        }
        shotSfx.Play();
        shotSfx.Play();

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
            Debug.Log("Recoil missing in ChSG");
        }

        ammo -= 1;
        if (ammo == 0)
        {
            state = weaponState.RELOADING;
            StartCoroutine(Reload());
        }
        else
        {
            StartCoroutine(PlayShotgunPumpSound(0.2f));
        }
    }

    /// <summary>
    /// sets the number of fire pooints
    /// </summary>
    /// <param name="newCount">number of new fire points</param>
    public void SetFirePointCount(int newCount)
    {
        firePointCount = newCount;
    }
    /// <summary>
    /// reloading shotgun
    /// </summary>
    /// <returns></returns>
    public IEnumerator Reload()
    {
        Debug.Log("Reloading!");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        state = weaponState.READY;
        reloadSfx.Play();
        yield break;
    }
    /// <summary>
    /// plays pump sound of shotgun
    /// </summary>
    /// <param name="delay">plays sound after the shot sound</param>
    /// <returns></returns>
    IEnumerator PlayShotgunPumpSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        shotgunPumpSfx.Play();
    }

    /// <summary>
    /// sets damage
    /// </summary>
    /// <param name="value">damage</param>
    public void SetDamage(int value)
    {
        damage = damage + value;
    }

    /// <summary>
    /// increases level
    /// </summary>
    /// <param name="level"></param>
    public void AddLevel(int level)
    {
        this.level += level;
    }

    /// <summary>
    /// returns ammo
    /// </summary>
    /// <returns>ammo</returns>
    public int GetAmmo()
    {
        return ammo;
    }
/// <summary>
/// returns level
/// </summary>
/// <returns>level</returns>
    public int GetLevel()
    {
        return this.level;
    }

/// <summary>
/// sets time between shots
/// </summary>
/// <param name="value">time between shots</param>
    public void SetTimeBetweenShots(float value)
    {
        this.timeBetweenShots = value;
    }
/// <summary>
/// returns damage
/// </summary>
/// <returns></returns>
    public int GetDamage()
    {
        return damage;
    }
}
