using Managers;
using UnityEngine;

namespace Tiles
{
    public class Indicator : MonoBehaviour
    {
        private Tile nextTileToOpen;
        private Tile myTile;

        public Tile NextTileToOpen => nextTileToOpen;
    
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
            TileManager.Instance.UnlockTile(nextTileToOpen);
            //myTile.AvailableIndicators.Remove(this);
            gameObject.SetActive(false);
        }
    }
}
