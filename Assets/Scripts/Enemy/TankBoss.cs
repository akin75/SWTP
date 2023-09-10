using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class TankBoss : MonoBehaviour
{
    private AIPath aiPath;
    private float originalMaxSpeed;
    [SerializeField] private float chargeSpeed = 3;
    [SerializeField] private float chargeDuration = 10f;
    [SerializeField] private float cooldownDuration = 5f;
    private bool isCooldown = false;
    private SpriteRenderer tankSprite;
    [SerializeField] private GameObject chargeSfx;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        originalMaxSpeed = aiPath.maxSpeed;
        tankSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Starts coroutine when player is in range
    /// </summary>
    /// <param name="other">player param</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCooldown)
        {
            StartCoroutine(ChargeCoroutine());
        }
    }
/// <summary>
/// Enemy increases speed and charges to the player
/// </summary>
/// <returns></returns>
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

/// <summary>
/// Enemy has a cooldown when he can charge again
/// </summary>
/// <returns></returns>
    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;

        // Warte für die angegebene Dauer
        yield return new WaitForSeconds(cooldownDuration);

        isCooldown = false;
    }
}