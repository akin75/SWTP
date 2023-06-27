using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ReturnToMainMenu(){
    SceneManager.LoadScene("MainMenu");//Ruft die Scene in der Klammer auf
     }
}
