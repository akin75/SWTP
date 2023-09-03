using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    public GameObject bait;
    public GameObject mine;
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
    public int GetMineCount()
    {
        return mineCount;
    }
    public int GetBaitCount()
    {
        return baitCount;
    }  
}
