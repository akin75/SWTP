using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject HighscorePage;
    public bool HSPIsEnabled = false; //HighscorePanel
    public TextMeshProUGUI Player1Wave;

    // Update is called once per frame
    void Update()
    {
                
    }

    public void ToggleHighscorePanel(){
        Player1Wave.text = "Test2";
        if(HSPIsEnabled){
            HighscorePage.SetActive(false);
            HSPIsEnabled = false;
        }

        else {
            HighscorePage.SetActive(true);
            HSPIsEnabled = true;
        }
    }
}
