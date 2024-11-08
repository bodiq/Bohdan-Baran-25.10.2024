using UnityEngine;

namespace Constants
{
    public static class IndicatorConstants
    {
        private const float IntervalBetweenSpawnResources = 0.08f;
        public const float ActivationAnimationDuration = 0.2f;
        public static readonly Vector3 AnimationEndScale = new (0.7f, 0.7f, 0.7f);
        public static readonly WaitForSeconds WaitBetweenSpawnResources = new (IntervalBetweenSpawnResources);
    }
}