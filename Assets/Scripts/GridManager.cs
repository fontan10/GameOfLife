using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GridManager : MonoBehaviour
{
    public Sprite sprite;

    [Range(10, 100)]
    public int resolution; // number of squares in the x

    private GameObject[,] tileGrid;
    private bool[,] aliveGrid, newAliveGrid;

    private int columns, rows;
    private float screenWidth, screenHeight;
    private float tileWidth, tileHeight;

    // Start is called before the first frame update
    void Start()
    {
        (tileGrid, aliveGrid) = CreateGrids();
        newAliveGrid = new bool[columns, rows];

        InvokeRepeating("UpdateGrids", 0.2f, 0.2f);
    }

    private void UpdateGrids()
    {
        UpdateAliveGrid();
        UpdateTileGrid();
    }

    private void UpdateTileGrid()
    {
        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                tileGrid[i, j].GetComponent<SpriteRenderer>().color = aliveGrid[i, j] ? Color.yellow : Color.black;
            }
        }
    }

    private void UpdateAliveGrid()
    {
        // Go through the aliveGrid and insert the new states in newAliveGrid
        for (int i = 0; i < columns; ++i)
        {
            for(int j = 0; j < rows; ++j)
            {
                int numAliveNeighbours = FindNumberAliveNeighbours(i, j);

                if (aliveGrid[i, j])
                {
                    newAliveGrid[i, j] = numAliveNeighbours == 2 || numAliveNeighbours == 3;
                }
                else
                {
                    newAliveGrid[i, j] = numAliveNeighbours == 3;
                }
            }
        }

        aliveGrid = newAliveGrid;
    }

    private int FindNumberAliveNeighbours(int x, int y)
    {
        int numAliveNeighbours = 0;
        for(int i = -1; i < 2; ++i)
        {
            for(int j = -1; j < 2; ++j)
            {
                int row = (x + i + rows) % rows;
                int col = (y + j + columns) % columns;
                if(aliveGrid[col, row])
                {
                    numAliveNeighbours++;
                }
            }
        }

        return numAliveNeighbours - Convert.ToInt32(aliveGrid[x, y]);
    }

    private (GameObject[,], bool[,]) CreateGrids()
    {
        rows = resolution;
        columns = (int)(rows * Camera.main.aspect);

        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = Camera.main.aspect * screenHeight;

        tileWidth = screenWidth / columns;
        tileHeight = screenHeight / rows;

        var tileGrid = new GameObject[columns, rows];
        var aliveGrid = new bool[columns, rows];

        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                bool alive = UnityEngine.Random.Range(0, 2) == 1;

                aliveGrid[i, j] = alive;

                float x = (i + 0.5f) * tileWidth - screenWidth / 2;
                float y = (j + 0.5f) * tileHeight - screenHeight / 2;

                tileGrid[i, j] = CreateTile(x, y, alive);
            }
        }

        return (tileGrid, aliveGrid);
    }

    // Creates a tile GameObject at (x,y)
    private GameObject CreateTile(float x, float y, bool alive)
    {
        var gameObject = new GameObject("(" + x + ", " + y + ")");
        gameObject.transform.position = new Vector3(x, y);
        gameObject.transform.localScale = new Vector3(tileWidth, tileHeight, 1);

        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = alive ? Color.yellow : Color.black;

        gameObject.transform.SetParent(GameObject.Find("Tiles").transform);

        return gameObject;
    }
}
