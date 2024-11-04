using System.Collections.Generic;
using Constants;
using Data;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        public Dictionary<ResourceType, int> PlayerResourceCount = new();

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement component missing on " + gameObject.name);
            }

            GameManager.Instance.Player = this;

            foreach (var type in ResourceConstants.ListResourceTypes)
            {
                PlayerResourceCount.Add(type, Random.Range(1000, 10000));
            }
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
