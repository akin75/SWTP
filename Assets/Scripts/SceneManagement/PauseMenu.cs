using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject PauseMenuUI;

    public AudioSource testsound;

    // Update is called once per frame
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
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Bip Bop, Menü geladen.");
        testsound.Play();
    }

    public void QuitGame()
    {
        Debug.Log("Bip Bop, Spiel geschlossen.");
        Application.Quit();
        
    }
}
