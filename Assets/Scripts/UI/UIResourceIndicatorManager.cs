using System.Collections.Generic;
using Configs;
using Enums;
using UnityEngine;

namespace UI
{
    public class UIResourceIndicatorManager : IUIScreen
    {
        [SerializeField] private UIResourceIndicator uiResourceIndicatorPrefab;
        [SerializeField] private ResourcesInformation resourcesInformation;

        private readonly Dictionary<ResourceType, UIResourceIndicator> UIResourceIndicators = new();

        public override void Initialize()
        {
            foreach (var resource in GameManager.Instance.Player.PlayerResourceCount)
            {
                if (resourcesInformation.resourcesInformation.TryGetValue(resource.Key, out var sprite))
                {
                    var indicator = Instantiate(uiResourceIndicatorPrefab, transform);
                    indicator.Initialize(resource.Value, sprite);
                    UIResourceIndicators.Add(resource.Key, indicator);
                }
            }
        }
    }
}