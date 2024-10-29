using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Indicator> indicators;

        private bool isUnlocked = false;
        public Tile[] neighbours = new Tile[6];
        
        //public Tile[] NearTiles => neighbours;
        public List<Indicator> MyIndicators => indicators;

        private readonly Vector3 startTileYPos = new(0f, 0f, 0f);

        public void OpenTile()
        {
            gameObject.transform.position += startTileYPos;
        }
    }
}
