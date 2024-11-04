using System;
using System.Collections.Generic;
using Enums;
using Tiles;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ResourcePool
    {
        public ResourceType resourceType;
        public ResourceItem prefab;
        public int initializeSize;
        public Transform parent;
        [HideInInspector] public Queue<ResourceItem> PoolQueue;
    }
}