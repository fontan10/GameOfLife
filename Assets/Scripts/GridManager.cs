using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float speed = 3;

    [Range(5, 200), SerializeField]
    private int resolution = 100; // number of squares in the x

    [SerializeField]
    public Sprite _sprite;

    private GameObject _tiles;

    private GameObject[,] _tileGrid;
    private bool[,] _aliveGrid, _newAliveGrid;

    private int _columns, _rows;
    private float _screenWidth, _screenHeight;
    private float _tileWidth, _tileHeight;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrids();

        InvokeRepeating("UpdateGrids", speed, speed);
    }

    public void Restart()
    {
        Destroy(_tiles);

        CreateGrids();
    }

    private void UpdateGrids()
    {
        for (int i = 0; i < _columns; ++i)
        {
            for (int j = 0; j < _rows; ++j)
            {
                int numAliveNeighbours = FindNumberAliveNeighbours(i, j);

                if (_aliveGrid[i, j])
                {
                    _newAliveGrid[i, j] = numAliveNeighbours == 2 || numAliveNeighbours == 3;
                }
                else
                {
                    _newAliveGrid[i, j] = numAliveNeighbours == 3;
                }


                _tileGrid[i, j].GetComponent<SpriteRenderer>().color = _newAliveGrid[i, j] ? Color.yellow : Color.black;
            }
        }

        var temp = _aliveGrid;
        _aliveGrid = _newAliveGrid;
        _newAliveGrid = temp;
    }

    private int FindNumberAliveNeighbours(int x, int y)
    {
        int numAliveNeighbours = 0;
        for(int i = -1; i < 2; ++i)
        {
            for(int j = -1; j < 2; ++j)
            {
                int col = (x + j + _columns) % _columns;
                int row = (y + i + _rows) % _rows;

                if(_aliveGrid[col, row])
                {
                    numAliveNeighbours++;
                }
            }
        }

        if (_aliveGrid[x, y])
        {
            numAliveNeighbours--;
        }

        return numAliveNeighbours;
    }

    private void CreateGrids()
    {
        _columns = resolution;
        _rows = (int)(_columns / Camera.main.aspect);

        _screenHeight = Camera.main.orthographicSize * 2;
        _screenWidth = Camera.main.aspect * _screenHeight;

        _tileWidth = _screenWidth / _columns;
        _tileHeight = _screenHeight / _rows;

        _tileGrid = new GameObject[_columns, _rows];
        _aliveGrid = new bool[_columns, _rows];
        _newAliveGrid = new bool[_columns, _rows];

        _tiles = new GameObject("Tiles");

        for (int i = 0; i < _columns; ++i)
        {
            for (int j = 0; j < _rows; ++j)
            {
                bool alive = UnityEngine.Random.Range(0, 2) == 1;

                _aliveGrid[i, j] = alive;

                float x = (i + 0.5f) * _tileWidth - _screenWidth / 2;
                float y = (j + 0.5f) * _tileHeight - _screenHeight / 2;

                _tileGrid[i, j] = CreateTile(x, y, alive, i, j);
            }
        }
    }

    // Creates a tile GameObject at (x,y)
    private GameObject CreateTile(float x, float y, bool alive, int i, int j)
    {
        var gameObject = new GameObject("(" + i + ", " + j + ")");
        gameObject.transform.position = new Vector3(x, y);
        gameObject.transform.localScale = new Vector3(_tileWidth, _tileHeight, 1);

        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = _sprite;
        spriteRenderer.color = alive ? Color.yellow : Color.black;

        gameObject.transform.SetParent(_tiles.transform);

        return gameObject;
    }
}
