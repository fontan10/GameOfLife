using UnityEngine;

public class MonoColourGridManager : GridManager<bool>
{
    private GameObject[,] _tileGrid;
    private bool[,] _newAliveGrid;

    protected override Color ChooseColor(bool alive)
    {
        return alive ? Color.yellow : Color.black;
    }

    public override void CreateGrids()
    {
        InitializeVariables();

        _tileGrid = new GameObject[columns, rows];
        _newAliveGrid = new bool[columns, rows];

        Tiles = new GameObject("Tiles");

        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                bool alive = Random.Range(0, 2) == 1;

                AliveGrid[i, j] = alive;

                float x = (i + 0.5f) * tileWidth - screenWidth / 2;
                float y = (j + 0.5f) * tileHeight - screenHeight / 2;

                _tileGrid[i, j] = CreateTile(x, y, alive, i, j);
            }
        }
    }

    protected override bool IsAlive(bool alive)
    {
        return alive;
    }

    public override void UpdateGrids()
    {
        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                int numAliveNeighbours = FindNumberAliveNeighbours(i, j, columns, rows);

                _newAliveGrid[i, j] = numAliveNeighbours == 3 || (AliveGrid[i, j] && numAliveNeighbours == 2);

                _tileGrid[i, j].GetComponent<SpriteRenderer>().color = ChooseColor(_newAliveGrid[i, j]);
            }
        }

        var temp = AliveGrid;
        AliveGrid = _newAliveGrid;
        _newAliveGrid = temp;
    }

    public override void SetAlive(int col, int row)
    {
        AliveGrid[col, row] = true;
        _tileGrid[col, row].GetComponent<SpriteRenderer>().color = ChooseColor(AliveGrid[col, row]);
    }

    public override void SetDead(int col, int row)
    {
        AliveGrid[col, row] = false;
        _tileGrid[col, row].GetComponent<SpriteRenderer>().color = ChooseColor(AliveGrid[col, row]);
    }
}
