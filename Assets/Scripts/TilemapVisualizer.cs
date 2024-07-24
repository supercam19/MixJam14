using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;

    [SerializeField]
    private RuleTile wallRule;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, wallRule);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    public void PaintWalls(IEnumerable<Vector2Int> wallPositions)
    {
        PaintTiles(wallPositions, floorTilemap, wallRule);
    }

    public void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintMultipleTiles(Tilemap tilemap, TileBase tile, Vector2Int[] positions) {
        foreach (Vector2Int pos in positions) {
            Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)pos);
            tilemap.SetTile(tilePosition, tile);
        }
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }

    public TileBase GetTile(Vector2Int position)
    {
        return floorTilemap.GetTile((Vector3Int)position);
    }
}
