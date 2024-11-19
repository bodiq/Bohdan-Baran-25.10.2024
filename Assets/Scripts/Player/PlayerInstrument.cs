using DG.Tweening;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerInstrument : MonoBehaviour
    {
        [SerializeField] private Instruments instrument;
        [SerializeField] private BoxCollider boxCollider;

        public void StartGather()
        {
            transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                boxCollider.enabled = true;
            });
        }

        public void StopGather()
        {
            boxCollider.enabled = false;
            transform.DOScale(Vector3.zero, 0.2f);
        }
    }
}