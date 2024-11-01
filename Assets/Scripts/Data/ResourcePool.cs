using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ResourcePool
    {
        public ResourceType resourceType;
        public GameObject prefab;
        public int initializeSize;
        public Transform parent;
        [HideInInspector] public Queue<GameObject> PoolQueue;
    }
}