using System.Collections.Generic;
using System.Linq;
using Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance { get; private set; }

        [SerializeField] private Tile tilePrefab;
        [SerializeField] private int columnsMap;
        [SerializeField] private int rowsMap;
        [SerializeField] private float tileSize;
        
        private Tile[,] _tiles;

        private static readonly float VerticalDistance = 0.75f;
        private static readonly float CoefficientPlacement = 0.866f;
        private static readonly float TileXOffsetOddRow = 0.25f;
        private static readonly float HalfUnit = 0.5f;

        private readonly List<Tile> _openTiles = new ();

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
            
            GenerateHexTiles();
        }

        private void Start()
        {
            SetTileDependencies();
            UnlockTile(_tiles[0, 0]);
        }

        private void GenerateHexTiles()
        {
            _tiles = new Tile[rowsMap, columnsMap];

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

                    var position = new Vector3(xOffset, 0, zOffset);
                    var tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    _tiles[i, j] = tile;
                }
            }
        }

        private void SetTileDependencies()
        {
            for (var i = 0; i < rowsMap; i++)
            {
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
                    tile.TileObjects.SetActive(false);

                    foreach (var tileAvailableIndicator in tile.AvailableIndicators)
                    {
                        tileAvailableIndicator.SetIndicatorDependence(tile);
                    }
                }
            }
        }

        private Tile GetNeighbour(int i, int j, bool condition)
        {
            return condition && i >= 0 && i < rowsMap && j >= 0 && j < columnsMap ? _tiles[i, j] : null;
        }

        public void UnlockTile(Tile tile)
        {
            if (tile.IsTileUnlocked)
            {
                return;
            }
            
            _openTiles.Add(tile);
            tile.gameObject.SetActive(true);
            if (tile.MyIndicator)
            {
                tile.MyIndicator.gameObject.SetActive(false);
            }
            tile.OpenTile();
            if (_openTiles.Count % 2 == 0 && _openTiles.Count > 4)
            {
                UnlockRandomOpenTileIndicator();
            }
            
        }

        public void UnlockRandomOpenTileIndicator()
        {
            var availableIndicators = _openTiles.SelectMany(tile => tile.AvailableIndicators
                .Where(indicator => indicator.NextTileToOpen != null 
                && !indicator.NextTileToOpen.IsTileReserved 
                && !indicator.NextTileToOpen.IsTileUnlocked)).ToList();

            if (availableIndicators.Count > 0)
            {
                var indicator = availableIndicators[Random.Range(0, availableIndicators.Count)];
                indicator.ActivateIndicator();
            }
            else
            {
                Debug.LogError("All tiles are opened");
            }
        }
    }
}
