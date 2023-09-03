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

    void Start()
    {   
        LoadHighscore();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void LoadHighscore()
    {
        for (int i=1; i<hs.Length; i++)
        {
            if (PlayerPrefs.HasKey("HS"+i))
            {
                hs[i] = JsonUtility.FromJson<HS>(PlayerPrefs.GetString("HS"+i));
                Debug.Log("HS Position:"+i+" Kills:"+hs[i].kills+" Wave:"+hs[i].wave);
            }    
        }
    }

    public void SetHighscore()
    {
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
