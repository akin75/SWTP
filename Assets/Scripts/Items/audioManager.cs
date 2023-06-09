using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    private AudioSource sfx;
    private float sfxDur;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        sfxDur = sfx.clip.length; // Annahme: Der AudioSource hat bereits einen AudioClip zugewiesen
        StartCoroutine(DestroyAfterDelay(sfxDur));
    }

    // Coroutine, um das GameObject nach einer Verzögerung zu zerstören
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}