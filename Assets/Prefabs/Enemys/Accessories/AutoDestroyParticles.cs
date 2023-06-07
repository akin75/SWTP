using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    private ParticleSystem particle;

    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var main = particle.main;
        duration = main.duration + main.startLifetimeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, duration);
    }
}
