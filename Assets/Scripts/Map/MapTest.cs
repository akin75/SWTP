using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

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
    private void Start()
    {
        GenerateMultipleMap(generateField);
    }

    private void GenerateMultipleMap(List<int> genCount)
    {
        Vector3Int genOffset = new Vector3Int(0, 0);
        foreach (var count in genCount)
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
                    /*
                    else
                    {
                        if (tileToIgnore.name == v.tileCell.name) continue;
                        output.SetTile(v.positionInMap + offset + genOffset, v.tileCell);
                        
                    }
                    */
                }

                genOffset.x += mapSize.x;
                
            }

            genOffset.y += mapSize.y;
            genOffset.x = 0;
        }
    }
    
    private void Rotate(int[] array, int rotationCount)
    {
        int length = array.Length;
        int[] temp = new int[length];
        
        for (int i = 0; i < length; i++)
        {
            int rotatedIndex = (i + rotationCount) % length;
            temp[rotatedIndex] = array[i];
            
        }
        Debug.Log("Test = " +String.Join("",
            new List<int>(temp)
                .ConvertAll(i => i.ToString())
                .ToArray()));
        Array.Copy(temp, array, length);
    }

    
}


