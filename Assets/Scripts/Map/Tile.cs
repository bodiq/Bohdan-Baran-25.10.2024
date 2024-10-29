using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<Indicator> indicators;
        [NonSerialized] public Tile[] neighbours = new Tile[6];
        
        private bool isUnlocked = false;
        public List<Indicator> AvailableIndicators => indicators;
        public bool IsTileUnlocked => isUnlocked;

        private readonly Vector3 startTileYPos = new(0f, 0f, 0f);

        public void OpenTile()
        {
            isUnlocked = true;
            
            if (AvailableIndicators == null || AvailableIndicators.Count == 0)
            {
                Debug.LogWarning("No available indicators to activate.");
                return;
            }

            var indicator = GetFirstAvailableIndicator();
            var position = indicator.transform.position;
            position = new Vector3(position.x, 0f, position.z);
            indicator.transform.position = position;
        }

        private Indicator GetFirstAvailableIndicator()
        {
            return AvailableIndicators.FirstOrDefault(indicator => indicator.NextTileToOpen != null && !indicator.NextTileToOpen.IsTileUnlocked);
        }
    }
}
