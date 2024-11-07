using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Animator animator;
        [SerializeField] private TileRestrictedMovement restrictedMovement;
        
        private Vector3 _velocity;

        private Joystick _inputJoystick;
        private CharacterController _characterController;

        private Vector3 _moveDirection = Vector3.zero;
        private static readonly int Speed = Animator.StringToHash(AnimationSpeedParameterName);

        private const string AnimationSpeedParameterName = "Speed";

        private float _baseMoveSpeed = 3f;

        private void Start()
        {
            _inputJoystick = GameManager.Instance?.Joystick;
            _characterController = GetComponent<CharacterController>();

            if (_inputJoystick == null)
            {
                Debug.LogError("Joystick not set in GameManager.");
            }
        }

        public void Move()
        {
            if (restrictedMovement.IsOnTile)
            {
                if (_inputJoystick != null)
                {
                    _moveDirection = Vector3.forward * _inputJoystick.Vertical + Vector3.right * _inputJoystick.Horizontal;
                    animator.SetFloat(Speed, _moveDirection.magnitude);
                }
                
                animator.speed = Mathf.Clamp(moveSpeed / _baseMoveSpeed, 0.5f, 2f); // Межі: від 0.5 до 2x швидкості
                var movement = _moveDirection.normalized * (moveSpeed * Time.deltaTime);
            
                if (!_characterController.isGrounded)
                {
                    _velocity.y += gravity * Time.deltaTime;
                }
                else
                {
                    _velocity.y = -2f;
                }

                movement.y = _velocity.y * Time.deltaTime;
            
                _characterController.Move(movement);
            
                if (_moveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(_moveDirection);
                }
            }
        }
    }
}
