using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cell;
    public int width = 10;
    public int height = 30;
    public int clearedRowCountAtOnce;

    private readonly int header = 10;
    private static string cellName = "Board Cell ({0} {1})";
    private Transform[,] grids;

    private void Awake()
    {
        grids = new Transform[width, height];
    }

    void Start()
    {
        DrawBoardEmptyCells();
    }

    private void DrawBoardEmptyCells()
    {
        for (int y = 0; y < height - header; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Transform newCell = Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);
                newCell.name = string.Format(cellName, x, y);
                newCell.transform.parent = this.transform;
            }
        }

    }


    private bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }


    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (IsOccupied((int)pos.x, (int)pos.y, shape))
                return false;
        }

        return true;
    }



    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null) return;

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            grids[(int)pos.x, (int)pos.y] = child;
        }

    }


    public bool IsOccupied(int x, int y, Shape shape)
    {
        return grids[x, y] != null && grids[x, y].parent != shape.transform;
    }

    private bool IsComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grids[x, y] == null)
                return false;

        }
        return true;
    }

    private void ClearOneRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grids[x, y] != null)
            {
                Destroy(grids[x, y].gameObject);
                grids[x, y] = null;
            }

        }

    }

    private void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grids[x, y] != null)
            {
                grids[x, y - 1] = grids[x, y];
                grids[x, y] = null;
                grids[x, y - 1].position += Vector3.down;
            }
        }

    }

    private void ShiftAllRowDown(int startY)
    {
        for (int i = startY; i < height; i++)
        {
            ShiftOneRowDown(i);
        }

    }

    public void ClearAllRow()
    {
        clearedRowCountAtOnce = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsComplete(y))
            {
                clearedRowCountAtOnce++;
                ClearOneRow(y);
                ShiftAllRowDown(y + 1);
                y--;
            }
        }

    }

    public bool IsOverLimit(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y > height - header - 1)
                return true;
        }

        return false;

    }

}
