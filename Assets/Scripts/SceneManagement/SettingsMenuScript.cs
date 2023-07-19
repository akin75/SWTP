using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
public static bool HealthBarOn = true;
public Toggle toogle_Mute;
public Toggle toogle_HealthBar;
public Toggle toogle_FullScreen;
    // Update is called once per frame
    void Update()
    {
       
    }

     public void ReturnToMainMenu(){
       if (toogle_Mute.isOn)
			AudioListener.volume = 0f;

		else
			AudioListener.volume = 1f;

       if (toogle_HealthBar.isOn)
		    HealthBarOn = true;

		else
        HealthBarOn = false;
//
       if (toogle_FullScreen.isOn);
		//doo something

    SceneManager.LoadScene("MainMenu");//Ruft die Szene in der Klammer auf
     }

    public bool HealthBarIsEnabled(){
        return HealthBarOn;
    } 
     
}
