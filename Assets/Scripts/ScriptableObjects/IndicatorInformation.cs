using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "IndicatorInformation", menuName = "ScriptableObjects/Indicator Information")]
    public class IndicatorInformation : ScriptableObject
    {
        [SerializeField] private float activationAnimationDuration = 0.2f;
        
        private const float IntervalBetweenSpawnResources = 0.08f;
        
        public readonly Vector3 AnimationEndScale = new (0.7f, 0.7f, 0.7f);
        public readonly WaitForSeconds WaitBetweenSpawnResources = new (IntervalBetweenSpawnResources);

        public float ActivationAnimationDuration => activationAnimationDuration;
    }
}