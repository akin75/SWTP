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
    private float timeSinceLastShot = Mathf.Infinity; // Anfangswert auf unendlich setzen
    private CameraShake cameraShake;
    private Recoil recoil;
    public ParticleSystem muzzleParticles;
    public int firePointCount = 5;
    private int level = 1;
    
    
    
    public void Shoot()
    {
        for (int i = 0; i < firePointCount; i++)
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
    }

    public void SetDamage(int value)
    {
        damage = damage + value;
    }

    public void AddLevel(int level)
    {
        this.level += level;
    }
}
