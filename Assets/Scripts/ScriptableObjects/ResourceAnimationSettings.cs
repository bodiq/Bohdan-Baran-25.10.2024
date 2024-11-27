using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourceAnimationSettings", menuName = "ScriptableObjects/Resource Animation Settings")]
    public class ResourceAnimationSettings : ScriptableObject
    {
        [Header("Tile Fill Settings")]
        [SerializeField] private Vector3 randomSpawnPositionRangeTileFill = new(0.5f, 0.5f, 0.5f);
        [SerializeField] private Vector3 randomEndPositionFlyRangeTileFill = new(1.4f, 0f, 1.4f);
        [SerializeField] private float tileFillJumpPower = 2.5f;

        [SerializeField] private float tileFillJumpDurationToFirstPos = 0.7f;
        [SerializeField] private float tileFillJumpDurationToSecondPos = 0.2f;
        [SerializeField] private float tileFillEndPositionHeightOffset = 0.7f;

        [Space(5)]
        [Header("Gathering Settings")]
        [SerializeField] private float gatheringJumpPower = 2f;
        [SerializeField] private float gatheringJumpDuration = 0.7f;
        [SerializeField] private float gatheringMoveToPlayerDuration = 0.4f;


        public Vector3 RandomSpawnPositionRangeTileFill => randomSpawnPositionRangeTileFill;
        public Vector3 RandomEndPositionFlyRangeTileFill => randomEndPositionFlyRangeTileFill;
        public float TileFillJumpPower => tileFillJumpPower;

        public float TileFillJumpDurationToFirstPos => tileFillJumpDurationToFirstPos;
        public float TileFillJumpDurationToSecondPos => tileFillJumpDurationToSecondPos;
        public float TileFillEndPositionHeightOffset => tileFillEndPositionHeightOffset;

        public float GatheringJumpPower => gatheringJumpPower;
        public float GatheringJumpDuration => gatheringJumpDuration;
        public float GatheringMoveToPlayerDuration => gatheringMoveToPlayerDuration;
    }
}