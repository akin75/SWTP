using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public SpriteRenderer mineSr;
    public ParticleSystem explosionParticles;
    public ParticleSystem smokeParticles;
    
    private bool hasExploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

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
    
    void DestroyObj()
    {
        Destroy(mineSr);
        Destroy(gameObject);
    }
}