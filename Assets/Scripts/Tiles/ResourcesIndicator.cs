using Configs;
using Enums;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tiles
{
    public class ResourcesIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resourcesEarnedText;
        [SerializeField] private TextMeshProUGUI resourcesToGoText;
        [SerializeField] private Image resourceImage;
        [SerializeField] private ResourcesInformation resourcesInformation;

        private int _resourcesToEarn;
        private int _resourcesEarned = 0;
        private int _resourceEarnedText = 0;
        private ResourceType _resourceType;
    
        private bool _isIndicatorFull = false;
        private bool _isResourcesFull = false;

        private ResourcesIndicatorManager _resourcesIndicatorManager;

        private int _countToIncrease;

        public bool IsIndicatorFull => _isIndicatorFull;
        public bool IsResourcesFull => _isResourcesFull;
        public ResourceType ResourceType => _resourceType;

        public int ResourceEarned => _resourcesEarned;
        public int ResourceToEarn => _resourcesToEarn;

        public int CountToIncrease
        {
            get => _countToIncrease;
            set => _countToIncrease = value;
        }

        private void Start()
        {
            _resourcesIndicatorManager = GetComponentInParent<ResourcesIndicatorManager>();
        }

        public void Initialize(int countToEarn, ResourceType resourceType)
        {
            _resourcesToEarn = countToEarn;
            _resourceType = resourceType;
            resourcesToGoText.text = _resourcesToEarn.ToString();
            if (resourcesInformation.resourcesInformation.TryGetValue(_resourceType, out var sprite))
            {
                resourceImage.sprite = sprite;
            }
        }

        public void IncreaseResourceCount()
        {
            if (!_isResourcesFull)
            {
                _resourcesEarned += _countToIncrease;
                if (_resourcesEarned >= _resourcesToEarn)
                {
                    _isResourcesFull = true;
                }
            }
        }

        public void IncreaseResourceTextAmount()
        {
            if (!_isIndicatorFull)
            {
                _resourceEarnedText += _countToIncrease;
                resourcesEarnedText.text = _resourceEarnedText.ToString();
                if (_resourceEarnedText >= _resourcesToEarn)
                {
                    _isIndicatorFull = true;

                    if (_resourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
                    {
                        TileManager.Instance.UnlockTile(_resourcesIndicatorManager.MyTile); 
                    }
                }
            }
        }
    }
}
