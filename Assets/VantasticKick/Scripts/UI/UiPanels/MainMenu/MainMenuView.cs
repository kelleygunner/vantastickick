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

        protected override void OnOpen(Action onComplete = null)
        {
            gameObject.SetActive(true);
            _playButton.onClick.AddListener(()=>OnPlayClick?.Invoke());
            onComplete?.Invoke();
        }

        protected override void OnClose(Action onComplete = null)
        {
            gameObject.SetActive(false);
            _playButton.onClick.RemoveAllListeners();
            onComplete?.Invoke();
        }
    }
}
