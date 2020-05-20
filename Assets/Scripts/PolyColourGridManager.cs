using UnityEngine;

public class PolyColourGridManager : GridManager<int>
{
    private GameObject[,] _tileGrid;
    private int[,] _newAliveGrid;

    protected override Color ChooseColor(int alive)
    {
        if(alive == 0)
        {
            return Color.black;
        }

        float hue = (float)(alive % 100) / 100f;
        float saturation = Mathf.Clamp((float)alive / 100f, 0f, 1f);
        float value = Mathf.Clamp((float)alive / 200f, 0.1f, 1f);

        return Color.HSVToRGB(hue, saturation, value);
    }

    public override void CreateGrids()
    {
        InitializeVariables();

        _tileGrid = new GameObject[columns, rows];
        _newAliveGrid = new int[columns, rows];

        Tiles = new GameObject("Tiles");

        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                int alive = Random.Range(0, 2);

                AliveGrid[i, j] = alive;

                float x = (i + 0.5f) * tileWidth - screenWidth / 2;
                float y = (j + 0.5f) * tileHeight - screenHeight / 2;

                _tileGrid[i, j] = CreateTile(x, y, alive, i, j);
            }
        }
    }

    protected override bool IsAlive(int alive)
    {
        return alive != 0;
    }

    public override void UpdateGrids()
    {
        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                int numAliveNeighbours = FindNumberAliveNeighbours(i, j, columns, rows);

                if(numAliveNeighbours == 3 ||
                        (AliveGrid[i, j] !=0 && numAliveNeighbours == 2))
                {
                    _newAliveGrid[i, j] = AliveGrid[i, j] + 1;
                }
                else
                {
                    _newAliveGrid[i, j] = 0;
                }

                _tileGrid[i, j].GetComponent<SpriteRenderer>().color = ChooseColor(_newAliveGrid[i, j]);
            }
        }

        var temp = AliveGrid;
        AliveGrid = _newAliveGrid;
        _newAliveGrid = temp;
    }

    public override void SetAlive(int col, int row)
    {
        AliveGrid[col, row] = 1;
        _tileGrid[col, row].GetComponent<SpriteRenderer>().color = ChooseColor(AliveGrid[col, row]);
    }

    public override void SetDead(int col, int row)
    {
        AliveGrid[col, row] = 0;
        _tileGrid[col, row].GetComponent<SpriteRenderer>().color = ChooseColor(AliveGrid[col, row]);
    }
}
