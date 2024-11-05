using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField] private List<IUIScreen> uiScreens = new();

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
        }

        public void Initialize()
        {
            foreach (var uiScreen in uiScreens)
            {
                uiScreen.Initialize();
            }
        }
    }
}
