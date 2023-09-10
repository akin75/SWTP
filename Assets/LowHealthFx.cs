using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LowHealthFx : MonoBehaviour
{
    private Volume globalVolume;
    private Vignette vignette;
    [SerializeField] private AnimationCurve vignetteIntensityCurve;
    private GameObject currentPlayer; // Referenz auf den aktuellen Player
    [SerializeField] private AudioSource fastHeartBeatSfx;
    
    private void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        globalVolume = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        globalVolume.profile.TryGet(out vignette);
    }

    private void Update()
    {
        LowHealthEffect();
        HeartBeat();
    }

    /// <summary>
    /// Post processing effects when player health is low
    /// </summary>
    private void LowHealthEffect()
    {
        if (vignette != null)
        {
            Player player = currentPlayer.GetComponent<Player>();
            float currentHealth = player.GetCurrentHealth();
            float healthPercentage = currentHealth / player.GetMaxHealth(); // Prozentsatz der verbleibenden Gesundheit (0-1)

            if (currentHealth <= 50)
            { 
                vignette.color.Override(new Color(0.2f, 0f, 0f));
                float intensity = vignetteIntensityCurve.Evaluate(1f - healthPercentage); // Invertieren des Prozentsatzes
                vignette.intensity.Override(intensity);
            }
            else
            {
                vignette.color.Override(Color.black);
                vignette.intensity.Override(0.25f);
            }
        }
    }

    /// <summary>
    /// heartbeat sfx when healt is low
    /// </summary>
    private void HeartBeat()
    {
        Player player = currentPlayer.GetComponent<Player>();
        float currentHealth = player.GetCurrentHealth();
        if (currentHealth <= 50)
        {
            fastHeartBeatSfx.mute = false;
        }
        else
        {
            fastHeartBeatSfx.mute = true;
        }
    }
}