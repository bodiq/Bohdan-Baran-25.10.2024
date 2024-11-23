using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private GameObject stepDustParticle;
        [SerializeField] private ResourcesInformation resourcesInformation;
        [SerializeField] private PlayerGather playerGather;
        [SerializeField] private Transform collectResourcesPoint;

        private PlayerMovement _playerMovement;

        public Dictionary<ResourceType, int> PlayerResourceCount = new();
        public PlayerGather PlayerGather => playerGather;

        public GameObject StepDustParticle => stepDustParticle;

        public Vector3 GetResourceEndPos => collectResourcesPoint.position;

        private void Awake()
        {
            InitializeResources();
        }

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement component missing on " + gameObject.name);
            }
        }
        
        private void InitializeResources()
        {
            foreach (var type in resourcesInformation.ListResourceTypes)
            {
                PlayerResourceCount[type] = Random.Range(1000, 5000);
            }
        }

        void Update()
        {
            _playerMovement?.Move();
        }
    }
}
