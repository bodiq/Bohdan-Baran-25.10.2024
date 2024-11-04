using System;
using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;
using Constants;
using Tiles;

namespace Managers
{
    public class ResourcesIndicatorManager : MonoBehaviour
    {
        [SerializeField] private List<ResourcesIndicator> resourcesIndicators;
        [SerializeField] private Canvas canvas;

        private ResourceData[] _resourceData;
        private Camera _mainCamera;

        public Dictionary<ResourceType, ResourcesIndicator> _activeResourceIndicators = new();

        private Tile _myTile;
        
        public Tile MyTile
        {
            get => _myTile;
            set => _myTile = value;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            canvas.worldCamera = _mainCamera;
        }

        private void LateUpdate()
        {
            if (_mainCamera != null)
            {
                transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
            }
        }

        private void SetRandomResourceCondition()
        {
            var arrayDataLenght = Random.Range(ResourceConstants.MinResourcesArrayLenght, ResourceConstants.MaxResourcesArrayLenght);
            
            _resourceData = new ResourceData[arrayDataLenght];
            
            var listAvailableEnums = new List<ResourceType>(ResourceConstants.ListResourceTypes);

            for (var i = 0; i < arrayDataLenght; i++)
            {
                if (listAvailableEnums.Count == 0) 
                {
                    Debug.LogWarning("Not enough free ResourceTypes.");
                    break;
                }
                
                var randomResource = GetRandomResourceType(listAvailableEnums);
                var randomCountToEarn = Random.Range(ResourceConstants.MinResourcesCount, ResourceConstants.MaxResourcesCount);
                listAvailableEnums.Remove(randomResource);
                _resourceData[i].ResourceType = randomResource;
                _resourceData[i].CountToEarn = randomCountToEarn;
            }
        }

        public bool CheckIfResourceIndicatorsAreFull()
        {
            foreach (var indicator in _activeResourceIndicators)
            {
                if (!indicator.Value.IsIndicatorFull)
                {
                    return false;
                }
            }

            return true;
        }

        private ResourceType GetRandomResourceType(List<ResourceType> resourceTypes)
        {
            var randomIndex = Random.Range(0, resourceTypes.Count);
            return resourceTypes[randomIndex];
        }

        public void Initialize()
        {
            SetRandomResourceCondition();
            
            for (var i = 0; i < _resourceData.Length; i++)
            {
                resourcesIndicators[i].gameObject.SetActive(true);
                resourcesIndicators[i].Initialize(_resourceData[i].CountToEarn, _resourceData[i].ResourceType);
                _activeResourceIndicators.Add(_resourceData[i].ResourceType, resourcesIndicators[i]);
            }
            
            gameObject.SetActive(false);
        }
    }
}
