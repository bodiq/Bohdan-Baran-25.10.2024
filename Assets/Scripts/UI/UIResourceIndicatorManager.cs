﻿using System.Collections.Generic;
using Enums;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class UIResourceIndicatorManager : UIScreen
    {
        [SerializeField] private UIResourceIndicator uiResourceIndicatorPrefab;
        [SerializeField] private ResourcesInformation resourcesInformation;

        public readonly Dictionary<ResourceType, UIResourceIndicator> UIResourceIndicators = new();

        public override void Initialize()
        {
            foreach (var resource in GameManager.Instance.Player.PlayerResourceCount)
            {
                if (resourcesInformation.ResourcesVisualInformation.TryGetValue(resource.Key, out var sprite))
                {
                    var indicator = Instantiate(uiResourceIndicatorPrefab, transform);
                    indicator.Initialize(resource.Value, sprite, resource.Key);
                    UIResourceIndicators.Add(resource.Key, indicator);
                }
            }
        }

        public void ChangeResourceIndicatorAmount(ResourceType resourceType, int addCount)
        {
            UIResourceIndicators[resourceType].ChangeResourceAmount(addCount);
        }
    }
}