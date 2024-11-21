using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

namespace Supplies
{
    public class Stone : Resource
    {
        protected override void RespawnResource()
        {
            LastIndexTaken = 0;
            ResetAllPieces();
            
            RespawnTween?.Kill();
            
            RespawnTween = resourcePiecesGroupObject.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
            {
                resourceCollider.enabled = true;
            });
        }
    }
}