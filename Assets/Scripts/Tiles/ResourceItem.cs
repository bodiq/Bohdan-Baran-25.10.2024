using DG.Tweening;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class ResourceItem : MonoBehaviour
    {
        private static readonly Vector3 RandomSpawnPositionRange = new (0.5f, 0.5f, 0.5f);
        private static readonly Vector3 RandomSpawnRotationRange = new (0, 360, 0);
        private static readonly Vector3 RandomEndPositionFlyRange = new (1.2f, 0f, 1.2f);

        private static readonly float EndPositionHeightOffset = 0.7f;
        
        private Vector3 _randomPositionOffset;
        private Vector3 _randomEndPositionOffset;
        
        private Tween _jumpTween;
        
        private void Awake()
        {
            var randomXOffsetPosition = Random.Range(-RandomSpawnPositionRange.x, RandomSpawnPositionRange.x);
            var randomYOffsetPosition = Random.Range(-RandomSpawnPositionRange.y, RandomSpawnPositionRange.y);
            var randomZOffsetPosition = Random.Range(-RandomSpawnPositionRange.z, RandomSpawnPositionRange.z);
            
            var randomXOffsetEndPosition = Random.Range(-RandomEndPositionFlyRange.x, RandomEndPositionFlyRange.x);
            var randomZOffsetEndPosition = Random.Range(-RandomEndPositionFlyRange.z, RandomEndPositionFlyRange.z);
            
            var randomYOffsetRotation = Random.Range(0, RandomSpawnRotationRange.y);

            _randomPositionOffset = new Vector3(randomXOffsetPosition, randomYOffsetPosition, randomZOffsetPosition);
            _randomEndPositionOffset = new Vector3(randomXOffsetEndPosition, EndPositionHeightOffset, randomZOffsetEndPosition);
            transform.rotation = Quaternion.Euler(0f, randomYOffsetRotation, 0f);
        }

        public void TriggerResourceFly(Vector3 startPosition, Vector3 endPosition, ResourcesIndicator resourcesIndicator, int count)
        {
            transform.position = startPosition + _randomPositionOffset;

            var firstEndPosition = endPosition + _randomEndPositionOffset;

            _jumpTween = DOTween.Sequence()
                .Append(transform.DOJump(firstEndPosition, 2.5f, 1, 0.7f).SetEase(Ease.Linear))
                .Append(transform.DOMove(endPosition, 0.2f))
                .OnComplete(() =>
                {
                    ResourcePoolManager.Instance.ReturnResource(resourcesIndicator.ResourceType, this);
                    resourcesIndicator.IncreaseResourceTextAmount(count);
                });
        }

        private void OnDisable()
        {
            _jumpTween?.Kill();
        }
    }
}