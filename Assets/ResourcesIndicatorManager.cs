using System.Collections;
using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;

public class ResourcesIndicatorManager : MonoBehaviour
{
    [SerializeField] private List<ResourcesIndicator> resourcesIndicators;

    public void Initialize(ResourceData[] resourceData)
    {
        for (var i = 0; i < resourceData.Length; i++)
        {
            resourcesIndicators[i].gameObject.SetActive(true);
            resourcesIndicators[i].Initialize(resourceData[i].CountToEarn, resourceData[i].ResourceType);
        }
    }
}
