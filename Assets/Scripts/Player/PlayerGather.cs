using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGather : MonoBehaviour
    {
        [SerializeField] private PlayerAnimation playerAnimator;

        [SerializeField] private PlayerInstrument axe;
        [SerializeField] private PlayerInstrument hammer;
    
        private const string ResourcesTag = "Resources";

        private List<GameObject> _activeResources = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ResourcesTag) && !_activeResources.Contains(other.gameObject))
            {
                _activeResources.Add(other.gameObject);
                StartGathering();    
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ResourcesTag))
            {
                if (_activeResources.Remove(other.gameObject))
                {
                    if (_activeResources.Count == 0)
                    {
                        StopGathering();
                    }
                }
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
