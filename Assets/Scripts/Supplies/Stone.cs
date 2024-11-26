using DG.Tweening;

namespace Supplies
{
    public class Stone : Resource
    {
        protected override void RespawnResource()
        {
            LastIndexTaken = 0;
            ResetAllPieces();
            
            RespawnTween?.Kill();
            
            RespawnTween = resourcePiecesGroupObject.transform.DOScale(InitialScaleValue, respawnScaleInDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                resourceCollider.enabled = true;
            });
        }

        protected override void PlayGatheredAnimation()
        {
            GatherTween = resourcePiecesGroupObject.transform.DOShakePosition(gatheredAnimationDuration, shakeAnimationPower);
        }
    }
}