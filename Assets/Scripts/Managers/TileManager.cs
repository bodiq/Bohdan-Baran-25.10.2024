using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

namespace Managers
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance { get; private set; }

        [SerializeField] private Tile tilePrefab;
        [SerializeField] private int columnsMap;
        [SerializeField] private int rowsMap;
        [SerializeField] private float tileSize;
        
        private Tile[,] tiles;

        private const float VerticalDistance = 0.75f;
        private const float CoefficientPlacement = 0.866f;
        private const float TileXOffsetOddRow = 0.25f;
        private const float HalfUnit = 0.5f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            GenerateHexTiles();
        }

        private void GenerateHexTiles()
        {
            tiles = new Tile[rowsMap, columnsMap];

            for (var i = 0; i < rowsMap; i++)
            {
                for (var j = 0; j < columnsMap; j++)
                {
                    
                    var xOffset = j * tileSize * VerticalDistance; 
                    var zOffset = i * tileSize * CoefficientPlacement;   
                    
                    if (i % 2 == 1)
                    {
                        xOffset += tileSize * HalfUnit - TileXOffsetOddRow;
                    }

                    var position = new Vector3(xOffset, 0, zOffset);
                    var tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    tiles[i, j] = tile;
                }
            }
        }

        public void UnlockTile(Tile tile)
        {
            if (tile.gameObject.activeSelf)
            {
                return;
            }
            else
            {
                tile.gameObject.SetActive(true);
                tile.OpenTile();
            }
        }
    }
}
