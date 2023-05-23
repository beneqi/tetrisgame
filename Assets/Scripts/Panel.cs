using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Panel : MonoBehaviour
{
    public BlockOfTileInfos[] blockOfTiles;
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public Vector3Int blockPos;

    private void Awake()
    {
        for(int i= 0; i < blockOfTiles.Length; i++)
        {
            this.tilemap = GetComponentInChildren<Tilemap>();
            this.activePiece = GetComponentInChildren<Piece>();
            this.blockOfTiles[i].Init();
        }
    }

    private void Start()
    {
        GenerateBlocks();
    }

    public void GenerateBlocks()
    {
        int rand = Random.Range(0, this.blockOfTiles.Length);

        BlockOfTileInfos blockOfTileInfos = this.blockOfTiles[rand];

        this.activePiece.Init(this, this.blockPos, blockOfTileInfos);
        Set(this.activePiece);

    }

    public void Set(Piece piece)
    {
        for(int i=0; i<piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i]+piece.position;
            this.tilemap.SetTile(tilePos, piece.info.tile);
        }
    }
}
