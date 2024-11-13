using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourcesInformation", menuName = "Resources Information")]
    public class ResourcesInformation : ScriptableObject
    {
        [SerializeField] private List<ResourcesInfo> resourcesInfo;

        public Dictionary<ResourceType, Sprite> resourcesInformation = new ();

        private void OnEnable()
        {
            resourcesInformation.Clear();

            foreach (var info in resourcesInfo.Where(info => !resourcesInformation.ContainsKey(info.resourceType)))
            {
                resourcesInformation.Add(info.resourceType, info.resourceSprite);
            }
        }
    }
}