using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Tile> tilesNear;
        [SerializeField] private List<Indicator> indicators;

        private bool isUnlocked = false;

        public List<Tile> NearTiles => tilesNear;
        public List<Indicator> MyIndicators => indicators;

        private readonly Vector3 startTileYPos = new(0f, 0f, 0f);
        
        public void OpenTile()
        {
            gameObject.transform.position += startTileYPos;
        }
    }
}
