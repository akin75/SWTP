using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTest : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize = new Vector2Int(5, 5);
    [SerializeField] private float cellSize;
    [SerializeField] List<TileContacts> tileContacts = new List<TileContacts>();
    private void Start()
    {
        for (int i = 0; i < tileContacts.Count-1; i++)
        {
            Debug.Log("Test" + tileContacts[i].IsEqual(tileContacts[i + 1]));
        }
    }
}
