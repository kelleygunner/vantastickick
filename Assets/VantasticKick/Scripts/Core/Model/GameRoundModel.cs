using System;
using UnityEngine;
using VantasticKick.Config;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.Core
{
    public class GameRoundModel : UiModel
    {
        public Action<int,int> OnAttemptsChanged;
        public Action<int,int> OnScoreChanged;
        public Action<int> OnCombo;
        public Action OnRoundFinish;
        public Action<int,Vector3> OnGoalScoredAt;
        public Action<int,Vector3> OnHitTargetAt;

        public int Score => _score;
        public int Attempts => _attempts;
        public int MaxAttempts => _maxAttempts;

        private GameConfig _gameConfig;
        private int _attempts;
        private int _maxAttempts;
        private int _shotScore;
        private int _targetScore;
        private int _score;
        private int _combo;
        

        public GameRoundModel(GameConfig config)
        {
            _gameConfig = config;
            _maxAttempts = _gameConfig.gameround.attempts;
        }

        public void CalculateKick()
        {
            var kickScore = _shotScore + _targetScore;
            _score += kickScore;

            if (_targetScore > 0)
            {
                _combo++;
            }
            else
            {
                _combo = 0;
            }
            
            OnAttemptsChanged?.Invoke(_attempts, _maxAttempts);
            OnScoreChanged?.Invoke(kickScore,_score);
            OnCombo?.Invoke(_combo);
            OnAttemptsChanged?.Invoke(_attempts, _maxAttempts);
            if (_attempts == _gameConfig.gameround.attempts)
            {
                OnRoundFinish?.Invoke();
            }
        }

        public void StartKick()
        {
            _shotScore = 0;
            _targetScore = 0;
            _attempts++;
        }

        public void CompleteKick()
        {
            CalculateKick();
        }

        public void ScoreGoal(Vector3 position)
        {
            _shotScore += _gameConfig.gameround.basicPoints;
            OnGoalScoredAt?.Invoke(_gameConfig.gameround.basicPoints,position);
        }
        
        public void AddTarget(Vector3 position)
        {
            _targetScore += _gameConfig.gameround.targetPoints;
                
            int lastComboIndex = _gameConfig.gameround.comboBonusPoints.Length - 1;
            if (_combo > lastComboIndex)
            {
                _targetScore += _gameConfig.gameround.comboBonusPoints[lastComboIndex];
            }
            else
            {
                _targetScore += _gameConfig.gameround.comboBonusPoints[_combo];
            }

            OnHitTargetAt(_targetScore, position);
        }

        public void Clear()
        {
            _attempts = 0;
            _score = 0;
            _shotScore = 0;
            _targetScore = 0;
            _combo = 0;
        }
    }
}
