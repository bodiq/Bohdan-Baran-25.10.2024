using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component missing on " + gameObject.name);
            }

            GameManager.Instance.Player = this;
        }

        void Update()
        {
            if (playerMovement)
            {
                playerMovement.Move();
            }
        }
    }
}
