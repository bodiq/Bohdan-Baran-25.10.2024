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

        private bool _isGathering = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ResourcesTag) && !_activeResources.Contains(other.gameObject))
            {
                _activeResources.Add(other.gameObject);
                if (!_isGathering)
                {
                    _isGathering = true;
                    playerAnimator.PlayGatherAnimation();
                }
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
                        _isGathering = false;
                    }
                }
            }
        }

        private void OnEnableInstrumentCollider()
        {
            axe.TurnCollider(true);
        }
        
        private void OnDisableInstrumentCollider()
        {
            axe.TurnCollider(false);
        }
        
        private void OnGatherStart()
        {
            axe.StartGather();
        }

        private void OnGatherStop()
        {
            if (_isGathering)
            {
                playerAnimator.PlayGatherAnimation();
            }
            else
            {
                playerAnimator.SetGatheringState(false);
                axe.StopGather();
            }
        }
    }
}
