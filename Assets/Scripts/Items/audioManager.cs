using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    private AudioSource sfx;
    private float sfxDur;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        sfxDur = sfx.clip.length; 
        StartCoroutine(DestroyAfterDelay(sfxDur));
    }

    // Coroutine, um das GameObject nach einer Verzögerung zu zerstören
    /// <summary>
    /// Object is being destroyed when audio is finished
    /// </summary>
    /// <param name="delay">time it takes to destroy object</param>
    /// <returns></returns>
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}