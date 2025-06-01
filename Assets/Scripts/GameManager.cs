using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    public static GameManager Instance { get; private set; }

    public BoardManager boardManager;
    public PlayerCharacterController playerCharacterController;

    public static TurnManager TurnManager { get; private set; }



    // Awake is called when the script instance is being loaded before start()
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnManager = new TurnManager();

        boardManager.Init();
        playerCharacterController.Spawn(boardManager, new Vector2Int(1, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
