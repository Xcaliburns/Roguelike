using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
    public Tile ExitTile; // Tile to represent the exit
    public bool IsExit = true; // Indicates if this cell is an exit

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        GameManager.Instance.boardManager.SetCellTile(cell, ExitTile);
    }
    public override bool PlayerWantsToEnter()
    {
        // If the player wants to enter the exit cell, return true
        return IsExit;
    }
    public override void PlayerEntered()
    {
        GameManager.Instance.NewLevel();
    }

}
