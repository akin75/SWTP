/* created by: SWT-P_SS_23_akin75 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject hud;
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] HealthBar healthBar;
    [SerializeField] Player player;
    [SerializeField] AudioSource testsound;
       
    [SerializeField]private AchievementManager AM;
    [SerializeField]private GameObject AP1;//AchievementPage
    [SerializeField]private GameObject AP2;
    [SerializeField] private Animator transition;
    public int APActive = 1;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    public Slider slider5;
    public Slider slider6;
    public Slider slider7;
    public Slider slider8;
    public Slider slider9;
    public Slider slider10;
    public Slider slider11;

    public TMP_Text coin;
    [SerializeField] private Texture2D cursorSprite;
   private Vector2 cursorHotspot;

    private void Start()
    {
        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.Auto);
        hud.SetActive(true);
    }

    void Update()
    {
         if (Input.GetKeyDown("a")){SwitchAchievementPage(); }
       
        coin.SetText(""+player.GetCoins());
        if (Input.GetKeyDown("escape"))
        {
           
            if (gameIsPaused)
            {
                Resume();
            } else 
            {
                Pause();
            }
        }
    }
    
/// <summary>
    ///Methods used for navigating in and UI elements associated with the pause menu in game.
    /// </summary>

/// <summary>
    /// Update the visual achievements and displays them in the pause screen.
    /// </summary>
     public void DisplayAchievements(){
       float[] ap= AM.ComputeProgPercent();
        slider1.value= ap[0];
        slider2.value= ap[1];
        slider3.value= ap[2];
        slider4.value= ap[3];
        slider5.value= ap[4];
        slider6.value= ap[5];
        slider7.value= ap[6];
        slider8.value= ap[7];
        slider9.value= ap[8];
        slider10.value= ap[9];
        slider11.value= ap[10];
    }

    /// <summary>
    /// Switches between the achievements pages.
    /// <summary>
    public void SwitchAchievementPage(){
        if(APActive == 1){
             APActive = 2;
            AP2.SetActive(true);
            AP1.SetActive(false);
           
            
        }
        else if(APActive == 2){
            APActive = 1;
            AP1.SetActive(true);
            AP2.SetActive(false);
            
        }
    }
    /// <summary>
    /// Pauses the game.
    /// <summary>
    void Pause()
    {
        if (pauseMenuUI != null)
        {
            hud.SetActive(false);
            pauseMenuUI.SetActive(true);
            DisplayAchievements();
        }        
        Time.timeScale = 0f;
        gameIsPaused = true;
        cameraController.enabled = false;
    }
    /// <summary>
    /// Resumes to the game.
    /// <summary>
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
             hud.SetActive(true);
            

        }
        Time.timeScale = 1f;
        gameIsPaused = false;
        cameraController.enabled = true;
    }

           
    /// <summary>
    /// Returns to the main menu.
    /// <summary>
    public void LoadMainMenu()
    {
         StartCoroutine(ChangeScene());
    }
 IEnumerator ChangeScene(){
       transition.SetTrigger("GameStart");
        Resume();
         

        yield return new WaitForSeconds(1);
        //Time.timeScale = 0f;
         SceneManager.LoadScene("MainMenu"); 
        

        
       
        
    }
    /// <summary>
    /// When the player dies, a game over screen will be shown.
    /// <summary>
    public void GameOver()
    {
        hud.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

   /// <summary>
    /// When the player dies, he can restart the game to start a new round.
    /// <summary>
public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        hud.SetActive(true);
        gameOverScreen.SetActive(false);
        cameraController.enabled = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /// <summary>
    /// To quit the game.
    /// <summary>
    public void QuitGame()
    {
        Application.Quit();
        
    }
    /// <summary>
    /// Returns a boolean if the game is paused at the moment.
    /// <summary>
    public bool IsPaused(){
        return gameIsPaused;        
    }
}
