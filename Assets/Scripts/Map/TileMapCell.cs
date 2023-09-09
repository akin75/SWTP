using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// Class <c>TileMapCell</c> is the functionality of Wave Function Collapse
/// </summary>
public class TileMapCell
{
    public List<TileContacts> tileContacts;
    public Vector3Int positionInMap;
    public Tile tileCell;
    private Vector2Int mapSize;
    public bool isCollider;
    public bool isCollapsed;
    public TileMapCell(List<TileContacts> tile, Vector3Int position, Tilemap tilemap, Vector2Int mapSize)
    {
        tileContacts = tile;
        positionInMap = position;
        this.mapSize = mapSize;
        this.isCollapsed = false;
    }
    
    

    /// <summary>
    /// Collapse a cell to a piece
    /// </summary>
    /// <param name="piece">Piece to collapse</param>
    public void Collapse(int piece)
    {
        tileCell = tileContacts[piece].ContactTile;
        tileContacts.RemoveAll(x => x.ContactTile != tileCell);
        SetCollider(tileContacts.Find(x => x.ContactTile == tileCell).isCollider);
        isCollapsed = true;
    }
    
    /// <summary>
    /// Get adjacent map cell
    /// </summary>
    /// <returns>List of Vector3Int</returns>

    public List<Vector3Int> GetAdjacentMapCell()
    {
        List<Vector3Int> cells = new List<Vector3Int>();
        if (positionInMap.x - 1 >= 0) cells.Add(new Vector3Int(positionInMap.x-1, positionInMap.y));
        if (positionInMap.x + 1 < mapSize.x) cells.Add(new Vector3Int(positionInMap.x+1, positionInMap.y));
        if (positionInMap.y - 1 >= 0) cells.Add(new Vector3Int(positionInMap.x, positionInMap.y-1));
        if (positionInMap.y + 1 < mapSize.y) cells.Add(new Vector3Int(positionInMap.x, positionInMap.y+1));
        return cells;
    }


    /// <summary>
    /// Get possibilities which pieces can be chosen
    /// </summary>
    /// <returns>List of tiles</returns>
    public List<TileContacts> GetPossibilities()
    {
        return tileContacts;
    }
    
    
    /// <summary>
    /// Get all the possible neighbour/piece to this cell
    /// </summary>
    /// <param name="direction">The direction of the neighbour to get for this cell</param>
    /// <returns>List of tiles</returns>

    public List<Tile> GetAllPossibleNeighbours(Vector3Int direction)
    {
        List<Tile[]> possibleNeighbours = new List<Tile[]>();
        var listTile = tileContacts;
        if (!isCollapsed)
        {
            foreach (var t in listTile)
            {
                
                if (direction == DirectionEnum.Forward) possibleNeighbours.Add(t.PosY.ToArray());
                if (direction == DirectionEnum.Right) possibleNeighbours.Add(t.PosX.ToArray());
                if (direction == DirectionEnum.Down) possibleNeighbours.Add(t.NegY.ToArray());
                if (direction == DirectionEnum.Left) possibleNeighbours.Add(t.NegX.ToArray());
            }
        }
        else
        {
            var contacts = tileContacts[0];
            if (direction == DirectionEnum.Forward) possibleNeighbours.Add(contacts.PosY.ToArray());
            if (direction == DirectionEnum.Right) possibleNeighbours.Add(contacts.PosX.ToArray());
            if (direction == DirectionEnum.Down) possibleNeighbours.Add(contacts.NegY.ToArray());
            if (direction == DirectionEnum.Left) possibleNeighbours.Add(contacts.NegX.ToArray());
        }

        return JoinArray(possibleNeighbours).ToList();
    }

    
    
    /// <summary>
    /// Constrain or delete tiles from TileContacts
    /// </summary>
    /// <param name="toDelete">TileContacts to delete</param>
    public void Constrain(TileContacts toDelete)
    {
        if (isCollapsed)
        {
            return;
        }
        tileContacts.Remove(toDelete);
        if (tileContacts.Count == 1)
        {
            Collapse(0);
        }
    }


    override 
    public String ToString()
    {
        String s = "";
        int i = 0;
        foreach (var tile in tileContacts)
        {
            s += "  | " + tile.ContactTile.name + "  |  ";
            if (i % 4 == 0) s += "\n";
            i++;
        }

        return s;
    }

/// <summary>
/// Join all the array together
/// </summary>
/// <param name="toJoin">List of tile array to join</param>
/// <returns>Tile Array </returns>
    private Tile[] JoinArray(List<Tile[]> toJoin) // todo
    {
        Tile[] temp = new Tile[]{};
        foreach (var tile in toJoin)
        {
            temp = temp.Concat(tile).ToArray();
        }

        var t = temp.ToList();
        
            
        return t.Distinct().ToArray();
    }

    /// <summary>
    /// Check if its a collider cell
    /// </summary>
    /// <param name="isCollider">Boolean statement if the cell is a collider</param>
    private void SetCollider(bool isCollider)
    {
        
        this.isCollider = isCollider;
    }
}
