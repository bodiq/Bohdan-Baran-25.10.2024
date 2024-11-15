using System.Collections;
using System.Collections.Generic;
using Enums;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

public class GenerationMap : MonoBehaviour
{
    [FormerlySerializedAs("tilePrefab")] [SerializeField] private MainTile mainTilePrefab;

    [SerializeField] private int columnsMap;
    [SerializeField] private int rowsMap;
    [SerializeField] private float tileSize;

    [SerializeField] private float maxHeight = 1.0f;
    [SerializeField] private float minHeight = 0.0f; 
    [SerializeField] private float maxHeightDifference = 0.5f;
    
    private static readonly float VerticalDistance = 0.75f;
    private static readonly float CoefficientPlacement = 0.866f;
    private static readonly float TileXOffsetOddRow = 0.25f;
    private static readonly float HalfUnit = 0.5f;
    
    private MainTile[,] _tiles;
    
    public MainTile[,] GenerateHexTiles()
    {
        _tiles = new MainTile[rowsMap, columnsMap];

        for (var i = 0; i < rowsMap; i++)
        {
            for (var j = 0; j < columnsMap; j++)
            {
                    
                var xOffset = j * tileSize * CoefficientPlacement; 
                var zOffset = i * tileSize * VerticalDistance;   
                    
                if (i % 2 == 1)
                {
                    xOffset += tileSize * HalfUnit - TileXOffsetOddRow;
                }
                    
                var height = Random.Range(minHeight, maxHeight);
                
                if (i == 0 && j == 0)
                {
                    height = 0;
                }
                else
                {
                    var averageNeighborHeight = GetAverageNeighborHeight(i, j);
                    if (Mathf.Abs(height - averageNeighborHeight) > maxHeightDifference)
                    {
                        height = averageNeighborHeight + Mathf.Sign(height - averageNeighborHeight) * maxHeightDifference;
                    }
                }

                var position = new Vector3(xOffset, height, zOffset);
                var tile = Instantiate(mainTilePrefab, position, Quaternion.identity, transform);
                _tiles[i, j] = tile;
            }
        }

        return _tiles;
    }
    
    private float GetAverageNeighborHeight(int i, int j)
    {
        float totalHeight = 0f;
        int count = 0;
        
        if (i > 0) { totalHeight += _tiles[i - 1, j]?.transform.position.y ?? minHeight; count++; }
        if (j > 0) { totalHeight += _tiles[i, j - 1]?.transform.position.y ?? minHeight; count++; }
        if (i > 0 && j > 0) { totalHeight += _tiles[i - 1, j - 1]?.transform.position.y ?? minHeight; count++; }
        if (i > 0 && j < columnsMap - 1) { totalHeight += _tiles[i - 1, j + 1]?.transform.position.y ?? minHeight; count++; }

        return count > 0 ? totalHeight / count : minHeight;
    }
    
    public void SetTileDependencies()
    {
        for (var i = 0; i < rowsMap; i++)
        {
            TileTypes tileType;

            if (i < 2)
            {
                tileType = TileTypes.Green;
            }
            else if (i < 4)
            {
                tileType = TileTypes.Grey;
            }
            else
            {
                tileType = TileTypes.Yellow;
            }
            
            for (var j = 0; j < columnsMap; j++)
            {
                var tile = _tiles[i, j];

                tile.neighbours[0] = GetNeighbour(i - 1, j - (i % 2 == 0 ? 1 : 0), j > 0 || i % 2 == 1); // Нижній лівий
                tile.neighbours[1] = GetNeighbour(i - 1, j + (i % 2 == 1 ? 1 : 0), j < columnsMap - 1 || i % 2 == 0); // Нижній правий
                tile.neighbours[2] = GetNeighbour(i, j - 1, j > 0); // Лівий
                tile.neighbours[3] = GetNeighbour(i, j + 1, j < columnsMap - 1); // Правий
                tile.neighbours[4] = GetNeighbour(i + 1, j - (i % 2 == 0 ? 1 : 0), j > 0 || i % 2 == 1); // Верхній лівий
                tile.neighbours[5] = GetNeighbour(i + 1, j + (i % 2 == 1 ? 1 : 0), j < columnsMap - 1 || i % 2 == 0); // Верхній правий
                    
                tile.ResourcesIndicatorManager.Initialize();
                tile.SetSubTilePreSetup(tileType);

                foreach (var tileAvailableIndicator in tile.AvailableIndicators)
                {
                    tileAvailableIndicator.SetIndicatorDependence(tile);
                }
            }
        }
    }
    
    private MainTile GetNeighbour(int i, int j, bool condition)
    {
        return condition && i >= 0 && i < rowsMap && j >= 0 && j < columnsMap ? _tiles[i, j] : null;
    }
    
}
