using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourcesInformation", menuName = "ScriptableObjects/Resources Information")]
    public class ResourcesInformation : ScriptableObject
    {
        [SerializeField] private List<ResourcesInfo> resourcesInfo;
        
        [SerializeField] private int minResourcesCount = 10;
        [SerializeField] private int maxResourcesCount = 400;

        public const int MinResourcesArrayLength = 1;
        public const int MaxResourcesArrayLength = 3;

        private readonly Dictionary<ResourceType, Sprite> _resourcesVisualInformation = new ();
        [NonSerialized] private List<ResourceType> _listResourceTypes;

        public int MinResourcesCount => minResourcesCount;
        public int MaxResourcesCount => maxResourcesCount;
        public IReadOnlyDictionary<ResourceType, Sprite> ResourcesVisualInformation => _resourcesVisualInformation;
        public IReadOnlyList<ResourceType> ListResourceTypes => _listResourceTypes;

        private void OnEnable()
        {
            InitializeResourcesVisualInformation();
            InitializeListResourceTypes();
        }

        private void InitializeResourcesVisualInformation()
        {
            _resourcesVisualInformation.Clear();

            foreach (var info in resourcesInfo)
            {
                if (!_resourcesVisualInformation.ContainsKey(info.resourceType))
                {
                    _resourcesVisualInformation.Add(info.resourceType, info.resourceSprite);
                }
                else
                {
                    Debug.LogWarning($"Duplicate resource type detected: {info.resourceType}");
                }
            }
        }

        private void InitializeListResourceTypes()
        {
            _listResourceTypes = Configs.ConfigHelper.GetEnumValues<ResourceType>();
        }
    }
}