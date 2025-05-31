using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    private BoardManager m_Board;
    private Vector2Int m_CellPosition;

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager; ;
        m_CellPosition = cell;

        transform.position = m_Board.CellToWorld(cell);

    }

}
