using Enums;
using UnityEngine;

namespace Tiles
{
    public class SubTile : MonoBehaviour
    {
        [SerializeField] private TileTypes tileType;
        [SerializeField] private GameObject resourcesObjects;

        public TileTypes TileType => tileType;

        public void TurnResourcesObjects(bool isActive)
        {
            resourcesObjects.SetActive(isActive);
        }
    }
}
