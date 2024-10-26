using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        [SerializeField] private Animator animator;

        private Joystick inputJoystick;
        private CharacterController characterController;

        private Vector3 moveDirection = Vector3.zero;

        private const string AnimationSpeedParameterName = "Speed";

        private void Start()
        {
            inputJoystick = GameManager.Instance.Joystick;
            characterController = GetComponent<CharacterController>();
        }

        public void Move()
        {
            if (inputJoystick != null)
            {
                moveDirection = Vector3.forward * inputJoystick.Vertical + Vector3.right * inputJoystick.Horizontal;
                animator.SetFloat(AnimationSpeedParameterName, moveDirection.magnitude);
            }

            var movement = moveDirection.normalized * (moveSpeed * Time.deltaTime);

            characterController.Move(movement);

            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }
}
