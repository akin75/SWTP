using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;


/// <summary>
/// Class <c>MapTest</c> is a procedurally generated Map. This class is only for the forest
/// </summary>
public class MapTest : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize = new Vector2Int(5, 5);
    [SerializeField] private float cellSize;
    [SerializeField] List<TileContacts> tileContacts = new List<TileContacts>();
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap output;
    [SerializeField] private Grid grid;
    [SerializeField] private Vector3Int offset = new Vector3Int(0,0);
    [SerializeField] private List<GameObject> treeObjects;
    [SerializeField] private List<int> generateField;
    [SerializeField] private Tile tileToIgnore;
    [SerializeField] private List<GameObject> barrelObjects;
    private JobHandle handle;
    private void Start()
    {

        GenerateMultipleMap();

    }

    
    /// <summary>
    /// Generate chunks of a map and put it together
    /// </summary>
    void GenerateMultipleMap()
    {
        Vector3Int genOffset = new Vector3Int(0, 0);
        foreach (var count in generateField)
        {
            for (int i = 0; i < count; i++)
            {
                TileMapGenerator gen = new TileMapGenerator(mapSize, tileContacts, tilemap);
                gen.SetTileCells();
                var rand = new System.Random();
                foreach (var v in gen.tileMapCells)
                {
                    if (v.isCollider)
                    {
                        int randomIndex = rand.Next(0, treeObjects.Count);
                        Instantiate(treeObjects[randomIndex], v.positionInMap + offset + genOffset, Quaternion.identity);
                    }
                }

                genOffset.x += mapSize.x;
                
            }

            genOffset.y += mapSize.y;
            genOffset.x = 0;
        }
    }
    
}


