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
        // note that there are non () here, because we are not using the MonoBehaviour version of TurnManager
        TurnManager.OnTick += OnTurnHappen;

        boardManager.Init();
        playerCharacterController.Spawn(boardManager, new Vector2Int(1, 1));

        m_FoodLabel = UIDocument.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Food : " + m_FoodAmount;
    }

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Food : " + m_FoodAmount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
