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

    private void Update()
    {
        this.panel.Clear(this);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }

        this.panel.Set(this);
    }

    private bool Move(Vector2Int transportation)
    {
        Vector3Int newPos = this.position;
        newPos.x += transportation.x;
        newPos.y += transportation.y;

        bool valid = this.panel.IsValidPos(this, newPos);

        if (valid)
        {
            this.position = newPos;
        }

        return valid;
    }
}
