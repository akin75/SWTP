using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCell
{
    // Start is called before the first frame update
    public List<TileContacts> tileContacts;
    public Vector3Int positionInMap;
    private Tilemap tilemap;
    public int entropy;
    public TileBase[] tileArray;
    public Tile tileCell;
    private Vector2Int mapSize;
    public bool isCollider;
    private List<TileBase[]> tileToJoin = new List<TileBase[]>();
    public bool isCollapsed;
    public TileMapCell(List<TileContacts> tile, Vector3Int position, Tilemap tilemap, Vector2Int mapSize)
    {
        tileContacts = tile;
        this.tilemap = tilemap;
        positionInMap = position;
        //tileArray = RemoveDuplicate(tileArray);
        this.mapSize = mapSize;
        this.isCollapsed = false;
    }
    

    public TileMapCell(int entropy)
    {
        this.entropy = entropy;
    }

    public void Collapse(int piece)
    {
        
        Debug.Log("TT" + piece + " DEbug : " + tileContacts.Count);
        tileCell = tileContacts[piece].ContactTile;
        tileContacts.RemoveAll(x => x.ContactTile != tileCell);
        SetCollider(tileContacts.Find(x => x.ContactTile == tileCell).isCollider);
        isCollapsed = true;
        //Debug.Log("t; " + tileArray.Length);
    }

    public List<Vector3Int> GetAdjacentMapCell()
    {
        List<Vector3Int> cells = new List<Vector3Int>();
        if (positionInMap.x - 1 >= 0) cells.Add(new Vector3Int(positionInMap.x-1, positionInMap.y));
        if (positionInMap.x + 1 < mapSize.x) cells.Add(new Vector3Int(positionInMap.x+1, positionInMap.y));
        if (positionInMap.y - 1 >= 0) cells.Add(new Vector3Int(positionInMap.x, positionInMap.y-1));
        if (positionInMap.y + 1 < mapSize.y) cells.Add(new Vector3Int(positionInMap.x, positionInMap.y+1));
        return cells;
    }


    public List<TileContacts> GetPossibilities()
    {
        return tileContacts;
    }

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
            // Debug.Log("TileContactsLength: " + tileContacts.Count);
            var contacts = tileContacts[0];
            if (direction == DirectionEnum.Forward) possibleNeighbours.Add(contacts.PosY.ToArray());
            if (direction == DirectionEnum.Right) possibleNeighbours.Add(contacts.PosX.ToArray());
            if (direction == DirectionEnum.Down) possibleNeighbours.Add(contacts.NegY.ToArray());
            if (direction == DirectionEnum.Left) possibleNeighbours.Add(contacts.NegX.ToArray());
        }

        return JoinArray(possibleNeighbours).ToList();
    }


    public void FilterTileArray(HashSet<Tile> toFilter, bool toJoin) // TODO
    {
        
    }
    

    public TileBase[] RemoveDuplicate(TileBase[] sourceTile)
    {
        return sourceTile.ToList()
            .GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToArray();
    }

    public void Constrain(TileContacts toDelete)
    {
        if (isCollapsed)
        {
            return;
        }
        //Debug.Log("Position: " + positionInMap.y + " , " + positionInMap.x + "  Length" + tileContacts.Count);
        tileContacts.Remove(toDelete);
        //Debug.Log("Length After: " + tileContacts.Count);
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

    private void SetCollider(bool isCollider)
    {
        
        this.isCollider = isCollider;
    }
    private void SetTileArray(TileBase[] tileBases)
    {
        tileArray = tileBases;
    }
    
}
