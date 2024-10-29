using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Map;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private Tile nextTileToOpen;
    private Tile myTile;

    public Tile NextTileToOpen => nextTileToOpen;

    private void Start()
    {
        myTile = GetComponentInParent<Tile>();

        if (myTile != null)
        {
            nextTileToOpen = myTile.neighbours[gameObject.transform.GetSiblingIndex()];
            if (nextTileToOpen == null || nextTileToOpen.IsTileUnlocked)
            {
                gameObject.SetActive(false);
                myTile.AvailableIndicators.Remove(this);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool CheckTileActive()
    {
        return nextTileToOpen.gameObject.activeSelf;
    }

    private void OnTriggerEnter(Collider other)
    {
        TileManager.Instance.UnlockTile(nextTileToOpen);
        gameObject.SetActive(false);
        myTile.AvailableIndicators.Remove(this);
    }
}
