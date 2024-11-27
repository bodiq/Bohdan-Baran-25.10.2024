using Enums;
using UnityEngine;

namespace Tiles
{
    public class SubTile : MonoBehaviour
    {
        [SerializeField] private TileTypes tileType;
        [SerializeField] private GameObject[] resourcesObjects;

        public TileTypes TileType => tileType;

        public void ResourcedObjectsPreSetup(bool isRotated = false)
        {
            var randomIndex = Random.Range(0, resourcesObjects.Length);
            
            if (isRotated)
            {
                resourcesObjects[randomIndex].transform.Rotate(0f, 180f, 0f);
            }
            
            resourcesObjects[randomIndex].SetActive(true);
        }
    }
}
