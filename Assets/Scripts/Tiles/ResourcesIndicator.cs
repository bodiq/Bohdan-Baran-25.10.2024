using Configs;
using Enums;
using Managers;
using ScriptableObjects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tiles
{
    public class ResourcesIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resourcesEarnedText;
        [SerializeField] private TextMeshProUGUI resourcesToGoText;
        [SerializeField] private Image resourceImage;
        [SerializeField] private ResourcesInformation resourcesInformation;

        private UIResourceIndicator _uiResourceIndicator;
        
        private int _resourcesToEarn;
        private int _resourcesEarned = 0;
        private int _resourceEarnedText = 0;
        
        private int _remainderCount = 0;
        private int _countToIncrease;
        
        private ResourceType _resourceType;
    
        private bool _isIndicatorFull = false;
        private bool _isResourcesFull = false;

        private ResourcesIndicatorManager _resourcesIndicatorManager;

        public bool IsIndicatorFull => _isIndicatorFull;
        public bool IsResourcesFull => _isResourcesFull;
        public ResourceType ResourceType => _resourceType;

        public int ResourceEarned => _resourcesEarned;
        public int ResourceToEarn => _resourcesToEarn;

        public int CountToIncrease
        {
            private get => _countToIncrease;
            set => _countToIncrease = value;
        }

        public int RemainderCount
        {
            private get => _countToIncrease;
            set => _remainderCount = value;
        }

        private void Start()
        {
            _resourcesIndicatorManager = GetComponentInParent<ResourcesIndicatorManager>();
            
            if (UIManager.Instance.UIResourceIndicatorManager.UIResourceIndicators.TryGetValue(_resourceType, out var indicator))
            {
                _uiResourceIndicator = indicator;
            }
        }

        public void Initialize(int countToEarn, ResourceType resourceType)
        {
            _resourcesToEarn = countToEarn;
            _resourceType = resourceType;
            resourcesToGoText.text = _resourcesToEarn.ToString();
            if (resourcesInformation.ResourcesVisualInformation.TryGetValue(_resourceType, out var sprite))
            {
                resourceImage.sprite = sprite;
            }
        }

        public void IncreaseResourceCount()
        {
            if (!_isResourcesFull)
            {
                if (_remainderCount != 0)
                {
                    _resourcesEarned += _remainderCount;
                    _uiResourceIndicator.ChangeResourceAmount(_remainderCount);
                    if (_resourcesEarned >= _resourcesToEarn)
                    {
                        _isResourcesFull = true;
                    }
                    _remainderCount = 0;
                }
                else
                {
                    _resourcesEarned += _countToIncrease;
                    _uiResourceIndicator.ChangeResourceAmount(_countToIncrease);
                    if (_resourcesEarned >= _resourcesToEarn)
                    {
                        _isResourcesFull = true;
                    }
                }
            }
        }

        public void IncreaseResourceTextAmount(int count, int remainder = 0)
        {
            if (!_isIndicatorFull)
            {
                if (remainder != 0)
                {
                    _resourceEarnedText += remainder;
                    resourcesEarnedText.text = _resourceEarnedText.ToString();
                    if (_resourceEarnedText >= _resourcesToEarn)
                    {
                        _isIndicatorFull = true;

                        if (_resourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
                        {
                            TileManager.Instance.UnlockTile(_resourcesIndicatorManager.MyMainTile); 
                        }
                    }
                }
                else
                {
                    _resourceEarnedText += count;
                    resourcesEarnedText.text = _resourceEarnedText.ToString();
                    if (_resourceEarnedText >= _resourcesToEarn)
                    {
                        _isIndicatorFull = true;

                        if (_resourcesIndicatorManager.CheckIfResourceIndicatorsAreFull())
                        {
                            TileManager.Instance.UnlockTile(_resourcesIndicatorManager.MyMainTile); 
                        }
                    }
                }
            }
        }
    }
}
