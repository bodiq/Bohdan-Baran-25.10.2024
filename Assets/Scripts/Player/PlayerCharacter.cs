using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement component missing on " + gameObject.name);
            }

            GameManager.Instance.Player = this;
        }

        void Update()
        {
            if (_playerMovement)
            {
                _playerMovement.Move();
            }
        }
    }
}
