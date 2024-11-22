
using DG.Tweening;

using UnityEngine;

namespace Supplies
{
    public class Tree : Resource
    {
        [SerializeField] private float durationRotationSpawn = 0.6f;
        
        private Tween _windTweenEffect;
        private Tween _rotateTweenSpawnEffect;

        private float _randomXRotation;
        private float _randomZRotation;

        private float _randomRotationDuration;

        private Vector3 _endRotationVector;

        private static readonly Vector3 EndValueRotatingOnRespawn = new (0, 360, 0);
        
        private void Start()
        {
            _randomXRotation = Random.Range(2f, 5f);
            _randomZRotation = Random.Range(2f, 5f);

            _randomRotationDuration = Random.Range(1.25f, 2.5f);

            _endRotationVector = new Vector3(_randomXRotation, 0f, _randomZRotation);
            StartWindEffect();
        }

        protected override void RespawnResource()
        {
            LastIndexTaken = 0;
            ResetAllPieces();
            
            resourcePiecesGroupObject.transform.rotation = Quaternion.identity;
            
            RespawnTween?.Kill();
            _rotateTweenSpawnEffect?.Kill();

            _rotateTweenSpawnEffect = resourcePiecesGroupObject.transform.DORotate(EndValueRotatingOnRespawn, durationRotationSpawn, RotateMode.FastBeyond360).OnComplete(StartWindEffect);
            
            RespawnTween = resourcePiecesGroupObject.transform.DOScale(Vector3.one, respawnScaleInDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                resourceCollider.enabled = true;
            });
        }

        private void StartWindEffect()
        {
            _windTweenEffect?.Kill();
            resourcePiecesGroupObject.transform.rotation = Quaternion.identity;

            _windTweenEffect = resourcePiecesGroupObject.transform.DORotate(_endRotationVector, _randomRotationDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}