using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// Class <c>TileMapGenerator</c> is a Wave Function Collapse class.
/// </summary>
public class TileMapGenerator
{
    // Start is called before the first frame update
    public TileMapCell[,] tileMapCells;
    private List<TileContacts> tileContactsList;
    private Vector2Int mapSize;
    private Tilemap tilemap;
    List<Vector3Int> stack = new List<Vector3Int>();
    private HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
    public TileMapGenerator(Vector2Int mapSize, List<TileContacts> tileContactsList, Tilemap tilemap)
    {
        this.tileContactsList = tileContactsList;
        this.mapSize = mapSize;
        this.tilemap = tilemap;
        tileMapCells = new TileMapCell[mapSize.y, mapSize.x];
        InitializeCellsArray();
    }

    /// <summary>
    /// Initialize the <c>TileMapCell</c> class
    /// </summary>
    private void InitializeCellsArray()
    {
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                tileMapCells[i, j] = new TileMapCell(new List<TileContacts>(tileContactsList), new Vector3Int(j, i), tilemap, mapSize);
            } 
        }
    }

    /// <summary>
    /// Start of the Wave Function Collapse algorithm. Check if everythin is collapsed if not get the minimum Entropy of a tile and collapse it
    /// </summary>
    public void SetTileCells()
    {
        var rand = new System.Random();
        var y = rand.Next(0, mapSize.y);
        var x = rand.Next(0, mapSize.x);
        var min = tileMapCells[y, x];
        
        var i = 0;
        while (!CheckIfEverythingCollapsed())
        {
            
            min = GetMinEntropy();
            //if(min.tileArray.Length== 0) Debug.Log("Position: " + min.positionInMap + "  Test: " + min.tileCell);
            
            var coords = rand.Next(0, min.tileContacts.Count);
            min.Collapse(coords); // Improvement Idea likely to select more path over than obstacles
            visited = new HashSet<Vector3Int>();
            Propagate(min);
        }
    }
    
    /// <summary>
    /// Get the possible neighbour of a TileMapCell
    /// </summary>
    /// <param name="tile">The tile to get the neighbour</param>
    /// <param name="a"> the direction to get</param>
    /// <returns>List of tile for possible neighbours of the TileMapCell</returns>

    private List<Tile> PossibleNeighbours(TileMapCell tile, Vector3Int a)
    {
        //Should return me the contact list of the tilecell
        if(tile.positionInMap + DirectionEnum.Forward == a) return tile.GetAllPossibleNeighbours(DirectionEnum.Forward);
        if(tile.positionInMap + DirectionEnum.Right == a) return tile.GetAllPossibleNeighbours(DirectionEnum.Right);
        if(tile.positionInMap + DirectionEnum.Down == a) return tile.GetAllPossibleNeighbours(DirectionEnum.Down);
        return tile.GetAllPossibleNeighbours(DirectionEnum.Left);
    }

   
    /// <summary>
    /// Propagate methods updates all the adjacent TileMapCell and deletes all the possibilities that the rules is set
    /// </summary>
    /// <param name="tileMapCell">Cell to propagate</param>

    private void Propagate(TileMapCell tileMapCell)
    {
        stack.Add(tileMapCell.positionInMap);
        var i = 0;
        while (stack.Count > 0 )
        {
            var currentTile = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            //Debug.Log(stack.Count);
            //if (i == 10) break;
            foreach (var d in ValidNeighbours(tileMapCells[currentTile.y,currentTile.x]))
            {
                var otherPossible = new List<TileContacts>(tileMapCells[d.y, d.x].GetPossibilities());

                var possibleNeighbours = PossibleNeighbours(tileMapCells[currentTile.y, currentTile.x], d);
                //Debug.Log("Poss Length" + possibleNeighbours.Count + "   otherPossible: " + otherPossible.Count);
                //Debug.Log(toString(possibleNeighbours));
                if (otherPossible.Count == 1) continue;
                foreach (var other in otherPossible)
                {
                    if (!possibleNeighbours.Contains(other.ContactTile))
                    {
                        //Debug.Log($"Selected Tile {tileMapCells[currentTile.y,currentTile.x].ToString()} \n   Other Tile {tileMapCells[d.y,d.x]}"  );
                        tileMapCells[d.y,d.x].Constrain(other);
                        if(!stack.Contains(tileMapCells[d.y,d.x].positionInMap)) stack.Add(tileMapCells[d.y,d.x].positionInMap);
                    }
                }
            }

            i++;
        }
    }
    
    /// <summary>
    /// Check if the tile map cell adjacent is a valid neighbour
    /// </summary>
    /// <param name="tileMapCell">Cell to look at </param>
    /// <returns>List of the position or Vector3Int</returns>
    private List<Vector3Int> ValidNeighbours(TileMapCell tileMapCell)
    {
        var adj = tileMapCell.GetAdjacentMapCell();
        List<Vector3Int> valid = new List<Vector3Int>();
        foreach (var a in adj)
        {
            if (!tileMapCells[a.y,a.x].isCollapsed && !stack.Contains(tileMapCells[a.y,a.x].positionInMap))
            {
                valid.Add(tileMapCells[a.y,a.x].positionInMap);
                visited.Add(tileMapCells[a.y, a.x].positionInMap);
            }
        }

        return valid;
    }
    
    /// <summary>
    /// Get the minimum Entropy of a TileMapCell. A minimum entropy is defined by how many piece a tilemapcell can collapse to
    /// </summary>
    /// <returns></returns>
    public TileMapCell GetMinEntropy()
    {
        List<TileMapCell> minEntropy = new List<TileMapCell>();
        List<TileMapCell> t = tileMapCells.Cast<TileMapCell>().ToList();

        return t.Where(x => !x.isCollapsed && x.tileContacts.Count > 1).OrderBy(x => x.tileContacts.Count).FirstOrDefault();
    }

    /// <summary>
    /// Check if every tilemapcell is collapsed
    /// </summary>
    /// <returns>Booleand statement. Returns True if every TileMapCell is collapsed</returns>
    private bool CheckIfEverythingCollapsed()
    {
        return tileMapCells.Cast<TileMapCell>().All((x) => x.isCollapsed);
    }
}
