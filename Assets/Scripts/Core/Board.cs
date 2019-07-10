using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 10;
    public int height = 30;
    public Transform cell;



    //PRIVATE
    private static string cellName = "Board Cell ({0} {1})";
    private Transform[,] grids;

    private void Awake()
    {
        grids = new Transform[height, width];
    }

    void Start()
    {
        DrawBoardEmptyCells();
    }

    private void DrawBoardEmptyCells()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Transform newCell = Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);
                newCell.name = string.Format(cellName, x, y);
                newCell.transform.parent = this.transform;
                grids[y,x] = newCell;

            }
        }

    }


}
