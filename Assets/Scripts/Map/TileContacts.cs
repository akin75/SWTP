using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileContacts
{
    // INDEX FOR contactTypes
    // 0: UP    1: Right    2: Down     3: Left
    [SerializeField] private List<Tile> contactTypes;
    [SerializeField] private Tile tile;
    
    
    public Tile ContactTile => tile;
    public List<Tile> ContactTypes => contactTypes;


    public bool IsEqual(TileContacts other)
    {
        return contactTypes.Contains(other.ContactTile) && other.contactTypes.Contains(ContactTile);
    }

}
