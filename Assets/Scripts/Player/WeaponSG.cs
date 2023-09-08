using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSG : MonoBehaviour, IWeapon
{
    public enum weaponState { READY, RELOADING };
    private weaponState state = weaponState.READY;
    public float fireForce = 25f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Camera cam;
    public float timeBetweenShots = 0.5f;
    public float maxDeviation = 20f;
    public int damage = 10;
    public int ammo = 8;
    private int maxAmmo = 8;
    public int reloadTime = 2;
    private float timeSinceLastShot = Mathf.Infinity; // Anfangswert auf unendlich setzen
    private CameraController cameraController;
    private Recoil recoil;
    public ParticleSystem muzzleParticles;
    public int firePointCount = 4;
    private int level = 0;
    public AudioSource reloadSfx;
    public AudioSource shotSfx;
    public AudioSource shotgunPumpSfx;
    
    private void Start()
    {
        recoil = GetComponentInParent<Recoil>();
    }

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

    public void SetFirePointCount(int newCount)
    {
        firePointCount = newCount;
    }
    public IEnumerator Reload()
    {
        Debug.Log("Reloading!");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        state = weaponState.READY;
        reloadSfx.Play();
        yield break;
    }

    IEnumerator PlayShotgunPumpSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        shotgunPumpSfx.Play();
    }

    public void SetDamage(int value)
    {
        damage = damage + value;
    }

    public void AddLevel(int level)
    {
        this.level += level;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public void SetTimeBetweenShots(float value)
    {
        this.timeBetweenShots = value;
    }

    public int GetDamage()
    {
        return damage;
    }
}
