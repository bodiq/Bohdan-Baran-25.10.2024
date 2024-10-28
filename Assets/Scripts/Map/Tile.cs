using System;
using UnityEngine;

namespace Map
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Tile nextTileUnlock;
        
        [SerializeField] private Indicator indicator;
        [SerializeField] private Transform indicatorParent;

        private readonly Vector3 startTileYPos = new(0f, 3f, 0f);

        public void UnlockNextTile()
        {
            nextTileUnlock.transform.position -= startTileYPos;
            nextTileUnlock.gameObject.SetActive(true);
        }
    }
}
