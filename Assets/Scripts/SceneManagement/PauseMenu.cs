using System;
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
       
    //public KillCounter kc;
    //public TMP_Text heal;
    public TMP_Text coin;
    //public TMP_Text kills;
    //public TMP_Text ammo;


    private void Start()
    {
        //pauseMenuUI = GameObject.Find("PauseScreen");
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
}
