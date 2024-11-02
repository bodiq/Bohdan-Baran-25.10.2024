using DG.Tweening;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourceItem : MonoBehaviour
    {
        private Vector3 _randomPositionRange = new (0.5f, 0.5f, 0.5f);
        private Vector3 _randomRotationRange = new (0, 360, 0);

        private Vector3 _randomPositionOffset;
        
        private float _randomXOffsetPosition;
        private float _randomYOffsetPosition;
        private float _randomZOffsetPosition;
        
        private float _randomXOffsetRotation;
        private float _randomYOffsetRotation;
        private float _randomZOffsetRotation;

        private Tween _jumpTween;
        
        private void Awake()
        {
            _randomXOffsetPosition = Random.Range(-_randomPositionRange.x, _randomPositionRange.x);
            _randomYOffsetPosition = Random.Range(-_randomPositionRange.y, _randomPositionRange.y);
            _randomZOffsetPosition = Random.Range(-_randomPositionRange.z, _randomPositionRange.z);
            
            _randomXOffsetRotation = Random.Range(0, _randomRotationRange.x);
            _randomYOffsetRotation = Random.Range(0, _randomRotationRange.y);
            _randomZOffsetRotation = Random.Range(0, _randomRotationRange.z);

            _randomPositionOffset = new Vector3(_randomXOffsetPosition, _randomYOffsetPosition, _randomZOffsetPosition);
            transform.rotation = Quaternion.Euler(_randomXOffsetRotation, _randomYOffsetRotation, _randomZOffsetRotation);
        }

        public void TriggerResourceFly(Vector3 startPosition, Vector3 endPosition, ResourcesIndicator resourcesIndicator)
        {
            transform.position = startPosition + _randomPositionOffset;
            
            _jumpTween = transform.DOJump(endPosition, 2f, 1, 1f).OnComplete(() =>
            {
                ResourcePoolManager.Instance.ReturnResource(resourcesIndicator.ResourceType, this);
                resourcesIndicator.IncreaseResourceAmount();
            });
        }
    }
}