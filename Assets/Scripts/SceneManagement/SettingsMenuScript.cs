using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
   
public Toggle toogle_Mute;
    // Update is called once per frame
    void Update()
    {
       
    }

     public void ReturnToMainMenu(){
       if (toogle_Mute.isOn)
			AudioListener.volume = 0f;

		else
			AudioListener.volume = 1f;
    SceneManager.LoadScene("MainMenu");//Ruft die Scene in der Klammer auf
     }

     
}
