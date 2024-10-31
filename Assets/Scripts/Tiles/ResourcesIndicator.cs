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
    private int _resourcesEarned;
    private ResourceType _resourceType;

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
}
