using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   [SerializeField] private Texture2D cursorSprite;
   private Vector2 cursorHotspot;
    void Start()
    {
        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.Auto);
    }

    public void StartGame(){
     SceneManager.LoadScene("StreetPlayer");//Ruft die Scene in der Klammer auf
     }

    public void OpenSettings(){
    SceneManager.LoadScene("SettingsMenu");//Ruft die Scene in der Klammer auf
     }
 
 
}
