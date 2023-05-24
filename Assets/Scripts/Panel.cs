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
    public Vector2Int boundarySize = new Vector2Int(10, 20);

    public RectInt Boundary
    {
        get
        {
            Vector2Int position = new Vector2Int(-boundarySize.x / 2, -boundarySize.y / 2);
            return new RectInt(position, boundarySize);
        }
    }
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
        if (IsValidPos(activePiece, blockPos))
        {
            Set(activePiece);
        }
        else
        {
            GameOver();
        }

        //Set(this.activePiece);

    }

    public void GameOver()
    {
            this.tilemap.ClearAllTiles();
    }

    public void Set(Piece piece)
    {
        for(int i=0; i<piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i]+piece.position;
            this.tilemap.SetTile(tilePos, piece.info.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePos, null);
        }
    }

    public bool IsValidPos(Piece piece, Vector3Int pos)
    {
        RectInt boundary = this.Boundary;
        for(int i=0; i<piece.cells.Length; i++)
        {
            Vector3Int blockPos = piece.cells[i] + pos;

            if (!boundary.Contains((Vector2Int)blockPos))
                return false;

            if (this.tilemap.HasTile(blockPos))
                return false;
        }

        return true;
    }
    public void ClearLines()
    {
        RectInt boundary = this.Boundary;
        int line = boundary.yMin;

        while(line < boundary.yMax)
        {
            if (IsLineFull(line))
            {
                LineClear(line);
            }
            else
            {
                line++;
            }
        }
    }

    private void LineClear(int line)
    {
        RectInt boundary = this.Boundary;

        
        for (int column = boundary.xMin; column < boundary.xMax; column++)
        {
            Vector3Int position = new Vector3Int(column, line, 0);
            this.tilemap.SetTile(position, null);
        }

        while (line < boundary.yMax)
        {
            for (int column = boundary.xMin; column < boundary.xMax; column++)
            {
                Vector3Int position = new Vector3Int(column, line + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(column, line, 0);
                this.tilemap.SetTile(position, above);
            }

            line++;
        }
    }

    public bool IsLineFull(int line)
    {
        RectInt boundary = this.Boundary;

        for (int columnumn = boundary.xMin; columnumn < boundary.xMax; columnumn++)
        {
            Vector3Int position = new Vector3Int(columnumn, line, 0);

            
            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

}
