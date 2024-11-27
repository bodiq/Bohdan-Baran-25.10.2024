using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using Supplies;
using UnityEngine;

namespace Player
{
    public class PlayerInstrument : MonoBehaviour
    {
        [SerializeField] private Instruments instrument;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private int countForHit = 2;
        [SerializeField] private TrailRenderer trailRenderer;

        private const string ResourcesTag = "Resources";

        private static readonly Vector3 EndScaleInstrument = new (1.3f, 1.3f, 1.3f);
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ResourcesTag))
            {
                var resource = other.GetComponent<Resource>();
                if (resource)
                {
                    if (CanGather(resource.ResourceType))
                    {
                        resource.GetGathered(countForHit);
                        UIManager.Instance.UIResourceIndicatorManager.ChangeResourceIndicatorAmount(resource.ResourceType, countForHit);
                    }
                }
            }
        }
        
        public bool CanGather(ResourceType resourceType)
        {
            return instrument switch
            {
                Instruments.Axe => resourceType == ResourceType.Wood,
                Instruments.Hammer => resourceType == ResourceType.Crystal || resourceType == ResourceType.Stone,
                _ => false
            };
        }

        public void StartGather()
        {
            transform.DOScale(EndScaleInstrument, 0.5f);
        }

        public void StopGather()
        {
            transform.DOScale(Vector3.zero, 0.5f);
        }

        public void TurnCollider(bool isActive)
        {
            boxCollider.enabled = isActive;
        }

        public void TurnTrail(bool isActive)
        {
            trailRenderer.emitting = isActive;
        }
    }
}