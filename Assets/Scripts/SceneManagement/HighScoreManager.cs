/* created by: SWT-P_SS_23_akin75 */

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
    [SerializeField] private bool HSPIsEnabled = false;

    [System.Serializable]
    public class HighscoreText
    {
        public TMP_Text name;
        public TMP_Text wave;
        public TMP_Text kills;
    }
    [SerializeField] private HighscoreText[] ht;

    /// <summary>
    /// Activate the Highscore UI and load the highscore from PlayerPrefs.
    /// </summary>
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
