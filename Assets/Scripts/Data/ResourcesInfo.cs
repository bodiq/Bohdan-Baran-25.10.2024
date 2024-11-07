using System;
using Enums;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ResourcesInfo
    {
        public ResourceType resourceType;
        public Sprite resourceSprite;
    }
}