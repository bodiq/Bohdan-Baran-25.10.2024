using System;
using System.Collections;
using System.Linq;
using Managers;
using UnityEngine;

namespace Tiles
{
    public class Indicator : MonoBehaviour
    {
        private Tile nextTileToOpen;
        private Tile myTile;

        public Tile NextTileToOpen => nextTileToOpen;

        private bool _isCollecting = false;

        private Coroutine _collectingCoroutine;
    
        public void SetIndicatorDependence(Tile tile)
        {
            myTile = tile;
        
            if (myTile != null)
            {
                nextTileToOpen = myTile.neighbours[gameObject.transform.GetSiblingIndex()];
                if (nextTileToOpen == null || nextTileToOpen.IsTileUnlocked)
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
            myTile.IsTileUnlocked = true;
            nextTileToOpen.IsTileReserved = true;
            nextTileToOpen.ResourcesIndicatorManager.gameObject.SetActive(true);
        }

        public bool CheckTileActive()
        {
            return nextTileToOpen.gameObject.activeSelf;
        }
        
        

        private void OnTriggerEnter(Collider other)
        {
            if (!_isCollecting)
            {
                _collectingCoroutine = StartCoroutine(CollectResource());
            }
            /*TileManager.Instance.UnlockTile(nextTileToOpen);
            gameObject.SetActive(false);*/
        }

        private void OnTriggerExit(Collider other)
        {
            _isCollecting = false;
            StopCoroutine(_collectingCoroutine);
            _collectingCoroutine = null;
        }

        private IEnumerator CollectResource()
        {
            _isCollecting = true;

            while (!nextTileToOpen.ResourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
            {
                var amountToIncresed = Mathf.CeilToInt(0.2f * Time.deltaTime);
                
                foreach (var activeResourceIndicator in nextTileToOpen.ResourcesIndicatorManager._activeResourceIndicators)
                {
                    activeResourceIndicator.Value.IncreaseResourceAmount(amountToIncresed);
                }

                yield return null;
            }

            _isCollecting = false;
        }
    }
}
