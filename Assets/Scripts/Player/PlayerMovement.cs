using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravity = -9.81f; // Гравітація, яка буде діяти вниз
        
        private Vector3 velocity;
        
        [SerializeField] private Animator animator;

        private Joystick _inputJoystick;
        private CharacterController _characterController;

        private Vector3 _moveDirection = Vector3.zero;
        private static readonly int Speed = Animator.StringToHash(AnimationSpeedParameterName);

        private const string AnimationSpeedParameterName = "Speed";

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
            if (_inputJoystick != null)
            {
                _moveDirection = Vector3.forward * _inputJoystick.Vertical + Vector3.right * _inputJoystick.Horizontal;
                animator.SetFloat(Speed, _moveDirection.magnitude);
            }
            
            var movement = _moveDirection.normalized * (moveSpeed * Time.deltaTime);
            
            if (!_characterController.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = -2f;
            }

            movement.y = velocity.y * Time.deltaTime;
            
            _characterController.Move(movement);
            
            if (_moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_moveDirection);
            }

        }
    }
}
