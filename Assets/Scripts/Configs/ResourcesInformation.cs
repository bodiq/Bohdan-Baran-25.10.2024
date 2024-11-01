using System;
using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ResourcesInformation", menuName = "Resources Information")]
    public class ResourcesInformation : ScriptableObject
    {
        [SerializeField] private List<ResourcesInfo> resourcesInfo;

        public Dictionary<ResourceType, Sprite> resourcesInformation = new ();

        private void OnEnable()
        {
            resourcesInformation.Clear();
        
            foreach (var info in resourcesInfo)
            {
                if (!resourcesInformation.ContainsKey(info.resourceType))
                {
                    resourcesInformation.Add(info.resourceType, info.resourceSprite);
                }
            }
        }
    }
}