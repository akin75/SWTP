using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTest : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize = new Vector2Int(5, 5);
    [SerializeField] private float cellSize;
    [SerializeField] List<TileContacts> tileContacts = new List<TileContacts>();
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap output;
    [SerializeField] private Grid grid;
    private void Start()
    {
        TileMapGenerator gen = new TileMapGenerator(mapSize, tileContacts, tilemap);
        gen.SetTileCells();
        GameObject tilemapGameObject = new GameObject("Collider");

        // Get a reference to the Grid component in the scene
        // Set the Grid as the parent of the Tilemap GameObject
        tilemapGameObject.transform.SetParent(grid.transform);

        // Add a Tilemap component to the GameObject
        Tilemap collider = tilemapGameObject.AddComponent<Tilemap>();
        tilemapGameObject.AddComponent<TilemapRenderer>();
        tilemapGameObject.AddComponent<TilemapCollider2D>();
        foreach (var v in gen.tileMapCells)
        {
            if (v.isCollider)
            {
                //Debug.Log("Collider");
                collider.SetTile(v.positionInMap, v.tileCell);
            }
            else
            {
                output.SetTile(v.positionInMap + new Vector3Int(-6, -9), v.tileCell);
            }

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


