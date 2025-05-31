using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    private Tilemap m_tilemap;

    public int width = 10;
    public int height = 10;
    public Tile[] groundTiles;
    public Tile[] WallTiles;
    public PlayerCharacterController Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_tilemap = GetComponentInChildren<Tilemap>();
        m_Grid = GetComponentInChildren<Grid>();
        m_BoardData = new CellData[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                // Check if the current position is on the border of the tilemap
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    // Set wall tiles on the borders
                    int tileNumber = Random.Range(0, WallTiles.Length);
                    tile = WallTiles[tileNumber];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    // Set ground tiles inside the borders
                    int tileNumber = Random.Range(0, groundTiles.Length);
                    tile = groundTiles[tileNumber];
                    m_BoardData[x, y].Passable = true;
                }
                m_tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        Player.Spawn(this, new Vector2Int(1, 1));
    }

    private CellData[,] m_BoardData;
    public class CellData
    {
        public bool Passable{ get; set; }
    }

   

    private Grid m_Grid;

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
}


