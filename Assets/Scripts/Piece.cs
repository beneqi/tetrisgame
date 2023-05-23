using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Panel panel { get; private set; }
    public Vector3Int position { get; private set; }
    public BlockOfTileInfos info { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public void Init(Panel panel, Vector3Int position, BlockOfTileInfos info)
    {
        this.panel = panel;
        this.position = position;
        this.info = info;

        if(this.cells == null)
            this.cells = new Vector3Int[info.cells.Length];

        for(int i=0; i<info.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)info.cells[i];
        }

    }
}
