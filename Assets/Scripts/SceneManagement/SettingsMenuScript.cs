/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuScript : MonoBehaviour
{
public static bool HealthBarOn = true;

[SerializeField] Slider volumeSlider;
[SerializeField] TMP_Text volume;
[SerializeField] Toggle toogle_Mute;
[SerializeField] Toggle toogle_HealthBar;
[SerializeField] Toggle something;

    /// <summary>
    /// Here the player can adjust settings used in the game.
    /// <summary>

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            Load();
            PlayerPrefs.SetFloat("musicVolume", 1);
            
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// The player can adjust the global audio. The settings get saved in the player prefs.
    /// <summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
        volume.SetText(Mathf.Round((volumeSlider.value * 100)).ToString());
    }
    /// <summary>
    /// Loads the audio settings from player prefs into the audio settings.
    /// <summary>
    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }
    /// <summary>
    /// Saves the audio settings in the player prefs.
    /// <summary>
    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    /// <summary>
    /// Returns the player to the main menu and applies the settings to the game.
    /// <summary>
     public void ReturnToMainMenu(){
       if (toogle_Mute.isOn)
			{AudioListener.volume = 0f;}

       if (toogle_HealthBar.isOn)
		    HealthBarOn = true;

		else
        HealthBarOn = false;



    SceneManager.LoadScene("MainMenu");//Ruft die Szene in der Klammer auf
     }

    /// <summary>
    /// Enables and disables the healthbars of enemies and the player.
    /// <summary>
    public bool HealthBarIsEnabled(){
        return HealthBarOn;
    } 
    
     
}
