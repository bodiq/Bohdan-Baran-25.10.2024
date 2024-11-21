using DG.Tweening;
using Managers;
using UnityEngine;

namespace Supplies
{
    public class Tree : Resource
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