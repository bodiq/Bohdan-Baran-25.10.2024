using Configs;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [Header("Input Setup")]
        [SerializeField] private Joystick joystick;

        [SerializeField] private PlayerCharacter player;
    

        public Joystick Joystick => joystick;
        public PlayerCharacter Player => player;

        protected override void Awake()
        {
            base.Awake();
            DOTween.SetTweensCapacity(2000, 500);
        }

        private void Start()
        {
            UIManager.Instance.Initialize();
        }
    }
}
