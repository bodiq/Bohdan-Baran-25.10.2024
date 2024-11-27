using Enums;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIResourceIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resourceCountText;
        [SerializeField] private Image resourceTypeImage;

        private int _resourceCount;
        private ResourceType _resourceType;
        
        public void Initialize(int count, Sprite sprite, ResourceType resourceType)
        {
            resourceCountText.text = count.ToString();
            resourceTypeImage.sprite = sprite;
            _resourceCount = count;
            _resourceType = resourceType;
        }

        public void ChangeResourceAmount(int countToAdd)
        {
            _resourceCount += countToAdd;
            resourceCountText.text = _resourceCount.ToString();
            GameManager.Instance.Player.PlayerResourceCount[_resourceType] = _resourceCount;
        }
    }
}
