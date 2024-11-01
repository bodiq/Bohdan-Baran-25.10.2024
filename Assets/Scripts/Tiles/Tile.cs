using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Indicator> indicators;
        [SerializeField] private ResourcesIndicatorManager resourcesIndicatorManager;
        [NonSerialized] public Tile[] neighbours = new Tile[6];
        
        private bool isUnlocked = false;
        private bool isReserved = false;

        public List<Indicator> AvailableIndicators => indicators;
        public ResourcesIndicatorManager ResourcesIndicatorManager => resourcesIndicatorManager;
        public bool IsTileUnlocked
        {
            get => isUnlocked;
            set => isUnlocked = value;
        }

        public bool IsTileReserved
        {
            get => isReserved;
            set => isReserved = value;
        }

        public void OpenTile()
        {
            isUnlocked = true;
            
            if (AvailableIndicators == null || AvailableIndicators.Count == 0)
            {
                Debug.LogWarning("No available indicators to activate.");
                return;
            }

            var indicator = GetRandomAvailableIndicator();

            if (indicator != null)
            {
                indicator.ActivateIndicator();
            }
            else
            {
                Debug.LogError("No indicators available on tile, null");
                TileManager.Instance.UnlockRandomOpenTileIndicator();
            }
        }

        private Indicator GetRandomAvailableIndicator()
        {
            var validIndicators = AvailableIndicators.Where(indicator => indicator.NextTileToOpen != null && !indicator.NextTileToOpen.IsTileUnlocked && !indicator.NextTileToOpen.IsTileReserved).ToList();

            return validIndicators.Count > 0 ? validIndicators[Random.Range(0, validIndicators.Count)] : null;
        }
    }
}
