using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using VantasticKick.Core;
using VantasticKick.UI.UiFramework;
using VantasticKick.Utils;

namespace VantasticKick.UI
{
    public class GamePanelView : UiView
    {
        public Action OnGamePanelHidden;
        
        [SerializeField] private TextMeshProUGUI _attemptsText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private RectTransform _statPanel;
        [SerializeField] private Countdown countdown;

        public Countdown Countdown => countdown; 
        protected override void OnOpen(Action onComplete = null)
        {
            gameObject.SetActive(true);
            
            if (_model is GameRound gameRound)
            {
                gameRound.OnAttemptsChanged += OnAttemptsChanged;
                gameRound.OnScoreChanged += OnScoreChanged;
                gameRound.OnCombo += OnCombo;
                gameRound.OnRoundFinish += OnRoundFinish;
                
                gameRound.UpdateRound();
            }

            _statPanel.anchoredPosition = 200 * Vector3.up;
            _statPanel.DOAnchorPos(Vector2.zero, 0.33f).SetEase(Ease.InBack).onComplete += () =>
            {
                onComplete?.Invoke();
            };
        }

        protected override void OnClose(Action onComplete = null)
        {
            if (_model is GameRound gameRound)
            {
                gameRound.OnAttemptsChanged -= OnAttemptsChanged;
                gameRound.OnScoreChanged -= OnScoreChanged;
                gameRound.OnCombo -= OnCombo;
                gameRound.OnRoundFinish -= OnRoundFinish;
            }
            
            _statPanel.DOAnchorPos(200 * Vector2.up, 0.33f).SetEase(Ease.OutBack).onComplete += () =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            };
        }
        
        private void OnAttemptsChanged(int attempts, int maxAttempts)
        {
            _attemptsText.text = $"Attempts: {attempts}/{maxAttempts}";
        }
        
        private void OnScoreChanged(int shotScore, int totalScore)
        {
            if (shotScore > 0)
            {
                _scoreText.transform.localScale = 1.1f * Vector3.one;
                _scoreText.transform.DOScale(Vector3.one, 0.33f).SetEase(Ease.InBack);
            }
            _scoreText.text = $"Score: {totalScore}";
        }
        
        private void OnCombo(int combo)
        {
            if (combo > 1)
            {
                //Display combo
            }
        }

        private void OnRoundFinish()
        {
            OnGamePanelHidden?.Invoke();
        }
    }
}
