using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileContacts
{
    // INDEX FOR contactTypes
    // 0: UP    1: Right    2: Down     3: Left
    
    [SerializeField] private Tile tile;
    [SerializeField] public List<Tile> posX = new List<Tile>();
    [SerializeField] public List<Tile> negX = new List<Tile>();
    [SerializeField] public List<Tile> posY = new List<Tile>();
    [SerializeField] public List<Tile> negY = new List<Tile>();
    [SerializeField] public bool isCollider;
    private int[] direction = new int[4];
    
    public Tile ContactTile => tile;

    public HashSet<Tile> PosX => new HashSet<Tile>(posX);
    public HashSet<Tile> PosY => new HashSet<Tile>(posY);
    public HashSet<Tile> NegX => new HashSet<Tile>(negX);
    public HashSet<Tile> NegY => new HashSet<Tile>(negY);
    public int[] Direction => direction;
    private int ContactNum { get;  set; }
    

    
}
