using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Puzzles
{
    public class GridConstruction : MonoBehaviour
    {
        public static List<Tile> GenerateGridAndMoveToPosition(int width, int height, Tile tilePrefab, Transform gridPosition)
        {
            List<Tile> tiles = new List<Tile>();

            float tileWidth = tilePrefab.GetWidth();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    var spawnedTile = Object.Instantiate(tilePrefab, new Vector3(x * tileWidth, y * tileWidth), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                    
                    bool isOffset = (x + y) % 2 == 0;
                    spawnedTile.Init(isOffset, 0);
                    tiles.Add(spawnedTile);
                }
            }

            float gridWidth = width * tileWidth;
            float gridHeight = height * tileWidth;
            Vector3 gridCenter = new Vector3(gridWidth / 2, gridHeight / 2, 0);

            for (int i = 0; i < tiles.Count; i++)
            {
                Transform child = tiles[i].transform;
                child.position -= gridCenter;
                child.SetParent(gridPosition);
                child.localPosition = child.position;
            }

            return tiles;
        }
    }
}