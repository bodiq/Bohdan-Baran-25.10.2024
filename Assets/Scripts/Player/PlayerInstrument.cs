﻿using System;
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
            if (other.CompareTag(ResourcesTag))
            {
                
            }
        }

        public void StartGather()
        {
            transform.DOScale(Vector3.one, 0.5f);
        }

        public void StopGather()
        {
            transform.DOScale(Vector3.zero, 0.5f);
        }

        public void TurnCollider(bool isActive)
        {
            boxCollider.enabled = isActive;
        }
    }
}