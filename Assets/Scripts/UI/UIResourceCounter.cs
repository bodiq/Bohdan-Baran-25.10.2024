using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceCountText;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private CanvasGroup canvasGroup;

    private Vector3 _initialPosition;
    private Vector3 _startAnimationPosition;

    private static readonly float SpawnPosYOffset = 4f;
    
    public void Initialize(Sprite sprite)
    {
        resourceIcon.sprite = sprite;
        _initialPosition = transform.position;
        _startAnimationPosition = new Vector3(_initialPosition.x, _initialPosition.y - SpawnPosYOffset, _initialPosition.z);
        SetStartPos();
    }

    private void SetStartPos()
    {
        gameObject.SetActive(false);
        transform.position = _startAnimationPosition;
        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;
    }

    public void StartAnimation(int count)
    {
        gameObject.SetActive(true);
        resourceCountText.text = "+" + count;
        transform.DOMove(_initialPosition, 0.6f);
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            canvasGroup.DOFade(0f, 0.4f).OnComplete(SetStartPos);
        });
        canvasGroup.DOFade(1f, 0.4f);
    }
}
