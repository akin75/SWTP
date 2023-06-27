using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
   public Animator transition;
    void Update()
    {
        if (Input.GetKeyDown("space")){//Beim Drücken der Lertaste wird LoadNextScene aufgerufen
            LoadNextScene();
        }
    }
    public void LoadNextScene(){
        // Animation funktioniert noch nicht
        //transition.setTrigger("Start"); 
       // StartCoroutine(LoadLevel);
        SceneManager.LoadScene("PixelDungeon");//Ruft die Scene in der Klammer auf
        
    }



   /* IEnumerator LoadLevel()
    {
        transition.setTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("PixelDungeon");
    }
   */
}
