using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supplies
{
    public abstract class Resource : MonoBehaviour
    {
        [SerializeField] protected Collider resourceCollider;
        [SerializeField] protected ResourceType resourceType;
        [SerializeField] protected GameObject[] resourcePieces;
        [SerializeField] protected GameObject resourcePiecesGroupObject;
        [SerializeField] protected int timeToRespawn;
        [SerializeField] protected float respawnScaleInDuration;
        [SerializeField] protected UIResourceCounterManager resourceCounterManager;
        [SerializeField] protected float gatheredAnimationDuration;
        [SerializeField] protected float shakeAnimationPower;
        [SerializeField] private ParticleSystem gatheringParticle;
        [SerializeField] private ParticleSystem destroyParticle;
        
        private Coroutine _respawnResourceCoroutine;
        private Vector3 _respawnStartScale;

        protected Tween GatherTween;
        protected Tween RespawnTween;
        protected Vector3 InitialScaleValue;
        protected int LastIndexTaken = 0;

        public ResourceType ResourceType => resourceType;

        protected virtual void Start()
        {
            resourceCounterManager.Initialize(resourceType);
            InitialScaleValue = resourcePiecesGroupObject.transform.localScale;

            _respawnStartScale = resourceType switch
            {
                ResourceType.Crystal or ResourceType.Stone => new Vector3(InitialScaleValue.x, 0f, InitialScaleValue.z),
                ResourceType.Wood => Vector3.zero,
                _ => _respawnStartScale
            };
        }

        public void GetGathered(int count)
        {
            resourcePieces[LastIndexTaken++].SetActive(false);
            GatherTween?.Kill();
            
            PlayGatheredAnimation();
            gatheringParticle.Play();

            for (var i = 0; i < count; i++)
            {
                var resource = ResourcePoolManager.Instance.GetResource(ResourceType);
                resource.gameObject.SetActive(true);
                resource.AnimateResourceForGathering(transform.position, ResourceType);
            }
            
            resourceCounterManager.ShowAvailableResourceCounter(count);

            if (LastIndexTaken == resourcePieces.Length - 1)
            {
                if (destroyParticle)
                {
                    destroyParticle.Play();
                }
                GameManager.Instance.Player.PlayerGather.TryRemoveResource(gameObject);
                resourceCollider.enabled = false;
            }

            if (_respawnResourceCoroutine != null)
            {
                StopCoroutine(_respawnResourceCoroutine);
            }
            
            _respawnResourceCoroutine = StartCoroutine(StartRespawnCoroutine());
        }

        private IEnumerator StartRespawnCoroutine()
        {
            yield return new WaitForSeconds(timeToRespawn);
            RespawnResource();
        }

        protected void ResetAllPieces()
        {
            resourcePiecesGroupObject.transform.localScale = _respawnStartScale;

            foreach (var piece in resourcePieces)
            {
                piece.gameObject.SetActive(true);
            }
        }
        
        protected abstract void RespawnResource();
        protected abstract void PlayGatheredAnimation();
    }
}