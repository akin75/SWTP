using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip[] sfxArray;
    private AudioSource audioSource;
    public float minPitch = -0.5f;
    public float maxPitch = 0.5f;
    private float volume = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        volume = audioSource.volume;
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
        audioSource.volume = volume;
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
        Destroy(gameObject);
    }
}