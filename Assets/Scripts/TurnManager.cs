using UnityEngine;

public class TurnManager
{
    public event System.Action OnTick;
    private int m_CurrentTurn;

    public TurnManager()
    {
        m_CurrentTurn = 0;
    }

    public void Tick()
    {
        m_CurrentTurn++;
        Debug.Log($"Turn {m_CurrentTurn} started.");
        OnTick?.Invoke();
    }
}
