using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField] private List<UIScreen> uiScreens = new();

        private UIResourceIndicatorManager _uiResourceIndicatorManager;

        public UIResourceIndicatorManager UIResourceIndicatorManager => _uiResourceIndicatorManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            
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
            _uiResourceIndicatorManager = Configs.ConfigHelper.GetUIScreen<UIResourceIndicatorManager>(uiScreens);
        }
    }
}
