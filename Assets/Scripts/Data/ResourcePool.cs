using System;
using System.Collections.Generic;
using DefaultNamespace;
using Enums;
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