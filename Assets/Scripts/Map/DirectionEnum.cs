using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  DirectionEnum
{
    public static Vector3Int Forward => new Vector3Int(0, 1);
    public static Vector3Int Right => new Vector3Int(1, 0);
    public static Vector3Int Left => new Vector3Int(-1, 0);
    public static Vector3Int Down => new Vector3Int(0, -1);
}
