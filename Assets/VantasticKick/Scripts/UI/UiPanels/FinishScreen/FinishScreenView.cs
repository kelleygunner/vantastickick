using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.UI
{
    public class FinishScreenView : UiView
    {
        public Action OnFinishButtonClick;
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _finishButton;

        protected override void OnOpen(Action onComplete = null)
        {
            gameObject.SetActive(true);
            if (_model is FinishScreenModel finishScreenModel)
            {
                _scoreText.text = $"Score: {finishScreenModel.Score}";
            }
            _finishButton.onClick.AddListener(()=>OnFinishButtonClick?.Invoke());
            onComplete?.Invoke();
        }

        protected override void OnClose(Action onComplete = null)
        {
            gameObject.SetActive(false);
            _finishButton.onClick.RemoveAllListeners();
            onComplete?.Invoke();
        }
    }
}
