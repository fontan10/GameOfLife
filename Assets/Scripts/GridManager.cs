using UnityEngine;

public abstract class GridManager<TAlive> : IGridManagerable
{
    public float Speed { get; set; }
    public int Resolution { get; set; }
    public GameObject Tiles { get; set; }
    public Sprite TileSprite { get; set; }
    protected TAlive[,] AliveGrid { get; set; }

    public abstract void CreateGrids();
    public abstract void UpdateGrids();
    protected abstract Color ChooseColor(TAlive alive);
    protected abstract bool IsAlive(TAlive alive);

    protected int columns, rows;
    protected float screenHeight, screenWidth, tileWidth, tileHeight;

    protected virtual void InitializeVariables()
    {
        columns = Resolution;
        rows = (int)(columns / Camera.main.aspect);
        screenHeight = 2 * Camera.main.orthographicSize;
        screenWidth = Camera.main.aspect * screenHeight;
        tileWidth = screenWidth / columns;
        tileHeight = screenHeight / rows;
        AliveGrid = new TAlive[columns, rows];
    }

    protected int FindNumberAliveNeighbours(int x, int y, int numColumns, int numRows)
    {
        int numAliveNeighbours = 0;
        for (int i = -1; i < 2; ++i)
        {
            for (int j = -1; j < 2; ++j)
            {
                int col = (x + j + numColumns) % numColumns;
                int row = (y + i + numRows) % numRows;

                if (IsAlive(AliveGrid[col, row]))
                {
                    numAliveNeighbours++;
                }
            }
        }

        if (IsAlive(AliveGrid[x, y]))
        {
            numAliveNeighbours--;
        }

        return numAliveNeighbours;
    }

    // Creates a tile GameObject at (x,y)
    protected GameObject CreateTile(float x, float y, TAlive alive, int i, int j)
    {
        var gameObject = new GameObject("(" + i + ", " + j + ")");
        gameObject.transform.position = new Vector3(x, y);
        gameObject.transform.localScale = new Vector3(tileWidth, tileHeight, 1);

        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = TileSprite;
        spriteRenderer.color = ChooseColor(alive);

        gameObject.transform.SetParent(Tiles.transform);

        return gameObject;
    }
}
