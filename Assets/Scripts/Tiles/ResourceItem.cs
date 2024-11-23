using Configs;
using DG.Tweening;
using Enums;
using Managers;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class ResourceItem : MonoBehaviour
    {
        [SerializeField] private ResourceAnimationSettings resourceAnimationSettings;

        private Vector3 _randomPositionOffsetTileFill;
        private Vector3 _randomEndPositionOffsetTileFill;
        
        private Vector3 _randomRotationAxisGathering;
        private Vector3 _randomEndJumpPositionGathering;
        
        private Tween _jumpTween;
        
        private Vector3 _startPosition;
        
        private void Awake()
        {
            InitializeGatheringOffsets();
            InitializeTileFillingOffsets();
        }

        private void InitializeGatheringOffsets()
        {
            _randomRotationAxisGathering = Random.onUnitSphere;
            
            var randomX = Random.Range(-1.5f, 1.5f);
            var randomZ = Random.Range(-1.5f, 1.5f);
            var randomY = Random.Range(1.25f, 1.75f);

            _randomEndJumpPositionGathering = new Vector3(randomX, randomY, randomZ);
        }

        private void InitializeTileFillingOffsets()
        {
            var randomYOffsetRotation = Random.Range(0, resourceAnimationSettings.RandomSpawnRotationRangeTileFill.y);

            _randomPositionOffsetTileFill = ConfigHelper.GenerateRandomVector(resourceAnimationSettings.RandomSpawnPositionRangeTileFill);
            _randomEndPositionOffsetTileFill = ConfigHelper.GenerateRandomVector(resourceAnimationSettings.RandomEndPositionFlyRangeTileFill);
            
            _randomEndPositionOffsetTileFill.y = resourceAnimationSettings.TileFillEndPositionHeightOffset;
            transform.rotation = Quaternion.Euler(0f, randomYOffsetRotation, 0f);
        }
        
        public void AnimateResourceForTileFilling(Vector3 startPosition, Vector3 endPosition, ResourcesIndicator resourcesIndicator, int count, int remainder = 0)
        {
            transform.position = startPosition + _randomPositionOffsetTileFill;

            var firstEndPosition = endPosition + _randomEndPositionOffsetTileFill;

            _jumpTween = DOTween.Sequence()
                .Append(transform.DOJump(firstEndPosition, resourceAnimationSettings.TileFillJumpPower, 1, resourceAnimationSettings.TileFillJumpDurationToFirstPos).SetEase(Ease.Linear))
                .Append(transform.DOMove(endPosition, resourceAnimationSettings.TileFillJumpDurationToSecondPos))
                .OnComplete(() =>
                {
                    ReturnResourceToPool(resourcesIndicator.ResourceType);
                    resourcesIndicator.IncreaseResourceTextAmount(count, remainder);
                });
        }

        public void AnimateResourceForGathering(Vector3 startPosition, ResourceType resourceType)
        {
            transform.position = startPosition;

            var randomJumpPos = transform.position + _randomEndJumpPositionGathering;

            _jumpTween = DOTween.Sequence()
                .Append(transform.DOJump(randomJumpPos, resourceAnimationSettings.GatheringJumpPower, 1,
                    resourceAnimationSettings.GatheringJumpDuration).SetEase(Ease.Linear))
                .Join(transform.DORotate(_randomRotationAxisGathering * 360,
                    resourceAnimationSettings.GatheringJumpDuration, RotateMode.LocalAxisAdd))
                .Append(DOTween.To(MoveToPlayer, 0f, 1f, resourceAnimationSettings.GatheringMoveToPlayerDuration).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    ReturnResourceToPool(resourceType);
                    _startPosition = Vector3.zero;
                });
        }

        private void MoveToPlayer(float value)
        {
            if (_startPosition == Vector3.zero)
            {
                _startPosition = transform.position;
            }
            
            transform.position = Vector3.Lerp(_startPosition, GameManager.Instance.Player.GetResourceEndPos, value);
        }
        
        private void ReturnResourceToPool(ResourceType resourceType)
        {
            ResourcePoolManager.Instance.ReturnResource(resourceType, this);
        }

        private void OnDisable()
        {
            _jumpTween?.Kill();
        }
    }
}