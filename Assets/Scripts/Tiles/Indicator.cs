using System;
using System.Collections;
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
            _myTile.IsTileUnlocked = true;
            _nextTileToOpen.IsTileReserved = true;
            _nextTileToOpen.ResourcesIndicatorManager.gameObject.SetActive(true);
            _nextTileToOpen.ResourcesIndicatorManager.MyTile = _nextTileToOpen;
            _nextTileToOpen.MyIndicator = this;
        }

        private void OnTriggerEnter(Collider other)
        {
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
                foreach (var activeResourceIndicator in _nextTileToOpen.ResourcesIndicatorManager.ActiveResourceIndicators)
                {
                    if (!activeResourceIndicator.Value.IsResourcesFull)
                    {
                        activeResourceIndicator.Value.IncreaseResourceCount(3);
                        var resource = ResourcePoolManager.Instance.GetResource(activeResourceIndicator.Key);
                        resource.gameObject.SetActive(true);
                        resource.TriggerResourceFly(GameManager.Instance.Player.transform.position, _nextTileToOpen.ResourcesEndPoint.position, activeResourceIndicator.Value, 3);
                    }
                }

                yield return null;
            }
            _isCollecting = false;
        }

    }
}
