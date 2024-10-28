using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private Tile tile;

    private void Start()
    {
        tile = GetComponentInParent<Tile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tile != null)
        {
            tile.UnlockNextTile();
            gameObject.SetActive(false);
        }
    }
}
