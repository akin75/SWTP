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
    [SerializeField] private Tilemap basetile;
    [SerializeField] private Tilemap colliderTile;

    private List<Quaternion> rotation = new List<Quaternion>()
        { Quaternion.Euler(0, 0, 90f), Quaternion.Euler(0, 0, 270f), Quaternion.Euler(0, 0, 180f), Quaternion.Euler(0,0,0f) };
    private void Start()
    {
        TileMapGenerator gen = new TileMapGenerator(mapSize, tileContacts, tilemap);
        gen.SetTileCells();
        var rand = new System.Random();
        foreach (var v in gen.tileMapCells)
        {
            if (v.isCollider)
            {
                colliderTile.SetTile(v.positionInMap + new Vector3Int(-6, -9), v.tileCell);
                //Debug.Log("Collider");

            }
            else
            {
                if (basetile.GetTile(v.positionInMap + new Vector3Int(-6, -9)).name == v.tileCell.name)
                {
                    basetile.SetTile(v.positionInMap + new Vector3Int(-6, -9), v.tileCell);
                }
                else
                {
                    output.SetTile(v.positionInMap + new Vector3Int(-6, -9), v.tileCell);
                }
                
            }

        }
    }

    private bool IsOverlapping(Vector3Int position)
    {
        var startPos = position + new Vector3Int(-1, 1);
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                startPos += new Vector3Int(x, -y);
                if (colliderTile.GetTile(startPos) != null && startPos != position)
                {
                    return true;
                }
            }
        }

        return false;
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


