using System.Collections.Generic;
using Configs;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {

        [SerializeField] private List<UIScreen> uiScreens = new();

        private UIResourceIndicatorManager _uiResourceIndicatorManager;

        public UIResourceIndicatorManager UIResourceIndicatorManager => _uiResourceIndicatorManager;

        protected override void Awake()
        {
            base.Awake();
            GetScreens();
        }

        public void Initialize()
        {
            foreach (var uiScreen in uiScreens)
            {
                uiScreen.Initialize();
            }
        }

        private void GetScreens()
        {
            _uiResourceIndicatorManager = ConfigHelper.GetUIScreen<UIResourceIndicatorManager>(uiScreens);
        }
    }
}
