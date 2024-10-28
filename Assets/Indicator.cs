using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private Tile nextTileToOpen;
    private Tile myTile;

    private void Start()
    {
        myTile = GetComponentInParent<Tile>();

        if (myTile != null)
        {
            nextTileToOpen = myTile.NearTiles[gameObject.transform.GetSiblingIndex()];
            if (nextTileToOpen == null)
            {
                gameObject.SetActive(false);
                myTile.MyIndicators.Remove(this);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
