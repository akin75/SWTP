using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject hud;
    public CameraController cameraController;
    public GameObject gameOverScreen;
    public HealthBar healthBar;
    public Player player;
    public AudioSource testsound;
    [SerializeField]private AchievementManager AM;
    [SerializeField]private GameObject AP1;//AchievementPage
    [SerializeField]private GameObject AP2;
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

    void Start()
    {
            

        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.Auto);
        hud.SetActive(true);
        
    }

    void Update()
    {
       
        //heal.SetText("Health: " + player.GetCurrentHealth());
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
    void Pause()
    {
        if (pauseMenuUI != null)
        {
            hud.SetActive(false);
            DisplayAchievements();
            pauseMenuUI.SetActive(true);
        }        
        Time.timeScale = 0f;
        gameIsPaused = true;
        cameraController.enabled = false;
    }
    
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Bip Bop, Menü geladen.");
        testsound.Play();
        Resume();
    }

    public void GameOver()
    {
        hud.SetActive(false);
        Debug.Log("Spieler Gestorben");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

public void RestartGame()
    {
        Debug.Log("Spiel neugestartet");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        hud.SetActive(true);
        gameOverScreen.SetActive(false);
        cameraController.enabled = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Bip Bop, Spiel geschlossen.");
        Application.Quit();
        
    }

    public bool IsPaused(){
        return gameIsPaused;        
    }
}
