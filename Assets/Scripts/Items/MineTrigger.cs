using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mineSr;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    
    private bool hasExploded = false;

    /// <summary>
    /// triggers the explosion
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
/// <summary>
/// handles the explosion
/// </summary>
    private void Explode()
    {
        //Debug.Log("Boom");
        if (smokeParticles != null)
        {
            var mainModule = smokeParticles.main;
            var durE = mainModule.duration * 2;
            var emE = smokeParticles.emission;
            
            explosionParticles.Play();
            smokeParticles.Play();
            //Destroy(mineSr);
            Invoke("DestroyObj", durE);
        }
    }
    
/// <summary>
/// destroys the object after explosion
/// </summary>
    void DestroyObj()
    {
        Destroy(mineSr);
        Destroy(gameObject);
    }
}