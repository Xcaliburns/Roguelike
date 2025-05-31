using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
     private Tilemap m_tilemap;

    public int width = 10;
    public int height = 10;
    public Tile[] groundTiles;
    public Tile[] WallTiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_tilemap = GetComponentInChildren<Tilemap>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile tile;
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    // Set wall tiles on the borders
                    int tileNumber = Random.Range(0, WallTiles.Length);
                    tile = WallTiles[tileNumber];
                }
                else
                {
                    // Set ground tiles inside the borders
                    int tileNumber = Random.Range(0, groundTiles.Length);
                    tile = groundTiles[tileNumber];
                }
               m_tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }

        }
    }
}


