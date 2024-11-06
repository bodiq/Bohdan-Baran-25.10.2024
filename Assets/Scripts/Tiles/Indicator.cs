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
            gameObject.SetActive(true);
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

                    hasToBeEarned = Mathf.Min(hasToBeEarned, value);
                    
                    Debug.LogError(hasToBeEarned);
                    
                    if (hasToBeEarned > 0)
                    {
                        var countToIncrease = 0;
                        if (hasToBeEarned <= 20)
                        {
                            countToIncrease = 1;
                        }
                        else
                        {
                            countToIncrease = hasToBeEarned / 20;
                            var remainder = hasToBeEarned % 20;
                            indicator.Value.RemainderCount = remainder;
                            indicator.Value.RemainderText = remainder;
                        }
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
                    if (GameManager.Instance.Player.PlayerResourceCount[resourcesIndicator.Key.ResourceType] <= 0)
                    {
                        continue;
                    }
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
