using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerInstrument : MonoBehaviour
    {
        [SerializeField] private Instruments instrument;
        [SerializeField] private BoxCollider boxCollider;

        private const string ResourcesTag = "Resources";
        
        private void OnTriggerEnter(Collider other)
        {
            if (CompareTag(ResourcesTag))
            {
                Debug.LogError("Boom");
            }
        }

        public void StartGather()
        {
            transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                boxCollider.enabled = true;
            });
        }

        public void StopGather()
        {
            boxCollider.enabled = false;
            transform.DOScale(Vector3.zero, 0.5f);
        }
    }
}