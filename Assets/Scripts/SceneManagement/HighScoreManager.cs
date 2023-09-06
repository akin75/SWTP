using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class HS
    {
        public int kills;
        public int wave;
    }

    [SerializeField] private GameObject HighscorePage;
    public bool HSPIsEnabled = false; //HighscorePanel

    [System.Serializable]
    public class HighscoreText
    {
        public TMP_Text name;
        public TMP_Text wave;
        public TMP_Text kills;
    }
    [SerializeField] private HighscoreText[] ht;

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleHighscorePanel(){
        for (int i=0; i<ht.Length; i++)
        {
            if (PlayerPrefs.HasKey("HS"+(i+1)))
            {
                
                ht[i].wave.SetText(""+JsonUtility.FromJson<HS>(PlayerPrefs.GetString("HS"+(i+1))).wave);
                ht[i].kills.SetText(""+JsonUtility.FromJson<HS>(PlayerPrefs.GetString("HS"+(i+1))).kills);
            }
            
        }

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
