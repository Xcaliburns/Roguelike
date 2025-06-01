using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    public Tile ObstacleTile; 
    public Tile halfDestroyedTile;
    public int MaxHealth = 3;

    private int m_HealthPoint;
    private Tile m_OriginalTile;

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);

        m_HealthPoint = MaxHealth;

        m_OriginalTile = GameManager.Instance.boardManager.GetCellTile(cell);
        GameManager.Instance.boardManager.SetCellTile(cell, ObstacleTile);
    }


    public override bool PlayerWantsToEnter()
    {
        m_HealthPoint -= 1;

        if (m_HealthPoint > 0)
        {
            // Change the tile if health is equal to 1
            if (m_HealthPoint ==1)
            {
                GameManager.Instance.boardManager.SetCellTile(m_Cell, halfDestroyedTile);
            }
            return false;
        }

        // When destroyed, restore the original tile and destroy the object
        GameManager.Instance.boardManager.SetCellTile(m_Cell, m_OriginalTile);
        Destroy(gameObject);
        return true;
    }
}
