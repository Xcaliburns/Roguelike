using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;


public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool Passable;
        public CellObject ContainedObject;
    }

    private CellData[,] m_BoardData;
    private Tilemap m_Tilemap;
    private Grid m_Grid;
    private List<Vector2Int> m_EmptyCellsList;

    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] WallTiles;
    public FoodObject[] FoodPrefabs; // Array of different food prefabs

    public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_Grid = GetComponentInChildren<Grid>();
        m_EmptyCellsList = new List<Vector2Int>();

        m_BoardData = new CellData[Width, Height];

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                    m_EmptyCellsList.Add(new Vector2Int(x, y));
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        m_EmptyCellsList.Remove(new Vector2Int(1, 1));
        GenerateFood();
    }

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width
            || cellIndex.y < 0 || cellIndex.y >= Height)
        {
            return null;
        }

        return m_BoardData[cellIndex.x, cellIndex.y];
    }

    void GenerateFood()
    {
        if (FoodPrefabs == null || FoodPrefabs.Length == 0)
        {
            Debug.LogError("FoodPrefabs array is not assigned or empty!");
            return;
        }

        int foodCount = Random.Range(2, 10);
        Debug.Log($"Generating {foodCount} food items on the board.");
        Debug.Log($"Empty cells available: {m_EmptyCellsList.Count}");
        Debug.Log($"FoodPrefabs length: {FoodPrefabs.Length}");
        for (int j = 0; FoodPrefabs != null && j < FoodPrefabs.Length; ++j)
        {
            Debug.Log($"FoodPrefabs[{j}]: {FoodPrefabs[j]}");
        }
        

        for (int i = 0; i < foodCount && m_EmptyCellsList.Count > 0; ++i)
        {
            int randomIndex = Random.Range(0, m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];

            m_EmptyCellsList.RemoveAt(randomIndex);
            CellData data = m_BoardData[coord.x, coord.y];

            // Choose a random food prefab
            FoodObject foodPrefab = FoodPrefabs[Random.Range(0, FoodPrefabs.Length)];
            Debug.Log($"Instantiating prefab: {foodPrefab}, type: {(foodPrefab != null ? foodPrefab.GetType().ToString() : "null")}");

            if (foodPrefab == null)
            {
                Debug.Log($"FoodPrefabs length: {(FoodPrefabs != null ? FoodPrefabs.Length : 0)}");
                for (int y = 0; FoodPrefabs != null && y < FoodPrefabs.Length; ++y)
                {
                    Debug.Log($"FoodPrefabs[{y}]: {FoodPrefabs[y]}");
                }
                Debug.LogError("A FoodPrefab is null in the FoodPrefabs array!");
                continue;
            }
            FoodObject newFood = Instantiate(foodPrefab, CellToWorld(coord), Quaternion.identity, transform);
            data.ContainedObject = newFood;
        }
    }
}
