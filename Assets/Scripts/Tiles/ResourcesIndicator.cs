using Configs;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourcesEarnedText;
    [SerializeField] private TextMeshProUGUI resourcesToGoText;
    [SerializeField] private Image resourceImage;
    [SerializeField] private ResourcesInformation resourcesInformation;

    private int _resourcesToEarn;
    private int _resourcesEarned = 0;
    private ResourceType _resourceType;
    private bool isFull = false;

    public bool IsFull => isFull;
    public ResourceType ResourceType => _resourceType; 

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

    public void IncreaseResourceAmount()
    {
        if (!isFull)
        {
            _resourcesEarned += 1;
            resourcesEarnedText.text = _resourcesEarned.ToString();
            if (_resourcesEarned == _resourcesToEarn)
            {
                isFull = true;
            }
        }
    }
}
