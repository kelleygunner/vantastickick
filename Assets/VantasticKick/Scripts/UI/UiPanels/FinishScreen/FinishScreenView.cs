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
        private void Start()
        {
            if (_model is FinishScreenModel finishScreenModel)
            {
                _scoreText.text = $"Score: {finishScreenModel.Score}";
            }
            _finishButton.onClick.AddListener(()=>OnFinishButtonClick?.Invoke());
        }

        private void OnDestroy()
        {
            _finishButton.onClick.RemoveAllListeners();
        }
    }
}
