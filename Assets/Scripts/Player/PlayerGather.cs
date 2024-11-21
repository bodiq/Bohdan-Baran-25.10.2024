using System.Collections.Generic;
using Enums;
using Supplies;
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

        private PlayerInstrument _instrumentToUse;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ResourcesTag) && !_activeResources.Contains(other.gameObject))
            {
                _activeResources.Add(other.gameObject);
                if (!_isGathering)
                {
                    var resource = other.GetComponent<Resource>();
                    if (resource)
                    {
                        _instrumentToUse = resource.ResourceType switch
                        {
                            ResourceType.Wood => axe,
                            ResourceType.Crystal or ResourceType.Stone => hammer,
                            _ => _instrumentToUse
                        };
                    }
                    _isGathering = true;
                    playerAnimator.PlayGatherAnimation();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ResourcesTag))
            {
                TryRemoveResource(other.gameObject);
            }
        }

        public void TryRemoveResource(GameObject resource)
        {
            if (_activeResources.Remove(resource))
            {
                if (_activeResources.Count == 0)
                {
                    _isGathering = false;
                }
            }
        }

        private void OnEnableInstrumentCollider()
        {
            _instrumentToUse.TurnCollider(true);
        }
        
        private void OnDisableInstrumentCollider()
        {
            _instrumentToUse.TurnCollider(false);
        }
        
        private void OnGatherStart()
        {
            _instrumentToUse.StartGather();
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
                _instrumentToUse.StopGather();
            }
        }
    }
}
