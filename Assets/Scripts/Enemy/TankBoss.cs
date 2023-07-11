using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class TankBoss : MonoBehaviour
{
    private AIPath aiPath;
    private float originalMaxSpeed;
    public float chargeSpeed = 3;
    public float chargeDuration = 10f;
    public float cooldownDuration = 5f;
    private bool isCooldown = false;
    private SpriteRenderer tankSprite;
    public GameObject chargeSfx;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        originalMaxSpeed = aiPath.maxSpeed;
        tankSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCooldown)
        {
            StartCoroutine(ChargeCoroutine());
        }
    }

    private IEnumerator ChargeCoroutine()
    {
        Instantiate(chargeSfx, transform.position, Quaternion.identity);
        tankSprite.color = new Color(1f, 0.5f, 0.5f);
        // Setze die Geschwindigkeit auf chargeSpeed
        aiPath.maxSpeed = chargeSpeed;

        // Warte für die angegebene Dauer
        yield return new WaitForSeconds(chargeDuration);

        // Setze die Geschwindigkeit auf die ursprüngliche Maximalgeschwindigkeit
        aiPath.maxSpeed = originalMaxSpeed;

        // Starte den Cooldown-Timer
        StartCoroutine(CooldownCoroutine());
        tankSprite.color = Color.white;
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;

        // Warte für die angegebene Dauer
        yield return new WaitForSeconds(cooldownDuration);

        isCooldown = false;
    }
}