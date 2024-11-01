using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Enums;

public class ResourcePoolManager : MonoBehaviour
{
    [SerializeField] private List<ResourcePool> resourcePools;
    
    private Dictionary<ResourceType, ResourcePool> _resourceDictionaryPool = new();

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var pool in resourcePools)
        {
            pool.PoolQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.initializeSize; i++)
            {
                var obj = Instantiate(pool.prefab, pool.parent);
                obj.SetActive(false);
                pool.PoolQueue.Enqueue(obj);
            }

            _resourceDictionaryPool[pool.resourceType] = pool;
        }
    }

    public GameObject GetResource(ResourceType resourceType)
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

    public void ReturnResource(ResourceType resourceType, GameObject obj)
    {
        if (!_resourceDictionaryPool.ContainsKey(resourceType))
        {
            Debug.LogError("Resource type " + resourceType + " not found!");
            Destroy(obj);
            return;
        }
        
        obj.SetActive(false);
        _resourceDictionaryPool[resourceType].PoolQueue.Enqueue(obj);
    }
}
