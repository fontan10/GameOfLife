using UnityEngine;

public interface IGridManagerable
{
    int Resolution { get; set; } // number of squares in the x
    Sprite TileSprite { get; set; }
    GameObject Tiles { get; set; }

    void UpdateGrids();
    void CreateGrids();
    void SetAlive(int col, int row);
    void SetDead(int col, int row);
}
