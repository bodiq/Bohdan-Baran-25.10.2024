using Cinemachine;
using Player;
using UnityEngine;

namespace CameraScripts
{
    public class SmoothCameraOffset : MonoBehaviour
    {
        [SerializeField] private float offsetDistance = 3.0f;
        [SerializeField] private float smoothTime = 0.5f;      
        [SerializeField] private float maxTransitionSpeed = 2.0f;  

        private CinemachineFramingTransposer _framingTransposer;
        private Vector3 _lastDirection;
        private Vector3 _velocity = Vector3.zero;
        private PlayerCharacter _player;

        void Start()
        {
            _player = GameManager.Instance.Player;
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        void LateUpdate()
        {
            var movementDirection = _player.transform.position - _framingTransposer.transform.position;
            if (movementDirection.magnitude > 0.1f)
            {
                _lastDirection = Vector3.Lerp(_lastDirection, movementDirection.normalized, Time.deltaTime * maxTransitionSpeed);
            }
        
            var targetOffset = _lastDirection * offsetDistance;
            targetOffset.y = _framingTransposer.m_TrackedObjectOffset.y;  
        
            _framingTransposer.m_TrackedObjectOffset = Vector3.SmoothDamp(
                _framingTransposer.m_TrackedObjectOffset,
                targetOffset,
                ref _velocity,
                smoothTime
            );
        }
    }
}