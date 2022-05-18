using System;
using System.Collections;
using TMPro;
using UnityEngine;
using VantasticKick.Core;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.UI
{
    public class GamePanelView : UiView
    {
        public Action OnGamePanelHidden;
        
        [SerializeField] private TextMeshProUGUI _attemptsText;
        [SerializeField] private TextMeshProUGUI _scoreText;

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

            StartCoroutine(TestRound());
            
            onComplete?.Invoke();
        }

        protected override void OnClose(Action onComplete = null)
        {
            gameObject.SetActive(false);
            
            if (_model is GameRound gameRound)
            {
                gameRound.OnAttemptsChanged -= OnAttemptsChanged;
                gameRound.OnScoreChanged -= OnScoreChanged;
                gameRound.OnCombo -= OnCombo;
                gameRound.OnRoundFinish -= OnRoundFinish;
            }
            onComplete?.Invoke();
        }
        
        private void OnAttemptsChanged(int attempts, int maxAttempts)
        {
            _attemptsText.text = $"Attempts: {attempts}/{maxAttempts}";
        }
        
        private void OnScoreChanged(int shotScore, int totalScore)
        {
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

        private IEnumerator TestRound()
        {
            if (_model is GameRound gameRound)
            {
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(false);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(false);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(false);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(true);
                yield return new WaitForSeconds(1f);
                gameRound.RegisterShot(false);   
            }
        }
    }
}
