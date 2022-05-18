using System;
using VantasticKick.Config;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.Core
{
    public class GameRound : UiModel
    {
        public Action<int,int> OnAttemptsChanged;
        public Action<int,int> OnScoreChanged;
        public Action<int> OnCombo;
        public Action OnRoundFinish;

        private GameConfig _gameConfig;
        private int _attempts;
        private int _score;
        private int _combo;

        public GameRound(GameConfig config)
        {
            _gameConfig = config;
            _attempts = 0;
            _score = 0;
            _combo = 0;
        }

        public void RegisterShot(bool isOnTarget)
        {
            int shotScore = 0;
            if (isOnTarget)
            {
                shotScore += _gameConfig.gameround.basicPoints;
                
                int lastComboIndex = _gameConfig.gameround.comboBonusPoints.Length - 1;
                if (_combo > lastComboIndex)
                {
                    shotScore += _gameConfig.gameround.comboBonusPoints[lastComboIndex];
                }
                else
                {
                    shotScore += _gameConfig.gameround.comboBonusPoints[_combo];
                }
                _combo++;
            }
            else
            {
                _combo = 0;
            }

            _attempts++;
            _score += shotScore;
            
            OnAttemptsChanged?.Invoke(_attempts, _gameConfig.gameround.attempts);
            OnScoreChanged?.Invoke(_score, shotScore);
            OnCombo?.Invoke(_combo);

            if (_attempts == _gameConfig.gameround.attempts)
            {
                OnRoundFinish?.Invoke();
            }
        }
    }
}
