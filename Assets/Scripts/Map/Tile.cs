using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Tile> tilesNear;
        [SerializeField] private List<Indicator> indicators;

        public List<Tile> NearTiles => tilesNear;
        public List<Indicator> MyIndicators => indicators;

        private readonly Vector3 startTileYPos = new(0f, 3f, 0f);
        
        public void UnlockNextTile(int index)
        {
            tilesNear[index].transform.position += startTileYPos;
        }
    }
}
