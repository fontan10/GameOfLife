using UnityEngine;

/// <summary> Handles the states of the <c>TileSprites</c> on screen. </summary>
public interface IGridManagerable
{
    /// <summary> Number of TileSprite in the x axis. </summary>
    int Resolution { get; set; }

    /// <summary> Image that will be displayed for a tile. </summary>
    Sprite TileSprite { get; set; }
    /// <summary> <c>GameObject</c> to parent all <c>TileSprite</c>s. </summary>
    GameObject Tiles { get; set; }


    /// <summary> Update grid of <c>TileSprite</c>s on screen to their next state. </summary>
    void UpdateGrids();

    /// <summary> Creates grid of <c>TileSprite</c>s on screen. </summary>
    void CreateGrids();
    /// <summary> Sets <c>TileSprite</c> at <c>col</c>, <c>row</c> alive. </summary>
    void SetAlive(int col, int row);
    /// <summary> Sets <c>TileSprite</c> at <c>col</c>, <c>row</c> dead. </summary>
    void SetDead(int col, int row);
}
