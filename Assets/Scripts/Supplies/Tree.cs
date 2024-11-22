
using DG.Tweening;

using UnityEngine;

namespace Supplies
{
    public class Tree : Resource
    {
        private Tween _windEffect;
        private Tween _rotateSpawnEffect;

        private float _randomXRotation;
        private float _randomZRotation;

        private float _randomRotationDuration;

        private Vector3 _endRotationVector;
        
        private void Start()
        {
            _randomXRotation = Random.Range(1f, 5f);
            _randomZRotation = Random.Range(1f, 5f);

            _randomRotationDuration = Random.Range(1.5f, 2.5f);

            _endRotationVector = new Vector3(_randomXRotation, 0f, _randomZRotation);
            StartWindEffect();
        }

        protected override void RespawnResource()
        {
            LastIndexTaken = 0;
            ResetAllPieces();
            
            transform.rotation = Quaternion.identity;
            
            RespawnTween?.Kill();
            _rotateSpawnEffect?.Kill();

            _rotateSpawnEffect = resourcePiecesGroupObject.transform.DORotate(new Vector3(0, 360, 0), 0.6f, RotateMode.FastBeyond360);
            
            RespawnTween = resourcePiecesGroupObject.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                resourceCollider.enabled = true;
                StartWindEffect();
            });
        }

        private void StartWindEffect()
        {
            _windEffect?.Kill();
            transform.rotation = Quaternion.identity;

            _windEffect = resourcePiecesGroupObject.transform.DORotate(_endRotationVector, _randomRotationDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}