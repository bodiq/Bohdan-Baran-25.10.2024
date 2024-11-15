using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class SubTile : MonoBehaviour
{
    [SerializeField] private TileTypes tileType;


    public TileTypes TileType => tileType;
}
