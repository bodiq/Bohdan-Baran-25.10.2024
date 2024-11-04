using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Indicator> indicators;
        [SerializeField] private ResourcesIndicatorManager resourcesIndicatorManager;
        [SerializeField] private GameObject tilesObjects;
        [SerializeField] private Transform resourcesEndPoint;
        
        [HideInInspector] public Tile[] neighbours = new Tile[6];
        
        private bool _isUnlocked = false;
        private bool _isReserved = false;

        private Indicator _myIndicatorResp;

        public List<Indicator> AvailableIndicators => indicators;
        public ResourcesIndicatorManager ResourcesIndicatorManager => resourcesIndicatorManager;
        public GameObject TileObjects => tilesObjects;
        public Transform ResourcesEndPoint => resourcesEndPoint;
        public Indicator MyIndicator
        {
            get => _myIndicatorResp;
            set => _myIndicatorResp = value;
        }

        public bool IsTileUnlocked
        {
            get => _isUnlocked;
            set => _isUnlocked = value;
        }

        public bool IsTileReserved
        {
            get => _isReserved;
            set => _isReserved = value;
        }

        public void OpenTile()
        {
            ResourcesIndicatorManager.gameObject.SetActive(false);
            tilesObjects.SetActive(true);
            _isUnlocked = true;
            
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
