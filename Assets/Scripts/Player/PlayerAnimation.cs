using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
    
        private static readonly int Speed = Animator.StringToHash(AnimationSpeedParameterName);
        private static readonly int IsGathering = Animator.StringToHash(AnimationGatherParameterName);
    
        private const string AnimationSpeedParameterName = "Speed";
        private const string AnimationGatherParameterName = "Gathering";

        public void SetMovementSpeed(float speedMovement)
        {
            animator.SetFloat(Speed, speedMovement);
        }

        public void ChangeAnimationSpeed(float animationSpeed)
        {
            animator.speed = animationSpeed;
        }

        public void SetGatheringState(bool isActive)
        {
            animator.SetBool(IsGathering, isActive);
        }
    }
}
