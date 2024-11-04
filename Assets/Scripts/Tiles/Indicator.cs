using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Tiles
{
    public class Indicator : MonoBehaviour
    {
        private Tile _nextTileToOpen;
        private Tile _myTile;

        public Tile NextTileToOpen => _nextTileToOpen;

        private bool _isCollecting = false;

        private Coroutine _collectingCoroutine;

        private static readonly float IntervalBetweenSpawnResources = 0.08f;
        private static readonly WaitForSeconds WaitBetweenSpawnResources = new (IntervalBetweenSpawnResources);

        private Dictionary<ResourcesIndicator, int> _resourcesIndicatorsToIncrease = new();
        
        public void SetIndicatorDependence(Tile tile)
        {
            _myTile = tile;
        
            if (_myTile != null)
            {
                _nextTileToOpen = _myTile.neighbours[gameObject.transform.GetSiblingIndex()];
                if (_nextTileToOpen == null || _nextTileToOpen.IsTileUnlocked)
                {
                    gameObject.SetActive(false);
                    //myTile.AvailableIndicators.Remove(this);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void ActivateIndicator()
        {
            var position = transform.position;
            position = new Vector3(position.x, 0f, position.z);
            transform.position = position;
            _nextTileToOpen.ReserveTile();
            _nextTileToOpen.ResourcesIndicatorManager.gameObject.SetActive(true);
            _nextTileToOpen.ResourcesIndicatorManager.MyTile = _nextTileToOpen;
            _nextTileToOpen.MyIndicator = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollecting)
                return; 
            
            _resourcesIndicatorsToIncrease.Clear();

            foreach (var indicator in _nextTileToOpen.ResourcesIndicatorManager.ActiveResourceIndicators)
            {
                if (GameManager.Instance.Player.PlayerResourceCount.TryGetValue(indicator.Key, out var value))
                {
                    var hasToBeEarned = indicator.Value.ResourceToEarn - indicator.Value.ResourceEarned;
                    if (hasToBeEarned > 0 && hasToBeEarned <= value)
                    {
                        var countToIncrease = hasToBeEarned <= 20 ? 1 : Mathf.Max(1, hasToBeEarned / 15);
                        indicator.Value.CountToIncrease = countToIncrease;
                        _resourcesIndicatorsToIncrease[indicator.Value] = countToIncrease;
                    }
                }
            }
            
            if (!_isCollecting)
            {
                _collectingCoroutine = StartCoroutine(CollectResource());
            }
        }


        private void OnTriggerExit(Collider other)
        {
            _isCollecting = false;
            if (_collectingCoroutine != null)
            {
                StopCoroutine(_collectingCoroutine);
                _collectingCoroutine = null;
            }
        }

        private IEnumerator CollectResource()
        {
            _isCollecting = true;

            while (!_nextTileToOpen.ResourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
            {
                foreach (var resourcesIndicator in _resourcesIndicatorsToIncrease)
                {
                    var resourceIndicator = resourcesIndicator;
                    if (!resourceIndicator.Key.IsResourcesFull)
                    {
                        resourceIndicator.Key.IncreaseResourceCount();
                        var resource = ResourcePoolManager.Instance.GetResource(resourceIndicator.Key.ResourceType);
                        resource.gameObject.SetActive(true);
                        resource.TriggerResourceFly(GameManager.Instance.Player.transform.position, _nextTileToOpen.ResourcesEndPoint.position, resourceIndicator.Key, resourcesIndicator.Value);
                    }
                }

                yield return WaitBetweenSpawnResources;
            }
            _isCollecting = false;
        }

    }
}
