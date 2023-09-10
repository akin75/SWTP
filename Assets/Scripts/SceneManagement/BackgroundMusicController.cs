using System.Collections;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip[] sfxArray;
    public AudioClip backgroundSound;
    private AudioSource audioSource;
    public float fadeDuration = 2f;

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
/// <summary>
/// plays a random sfx for atmosphere
/// </summary>
/// <param name="index"></param>
    private void PlayRandomSfx(int index)
    {
        if (audioSource != null && sfxArray.Length > index)
        {
            audioSource.clip = sfxArray[index];
            audioSource.Play();
            StartCoroutine(PlayPublicSoundWithFade());
        }
    }

    private IEnumerator PlayPublicSoundWithFade()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        float startTime = Time.time;
        float endTime = startTime + fadeDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / fadeDuration;
            audioSource.volume = Mathf.Lerp(0f, 0.5f, t);
            yield return null;
        }

        audioSource.volume = 0.5f;
        audioSource.loop = true;
        audioSource.clip = backgroundSound;
        audioSource.Play();
    }
}