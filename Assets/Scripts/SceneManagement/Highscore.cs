/* created by: SWT-P_SS_23_akin75 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{   
    [System.Serializable]
    public class HS
    {
        public int kills;
        public int wave;
    }

    [SerializeField] private HS[] hs;

    /// <summary>
    /// Load the saved highscore if possible from PlayerPrefs.
    /// </summary>
    public void LoadHighscore()
    {
        for (int i=1; i<hs.Length; i++)
        {
            if (PlayerPrefs.HasKey("HS"+i))
            {
                hs[i] = JsonUtility.FromJson<HS>(PlayerPrefs.GetString("HS"+i));
            }    
        }
    }

    /// <summary>
    /// Compare the new score with the old highscore, sort and save it to PlayerPrefs.
    /// </summary>
    public void SetHighscore()
    {
        LoadHighscore();

        hs[0].kills = GameObject.Find("HUD").GetComponent<KillCounter>().GetKills();
        hs[0].wave = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().GetWaveCounter();
        
        for (int i=1; i<hs.Length; i++)
        {
            if (hs[0].kills > hs[i].kills)
            {
                PlayerPrefs.SetString("HS"+(i), JsonUtility.ToJson(hs[0]));
                for (int j=i; j<(hs.Length-1); j++)
                {
                    PlayerPrefs.SetString("HS"+(j+1), JsonUtility.ToJson(hs[j]));
                }
                break;
            }           
        }
    }
}
