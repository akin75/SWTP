using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip[] sfxArray;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public float minPitch = -0.5f;
    public float maxPitch = 0.5f;
    public bool hasMusic = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (sfxArray.Length > 0)
        {
            int randomIndex = Random.Range(0, sfxArray.Length);
            PlayRandomSfx(randomIndex);
        }
        else
        {
            Debug.LogWarning("No sound effects assigned to the array!");
        }
    }

    private void PlayRandomSfx(int index)
    {
        if (audioSource != null && sfxArray.Length > index)
        {
            audioSource.clip = sfxArray[index];
            audioSource.pitch = Random.Range(minPitch, maxPitch); // Zufälliger Pitch-Wert
            audioSource.Play();
            StartCoroutine(DestroyAfterSound(audioSource.clip.length));
        }
    }

    private IEnumerator DestroyAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hasMusic == false)
        { 
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(playBackgroundMusic());
        }
    }

    private IEnumerator playBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.01f;
        audioSource.Play();
        for (float i = 0.01f; i < 0.3; i = i + 0.001f)
        {
            yield return new WaitForSeconds(0.25f);
            audioSource.volume = i;     
        }
    }
}