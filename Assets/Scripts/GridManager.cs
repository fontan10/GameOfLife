using UnityEngine;

public abstract class GridManager<TAlive> : IGridManagerable
{
    /// <summary> Number of TileSprites in the x direction. </summary>
    public int Resolution { get; set; }

    /// <summary> Sprite to be displayed when Tile is alive. </summary>
    public Sprite TileSprite { get; set; }

    /// <summary>
    /// 2d Array holding the alive state of the tiles currently on screen.
    /// </summary>
    protected TAlive[,] AliveGrid { get; set; }

    public GameObject Tiles { get; set; }
    public abstract void CreateGrids();
    public abstract void UpdateGrids();
    public abstract void SetAlive(int col, int row);
    public abstract void SetDead(int col, int row);


    protected int columns, rows;
    protected float screenHeight, screenWidth, tileWidth, tileHeight;


    protected abstract Color ChooseColor(TAlive alive);
    protected abstract bool IsAlive(TAlive alive);

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

    /// <summary>
    /// Returns the number of alive neighbours beside the <c>TileSprite</c>
    /// at (x, y).
    /// </summary>
    /// <param name="x">x grid position</param>
    /// <param name="y">y grid position</param>
    /// <param name="numColumns">number of columns in the grid</param>
    /// <param name="numRows">number of rows in the grid</param>
    /// <returns>number of alive neighbours</returns>
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

    /// <summary>
    /// Creates a <c>Tile</c> <c>GameObject</c> named "(i, j)" at 
    /// screen pixel (<c>x</c>,<c>y</c>) and sets it to <c>alive</c>.
    /// </summary>
    /// <returns> The GameObject created </returns>
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
