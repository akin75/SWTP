using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    [SerializeField] private GameObject bait;
    [SerializeField] private GameObject mine;
    public int baitCount;
    public int mineCount;

    public AudioSource throwSfx;
    public AudioSource failSfx;

    private Vector3 playerPosition;
    
    void Update()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        ThrowItem();
    }

    /// <summary>
    /// places mine and bait
    /// </summary>
    private void ThrowItem()
    {
        if (Input.GetKeyDown("q"))
        {
            if (mineCount > 0)
            {
                Instantiate(mine, playerPosition, Quaternion.identity); 
                throwSfx.Play();
                mineCount--;
            }
            else
            {
                if (failSfx != null)
                {
                    failSfx.Play();
                }
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if (baitCount > 0)
            {
                Instantiate(bait, playerPosition, Quaternion.identity);
                throwSfx.Play();
                baitCount--;
            }
            if (failSfx != null)
            {
                failSfx.Play();
            }
        }
        
    }
    /// <summary>
    /// returns number of mines
    /// </summary>
    /// <returns>mine count</returns>
    public int GetMineCount()
    {
        return mineCount;
    }
    /// <summary>
    /// returns number of bait
    /// </summary>
    /// <returns>bait count</returns>
    public int GetBaitCount()
    {
        return baitCount;
    }  
}
