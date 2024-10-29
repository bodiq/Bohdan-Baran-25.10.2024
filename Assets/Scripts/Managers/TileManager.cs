using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

namespace Managers
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance { get; private set; }

        private List<Tile> tiles;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            tiles = GetComponentsInChildren<Tile>().ToList();
        }

        public void UnlockTile(Tile tile)
        {
            if (tile.gameObject.activeSelf)
            {
                return;
            }
            else
            {
                tile.gameObject.SetActive(true);
                tile.OpenTile();
            }
        }
    }
}
