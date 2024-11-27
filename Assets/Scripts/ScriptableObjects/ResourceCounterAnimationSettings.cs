using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourceCounterAnimationSettings", menuName = "ScriptableObjects/Resource Counter Animation Settings")]
    public class ResourceCounterAnimationSettings : ScriptableObject
    {
        [SerializeField] private float durationMoveAnimation;
        [SerializeField] private float durationScaleAnimation;
        [SerializeField] private float durationFadeInAnimation;
        [SerializeField] private float durationFadeOutAnimation;
        [SerializeField] private float delayBeforeDestroy;

        public float DurationMoveAnimation => durationMoveAnimation;
        public float DurationScaleAnimation => durationScaleAnimation;
        public float DurationFadeInAnimation => durationFadeInAnimation;
        public float DurationFadeOutAnimation => durationFadeOutAnimation;
        public float DelayBeforeDestroy => delayBeforeDestroy;
    }
}