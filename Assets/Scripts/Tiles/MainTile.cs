using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class MainTile : MonoBehaviour
    {
        [SerializeField] private List<Indicator> indicators;
        [SerializeField] private ResourcesIndicatorManager resourcesIndicatorManager;
        [SerializeField] private Transform resourcesEndPoint;
        [SerializeField] private SubTile[] tiles = new SubTile[3];
        
        [HideInInspector] public MainTile[] neighbours = new MainTile[6];

        private static readonly float TileAnimationOpenDuration = 0.2f;
        
        private bool _isUnlocked = false;
        private bool _isReserved = false;

        private Indicator _myIndicatorResp;
        private Tween _openTileTween;

        private SubTile _tileSelected;

        public List<Indicator> AvailableIndicators => indicators;
        public ResourcesIndicatorManager ResourcesIndicatorManager => resourcesIndicatorManager;
        public GameObject TileObjects => _tileSelected.gameObject;
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

            _openTileTween = _tileSelected.transform.DOScale(Vector3.one, TileAnimationOpenDuration).OnComplete(() =>
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
            _tileSelected.gameObject.SetActive(true);
        }

        public void ReserveTile()
        {
            _isReserved = true;
        }

        public void SetSubTilePreSetup(TileTypes tileType)
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                if (tileType == tiles[i].TileType)
                {
                    _tileSelected = tiles[i];
                    _tileSelected.transform.localScale = Vector3.zero;
                    return;
                }
            }
        }

        private Indicator GetRandomAvailableIndicator()
        {
            var validIndicators = AvailableIndicators.Where(indicator => indicator.NextMainTileToOpen != null && !indicator.NextMainTileToOpen.IsTileUnlocked && !indicator.NextMainTileToOpen.IsTileReserved).ToList();

            return validIndicators.Count > 0 ? validIndicators[Random.Range(0, validIndicators.Count)] : null;
        }

        private void OnDisable()
        {
            _openTileTween?.Kill();
        }
    }
}
