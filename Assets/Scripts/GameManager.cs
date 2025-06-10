using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    public static GameManager Instance { get; private set; }

    public BoardManager boardManager;
    public PlayerCharacterController playerCharacterController;
    public UIDocument UIDocument;
    private Label m_FoodLabel;
    private int m_FoodAmount = 100;
    private int m_CurrentLevel = 1;
    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;

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
        TurnManager.OnTick += OnTurnHappen;

        NewLevel();

        m_FoodLabel = UIDocument.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Food : " + m_FoodAmount;
        m_GameOverPanel = UIDocument.rootVisualElement.Q<VisualElement>("GameOver");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverLabel");

        m_GameOverPanel.style.visibility = Visibility.Hidden;
        StartNewGame();
    }

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        if (m_FoodAmount <= 0)
        {
            playerCharacterController.GameOver();
            m_GameOverPanel.style.visibility = Visibility.Visible;
            m_GameOverMessage.text = "Game Over!\n\nSurvived " + m_CurrentLevel + " days\n\n Press Enter to retry"; 

        }
    }

    public void NewLevel()
    {
        boardManager.Clean();
        boardManager.Init();
        playerCharacterController.Spawn(boardManager, new Vector2Int(1, 1));

        m_CurrentLevel++;
    }


    public void StartNewGame()
    {
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        m_CurrentLevel = 1;
        m_FoodAmount = 20;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        boardManager.Clean();
        boardManager.Init();

        playerCharacterController.Init();
        playerCharacterController.Spawn(boardManager, new Vector2Int(1, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
