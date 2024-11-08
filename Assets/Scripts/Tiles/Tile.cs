using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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

        private static readonly float TileAnimationOpenDuration = 0.2f;
        
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

        public bool IsTileUnlocked => _isUnlocked;
        public bool IsTileReserved => _isReserved;
        

        public void OpenTile()
        {
            ResourcesIndicatorManager.gameObject.SetActive(false);
            UnlockTile();

            tilesObjects.transform.DOScale(Vector3.one, TileAnimationOpenDuration).OnComplete(() =>
            {
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
                    Debug.LogWarning("No indicators available on tile, null");
                    TileManager.Instance.UnlockRandomOpenTileIndicator();
                }
            });
        }
        
        public void UnlockTile()
        {
            _isUnlocked = true;
            tilesObjects.SetActive(true);
        }

        public void ReserveTile()
        {
            _isReserved = true;
        }

        private Indicator GetRandomAvailableIndicator()
        {
            var validIndicators = AvailableIndicators.Where(indicator => indicator.NextTileToOpen != null && !indicator.NextTileToOpen.IsTileUnlocked && !indicator.NextTileToOpen.IsTileReserved).ToList();

            return validIndicators.Count > 0 ? validIndicators[Random.Range(0, validIndicators.Count)] : null;
        }
    }
}