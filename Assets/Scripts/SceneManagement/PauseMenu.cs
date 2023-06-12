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
    private GameObject gameOverScreen;
    public HealthBar healthBar;
    public Player player;
    public AudioSource testsound;
   
   // public KillCounter kc;
    public TMP_Text heal;
    public TMP_Text coin;
    public TMP_Text kills;


    private void Start()
    {
        //pauseMenuUI = GameObject.Find("PauseScreen");
        hud.SetActive(true);
    }

    void Update()
    {
        heal.SetText("Health: " + player.GetCurrentHealth());
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
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            

        }
        Time.timeScale = 1f;
        gameIsPaused = false;
        cameraController.enabled = true;
    }

    void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }        
        Time.timeScale = 0f;
        gameIsPaused = true;
        cameraController.enabled = false;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Bip Bop, Menü geladen.");
        testsound.Play();
    }

    public void GameOver()
    {
        Debug.Log("Spieler Gestorben");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.5f;
        gameIsPaused = true;
    }

public void RestartGame()
    {
        Debug.Log("Spiel neugestartet");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
