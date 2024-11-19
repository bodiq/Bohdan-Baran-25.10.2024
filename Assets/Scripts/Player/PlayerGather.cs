using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerGather : MonoBehaviour
    {
        [SerializeField] private PlayerAnimation playerAnimator;

        [SerializeField] private PlayerInstrument axe;
        [SerializeField] private PlayerInstrument hammer;
    
        private const string ResourcesTag = "Resources";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ResourcesTag))
            {
                StartGathering();    
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(ResourcesTag))
            {
                StopGathering();
            }
        }

        private void StopGathering()
        {
            axe.StopGather();
            playerAnimator.SetGatheringState(false);
        }

        private void StartGathering()
        {
            axe.StartGather();
            playerAnimator.SetGatheringState(true);
        }
    }
}
