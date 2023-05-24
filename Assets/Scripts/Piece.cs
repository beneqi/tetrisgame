using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Panel panel { get; private set; }
    public Vector3Int position { get; private set; }
    public BlockOfTileInfos info { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public int RotationValue { get; private set; }
    public float paceDelay = 1.0f;
    public float lockDelay = 0.5f;
    public float paceTime, lockTime;


    public void Init(Panel panel, Vector3Int position, BlockOfTileInfos info)
    {
        this.RotationValue = 0;
        this.panel = panel;
        this.position = position;
        this.info = info;
        this.paceTime = Time.time + this.paceDelay;
        this.lockTime = 0.0f;


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
        this.lockTime += Time.deltaTime;
           
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rotation(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Rotation(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        /*else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Rotation(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Rotation(1);
        }*/

        if (Time.time >= this.paceTime)
            Pace();

        this.panel.Set(this);
    }

    private void Pace()
    {
        this.paceTime = Time.time + this.paceDelay;
        Move(Vector2Int.down);

        if (this.lockTime >= this.lockDelay)
            Lock();
    }

    private void Lock()
    {
        this.panel.Set(this);
        this.panel.ClearLines();
        this.panel.GenerateBlocks();
    }
    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
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
            this.lockTime = 0.0f;
        }

        return valid;
    }

    private void Rotation(int direction)
    {
        int baseRotation = this.RotationValue;
        this.RotationValue = Wrap(this.RotationValue + direction, 0, 4);

        ApplyRoationMatrix(direction);

        if(!TestWallKicks(this.RotationValue, direction))
        {
            this.RotationValue = baseRotation;
            ApplyRoationMatrix(-direction);
        }


    }

    private void ApplyRoationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];

            int x;
            int y;

            switch (this.info.blockOfTiles)
            {
                case BlockOfTiles.First:
                case BlockOfTiles.Fourth:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Info.RotationMatrix[0] * direction) + (cell.y * Info.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Info.RotationMatrix[2] * direction) + (cell.y * Info.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Info.RotationMatrix[0] * direction) + (cell.y * Info.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Info.RotationMatrix[2] * direction) + (cell.y * Info.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }

    private bool TestWallKicks(int rotationValue, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationValue, rotationDirection);
        
        for(int i=0; i<this.info.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = this.info.wallKicks[wallKickIndex, i];

            if (Move(translation))
                return true;
        }

        return false;
    }
    private int GetWallKickIndex(int rotationValue, int rotationDirection)
    {
        int wallKickIndex = rotationValue * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.info.wallKicks.GetLength(0));

    }

 
}
