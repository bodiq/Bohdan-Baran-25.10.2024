using System.Collections.Generic;
using System.Linq;
using Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GenerationMap generationMap;
        public static TileManager Instance { get; private set; }
        
        private Tile[,] _tiles;

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
            
            _tiles = generationMap.GenerateHexTiles();
        }

        private void Start()
        {
            generationMap.SetTileDependencies();
            UnlockFirstTile();
        }

        private void UnlockFirstTile()
        {
            var firstTile = _tiles[0, 0];
            firstTile.TileObjects.transform.localScale = Vector3.one;
            UnlockTile(firstTile);
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
                Debug.LogWarning("All tiles are opened");
            }
        }
    }
}
