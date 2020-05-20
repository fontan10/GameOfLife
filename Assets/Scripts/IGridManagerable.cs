using UnityEngine;

public interface IGridManagerable<TAlive>
{
    int Resolution { get; set; } // number of squares in the x

    Sprite TileSprite { get; set; }
    GameObject Tiles { get; set; }

    void UpdateGrids();
    void CreateGrids();
}
