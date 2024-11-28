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
        [SerializeField] private float animationScaleInOut = 0.3f;

        private const string ResourcesTag = "Resources";

        private static readonly Vector3 EndScaleInstrument = new (1.3f, 1.3f, 1.3f);
        private static readonly Vector3 StartScaleInstrument = Vector3.zero;

        private Tween _instrumentScaleTween;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ResourcesTag)) return;

            if (!other.TryGetComponent<Resource>(out var resource)) return;

            if (CanGather(resource.ResourceType))
            {
                GatherResource(resource);
            }
        }
        
        private void GatherResource(Resource resource)
        {
            resource.GetGathered(countForHit);
            UIManager.Instance.UIResourceIndicatorManager.ChangeResourceIndicatorAmount(resource.ResourceType, countForHit);
        }
        
        private bool CanGather(ResourceType resourceType)
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
            PlayScaleAnimation(EndScaleInstrument);
        }

        public void StopGather()
        {
            PlayScaleAnimation(StartScaleInstrument);
        }

        private void PlayScaleAnimation(Vector3 targetScale)
        {
            _instrumentScaleTween?.Kill();
            _instrumentScaleTween = transform.DOScale(targetScale, animationScaleInOut);
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