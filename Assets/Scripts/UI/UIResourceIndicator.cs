using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIResourceIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resourceCount;
        [SerializeField] private Image resourceTypeImage;

        public void Initialize(int count, Sprite sprite)
        {
            resourceCount.text = count.ToString();
            resourceTypeImage.sprite = sprite;
        }

        public void ChangeResourceAmount(int newValue)
        {
            resourceCount.text = newValue.ToString();
        }
    }
}
