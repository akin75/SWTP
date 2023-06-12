using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    private GameObject gameOverScreen;
    //public HealthBar healthBar;
    //public Player player;
    //public AudioSource testsound;

    private void Start()
    {
        pauseMenuUI = GameObject.Find("PauseScreen");
    }

    void Update()
    {
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
    }

    void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }        
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Bip Bop, Menü geladen.");
        //testsound.Play();
    }

    public void GameOver()
    {
        Debug.Log("Spieler Gestorben");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

public void RestartGame()
    {
        Debug.Log("Spiel neugestartet");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Bip Bop, Spiel geschlossen.");
        Application.Quit();
        
    }
}
