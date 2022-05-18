using System;
using UnityEngine;
using UnityEngine.UI;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.UI
{
    public class MainMenuView : UiView
    {
        public Action OnPlayClick;

        [SerializeField] private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(()=>OnPlayClick?.Invoke());
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
        }
    }
}
