using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingHandling : MonoBehaviour
{
    private Player player;
    private Volume volume;
    private Vignette vignette;
    [SerializeField] private AnimationCurve vignetteIntensity;
    public AudioSource heartbeatSlowSfx;
    public AudioSource heartbeatFastSfx;

    // Start is called before the first frame update
    void Start()
    {
        volume = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        LowHealthEffect();
        HeartBeat();
    }
    
    /// <summary>
    /// post processing effects when health is low
    /// </summary>
    private void LowHealthEffect()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        float currentHealth = player.GetCurrentHealth();
        if (vignette != null)
        {
            if (currentHealth <= 50)
            { 
                vignette.color.Override(new Color(0.3f, 0f, 0f)); // Dunkelrot mit RGB-Werten von (0.5, 0, 0)
                float intensity = vignetteIntensity.Evaluate(100f - currentHealth);
                vignette.intensity.Override(intensity);
            } else
            {
                vignette.color.Override(Color.black);
                vignette.intensity.Override(0.4f);
            }
        }
    }

    /// <summary>
    /// heartbeat sfx when health is low
    /// </summary>
    private void HeartBeat()
    {
        float currentHealth = player.GetCurrentHealth();
        if (currentHealth <= 50)
        {
            heartbeatSlowSfx.mute = false;
            heartbeatFastSfx.mute = true;
        } else if (currentHealth <= 25)
        {
            heartbeatSlowSfx.mute = true;
            heartbeatFastSfx.mute = false;
        }
        else
        {
            heartbeatSlowSfx.mute = true;
            heartbeatFastSfx.mute = true;
        }
    }
}
