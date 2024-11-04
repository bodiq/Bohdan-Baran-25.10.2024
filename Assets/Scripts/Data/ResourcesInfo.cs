using System;
using Enums;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ResourcesInfo
    {
        public ResourceType ResourceType { get; }
        public Sprite ResourceSprite { get; }

        public ResourcesInfo(ResourceType resourceType, Sprite resourceSprite)
        {
            ResourceType = resourceType;
            ResourceSprite = resourceSprite;
        }
    }
}