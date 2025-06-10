using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerCharacterController : MonoBehaviour
{

    private BoardManager m_Board;
    private Vector2Int m_CellPosition;
    private bool m_IsGameOver;

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager; ;
        m_CellPosition = cell;

        transform.position = m_Board.CellToWorld(cell);

    }
    public void Init()
    {
        m_IsGameOver = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;
        // Check if the game is over
        if (m_IsGameOver)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameManager.Instance.StartNewGame();
            }

            return;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y++;
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y--;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x--;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x++;
            hasMoved = true;
        }

        if (hasMoved)
        {
            //check if the new cell is within bounds and passable
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);

             if (cellData != null && cellData.Passable)
            {
                GameManager.TurnManager.Tick();
                //if the cell is passable, move the player
                if(cellData.ContainedObject== null)
                {
                    MoveTo(newCellTarget);
                }

                else if (cellData.ContainedObject.PlayerWantsToEnter())
                {                                   
                    MoveTo(newCellTarget);
                    //if the cell has an object, call PlayerWantsToEnter
                    cellData.ContainedObject.PlayerEntered();
                }
               
            }

        }
    }

    public void MoveTo(Vector2Int cell)
    {
        m_CellPosition = cell;

        transform.position = m_Board.CellToWorld(m_CellPosition);
    }

    public void GameOver()
    {
        m_IsGameOver = true;
    }
}