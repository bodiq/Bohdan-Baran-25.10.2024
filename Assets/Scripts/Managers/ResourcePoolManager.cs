using System.Collections.Generic;
using Data;
using DefaultNamespace;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ResourcePoolManager : MonoBehaviour
    {
        public static ResourcePoolManager Instance { get; private set; }
    
        [SerializeField] private List<ResourcePool> resourcePools;
    
        private Dictionary<ResourceType, ResourcePool> _resourceDictionaryPool = new();

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
        
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var pool in resourcePools)
            {
                pool.PoolQueue = new Queue<ResourceItem>();

                for (int i = 0; i < pool.initializeSize; i++)
                {
                    var obj = Instantiate(pool.prefab, pool.parent);
                    obj.gameObject.SetActive(false);
                    pool.PoolQueue.Enqueue(obj);
                }

                _resourceDictionaryPool[pool.resourceType] = pool;
            }
        }

        public ResourceItem GetResource(ResourceType resourceType)
        {
            if (!_resourceDictionaryPool.ContainsKey(resourceType))
            {
                Debug.LogError("Resource type " + resourceType + " not found!");
                return null;
            }

            var pool = _resourceDictionaryPool[resourceType];

            if (pool.PoolQueue.Count > 0)
            {
                return pool.PoolQueue.Dequeue();
            }
            else
            {
                var obj = Instantiate(pool.prefab, pool.parent);
                return obj;
            }
        }

        public void ReturnResource(ResourceType resourceType, ResourceItem obj)
        {
            if (!_resourceDictionaryPool.ContainsKey(resourceType))
            {
                Debug.LogError("Resource type " + resourceType + " not found!");
                Destroy(obj);
                return;
            }
        
            obj.gameObject.SetActive(false);
            _resourceDictionaryPool[resourceType].PoolQueue.Enqueue(obj);
        }
    }
}
