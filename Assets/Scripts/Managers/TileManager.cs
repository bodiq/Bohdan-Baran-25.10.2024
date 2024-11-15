using System.Collections.Generic;
using System.Linq;
using Configs;
using Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TileManager : MonoSingleton<TileManager>
    {
        [SerializeField] private GenerationMap generationMap;
        
        private MainTile[,] _tiles;

        private readonly List<MainTile> _openTiles = new ();

        protected override void Awake()
        {
            base.Awake();
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

        public void UnlockTile(MainTile mainTile)
        {
            if (mainTile.IsTileUnlocked)
            {
                return;
            }
            
            _openTiles.Add(mainTile);
            mainTile.gameObject.SetActive(true);
            if (mainTile.MyIndicator)
            {
                mainTile.MyIndicator.gameObject.SetActive(false);
            }
            mainTile.OpenTile();
            if (_openTiles.Count % 2 == 0 && _openTiles.Count > 4)
            {
                UnlockRandomOpenTileIndicator();
            }
            
        }

        public void UnlockRandomOpenTileIndicator()
        {
            var availableIndicators = _openTiles.SelectMany(tile => tile.AvailableIndicators
                .Where(indicator => indicator.NextMainTileToOpen != null 
                && !indicator.NextMainTileToOpen.IsTileReserved 
                && !indicator.NextMainTileToOpen.IsTileUnlocked)).ToList();

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
