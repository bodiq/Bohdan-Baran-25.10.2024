using System;
using System.Linq;
using Enums;
using ScriptableObjects;
using UnityEngine;

public class UIResourceCounterManager : MonoBehaviour
{
    [SerializeField] private UIResourceCounter[] resourceCounters;
    [SerializeField] private ResourcesInformation resourcesInformation;
    [SerializeField] private Canvas canvas;
    
    private Camera _mainCamera;

    private void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void Initialize(ResourceType resourceType)
    {
        if(resourcesInformation.ResourcesVisualInformation.TryGetValue(resourceType, out var sprite))
        {
            foreach (var counter in resourceCounters)
            {
                counter.Initialize(sprite);
            }
        }
        _mainCamera = Camera.main;
        canvas.worldCamera = _mainCamera;
    }

    public void ShowAvailableResourceCounter(int count)
    {
        var activeCounter = resourceCounters.FirstOrDefault(counter => !counter.gameObject.activeSelf);
        if (activeCounter)
        {
            activeCounter.StartAnimation(count);
        }
        else
        {
            Debug.LogWarning("No Available Counters");
        }
    }
}
