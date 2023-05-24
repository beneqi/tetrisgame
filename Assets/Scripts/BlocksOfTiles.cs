using UnityEngine.Tilemaps;
using System;
using UnityEngine;

public enum BlockOfTiles
{
    First,
    Second,
    Third,
    Fourth,
    Fifth,
    Sixth,
    Seventh,
}

[Serializable]
public struct BlockOfTileInfos
{
    public BlockOfTiles blockOfTiles;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallKicks { get; private set; }


    public void Init()
    {
        this.cells = Info.Cells[this.blockOfTiles];
        this.wallKicks = Info.WallKicks[this.blockOfTiles];
    }
}