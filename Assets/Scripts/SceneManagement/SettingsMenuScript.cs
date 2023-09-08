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
    // Update is called once per frame
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
    
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
        volume.SetText(Mathf.Round((volumeSlider.value * 100)).ToString());
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }

    private void Save(){
       
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

     public void ReturnToMainMenu(){
       if (toogle_Mute.isOn)
			{AudioListener.volume = 0f;}

       if (toogle_HealthBar.isOn)
		    HealthBarOn = true;

		else
        HealthBarOn = false;
//
       if  (something.isOn);
		//doo something

    SceneManager.LoadScene("MainMenu");//Ruft die Szene in der Klammer auf
     }

    public bool HealthBarIsEnabled(){
        return HealthBarOn;
    } 
    
     
}
