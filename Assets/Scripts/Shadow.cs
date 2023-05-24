using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shadow : MonoBehaviour
{
    public Tile tile;
    public Panel panel;
    public Piece piece;
    public Tilemap tilemap { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }
    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = this.piece.cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = this.piece.position;

        int current = position.y;
        int bottom = -this.panel.boundarySize.y / 2 - 1;

        this.panel.Clear(this.piece);

        for (int line = current; line >= bottom; line--)
        {
            position.y = line;

            if (this.panel.IsValidPos(this.piece, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }

        this.panel.Set(this.piece);
    }

    private void Set()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}
