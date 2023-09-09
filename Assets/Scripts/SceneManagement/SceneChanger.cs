using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
   [SerializeField] private Animator transition;
    void Update()
    {
        if (Input.GetKeyDown("space")){//Beim Drücken der Lertaste wird LoadNextScene aufgerufen
           
        }
    }
    public void LoadNextScene(){
          
    }

}
