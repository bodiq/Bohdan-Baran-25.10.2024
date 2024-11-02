using DG.Tweening;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourceItem : MonoBehaviour
    {
        private readonly Vector3 _randomSpawnPositionRange = new (0.5f, 0.5f, 0.5f);
        private readonly Vector3 _randomSpawnRotationRange = new (0, 360, 0);

        private readonly Vector3 _randomEndPositionFlyRange = new (1.2f, 0f, 1.2f);

        private Vector3 _randomPositionOffset;
        private Vector3 _randomEndPositionOffset;
        
        private Tween _jumpTween;
        
        private void Awake()
        {
            var randomXOffsetPosition = Random.Range(-_randomSpawnPositionRange.x, _randomSpawnPositionRange.x);
            var randomYOffsetPosition = Random.Range(-_randomSpawnPositionRange.y, _randomSpawnPositionRange.y);
            var randomZOffsetPosition = Random.Range(-_randomSpawnPositionRange.z, _randomSpawnPositionRange.z);
            
            var randomXOffsetEndPosition = Random.Range(-_randomEndPositionFlyRange.x, _randomEndPositionFlyRange.x);
            var randomZOffsetEndPosition = Random.Range(-_randomEndPositionFlyRange.z, _randomEndPositionFlyRange.z);
            
            var randomXOffsetRotation = Random.Range(0, _randomSpawnRotationRange.x);
            var randomYOffsetRotation = Random.Range(0, _randomSpawnRotationRange.y);
            var randomZOffsetRotation = Random.Range(0, _randomSpawnRotationRange.z);

            _randomPositionOffset = new Vector3(randomXOffsetPosition, randomYOffsetPosition, randomZOffsetPosition);
            _randomEndPositionOffset = new Vector3(randomXOffsetEndPosition, 0.7f, randomZOffsetEndPosition);
            transform.rotation = Quaternion.Euler(randomXOffsetRotation, randomYOffsetRotation, randomZOffsetRotation);
        }

        public void TriggerResourceFly(Vector3 startPosition, Vector3 endPosition, ResourcesIndicator resourcesIndicator)
        {
            transform.position = startPosition + _randomPositionOffset;

            var firstEndPosition = endPosition + _randomEndPositionOffset;
            
            _jumpTween = transform.DOJump(firstEndPosition, 2.5f, 1, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMove(endPosition, 0.2f).OnComplete(() =>
                {
                    ResourcePoolManager.Instance.ReturnResource(resourcesIndicator.ResourceType, this);
                    resourcesIndicator.IncreaseResourceAmount();
                });
            });
        }
    }
}