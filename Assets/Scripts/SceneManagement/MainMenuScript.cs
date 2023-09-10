/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   [SerializeField] private Texture2D cursorSprite;
   [SerializeField] private Animator transition;
   private Vector2 cursorHotspot;
    void Start()
    {
        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.Auto);
    }

   
    /// <summary>
    /// In the main menu, the player has the option to start the game.
    /// <summary>
    public void StartGame(){
         Time.timeScale = 1f;
     SceneManager.LoadScene("StreetPlayer");//Ruft die Scene in der Klammer auf
     }
    /// <summary>
    /// With this option he can open the settings menu.
    /// <summary>
    public void OpenSettings(){
    SceneManager.LoadScene("SettingsMenu");//Ruft die Scene in der Klammer auf
     }
    /// <summary>
    /// To quit the game.
    /// <summary>
    public void QuitGame()
    {
        Application.Quit();
        
    }
}
