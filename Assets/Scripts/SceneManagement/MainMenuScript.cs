using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
     SceneManager.LoadScene("StreetPlayer");//Ruft die Scene in der Klammer auf
     }

    public void OpenSettings(){
    SceneManager.LoadScene("SettingsMenu");//Ruft die Scene in der Klammer auf
     }
 
 
}
