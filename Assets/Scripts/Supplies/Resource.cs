using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Supplies
{
    public abstract class Resource : MonoBehaviour
    {
        [SerializeField] protected Collider resourceCollider;
        [SerializeField] protected ResourceType resourceType;
        [SerializeField] protected GameObject[] resourcePieces;
        [SerializeField] protected GameObject resourcePiecesGroupObject;
        [SerializeField] protected int timeToRespawn;

        protected int LastIndexTaken = 0;
        private Coroutine _respawnResourceCoroutine;
        private Tween _gatherTween;
        protected Tween RespawnTween;
        private readonly Vector3 _gatherEndScaleValue = new (0.8f, 0.8f, 0.8f);

        public ResourceType ResourceType => resourceType;

        public void GetGathered()
        {
            resourcePieces[LastIndexTaken++].SetActive(false);
            _gatherTween?.Kill();
            _gatherTween = resourcePiecesGroupObject.transform.DOScale(_gatherEndScaleValue, 0.1f).SetLoops(2, LoopType.Yoyo);

            if (LastIndexTaken == resourcePieces.Length - 1)
            {
                GameManager.Instance.Player.PlayerGather.TryRemoveResource(gameObject);
                resourceCollider.enabled = false;
            }

            if (_respawnResourceCoroutine != null)
            {
                StopCoroutine(_respawnResourceCoroutine);
            }
            
            _respawnResourceCoroutine = StartCoroutine(StartRespawnCoroutine());
        }

        protected abstract void RespawnResource();

        private IEnumerator StartRespawnCoroutine()
        {
            yield return new WaitForSeconds(timeToRespawn);
            RespawnResource();
        }

        protected void ResetAllPieces()
        {
            resourcePiecesGroupObject.transform.localScale = Vector3.zero;
            foreach (var piece in resourcePieces)
            {
                piece.gameObject.SetActive(true);
            }
        }
    }
}