using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private VariableJoystick inputJoystick;
        private CharacterController characterController;

        private Vector3 moveDirection = Vector3.zero;

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
            }

            var movement = moveDirection * (moveSpeed * Time.deltaTime);

            characterController.Move(movement);
        }
    }
}
