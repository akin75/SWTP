using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
      public GameObject bulletPrefab;
      public Transform firePoint;
      public float fireForce = 20f;
      
      public float timeBetweenShots = 0.02f;
      private float timeSinceLastShot = 0f;

      public float maxDeviation = 10f;

      public void Update()
      {
            timeSinceLastShot += Time.deltaTime;
            if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots)
            {
                  Fire();
                  timeSinceLastShot = 0f;
            }
      }

      public void Fire()
      {
            // Generate random deviation angle
            float deviationAngle = UnityEngine.Random.Range(-maxDeviation, maxDeviation);
            Quaternion deviation = Quaternion.Euler(0, 0, deviationAngle);
            
            // Create bullet and apply deviation to rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * deviation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * fireForce, ForceMode2D.Impulse);
      }
}
