using System.Collections.Generic;
using Configs;
using Enums;
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
                if (resourcesInformation.resourcesInformation.TryGetValue(resource.Key, out var sprite))
                {
                    var indicator = Instantiate(uiResourceIndicatorPrefab, transform);
                    indicator.Initialize(resource.Value, sprite, resource.Key);
                    UIResourceIndicators.Add(resource.Key, indicator);
                }
            }
        }
    }
}