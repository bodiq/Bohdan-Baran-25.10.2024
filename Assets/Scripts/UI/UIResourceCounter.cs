using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceCountText;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private ResourceCounterAnimationSettings resourceCounterAnimationSettings;

    private Vector3 _initialPosition;
    private Vector3 _startAnimationPosition;

    private static readonly float SpawnPosYOffset = 13f;
    
    public void Initialize(Sprite sprite)
    {
        resourceIcon.sprite = sprite;
        _initialPosition = transform.localPosition;
        _startAnimationPosition = new Vector3(_initialPosition.x, _initialPosition.y - SpawnPosYOffset, _initialPosition.z);
        SetStartPos();
    }

    private void SetStartPos()
    {
        gameObject.SetActive(false);
        transform.localPosition = _startAnimationPosition;
        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;
    }

    public void StartAnimation(int count)
    {
        gameObject.SetActive(true);
        resourceCountText.text = "+" + count;
        transform.DOLocalMove(_initialPosition, resourceCounterAnimationSettings.DurationMoveAnimation).SetEase(Ease.OutQuint);
        transform.DOScale(Vector3.one, resourceCounterAnimationSettings.DurationScaleAnimation).SetEase(Ease.OutBack).OnComplete(() =>
        {
            canvasGroup.DOFade(0f, resourceCounterAnimationSettings.DurationFadeOutAnimation).OnComplete(SetStartPos).SetDelay(resourceCounterAnimationSettings.DelayBeforeDestroy);
        });
        canvasGroup.DOFade(1f, resourceCounterAnimationSettings.DurationFadeInAnimation);
    }
}
