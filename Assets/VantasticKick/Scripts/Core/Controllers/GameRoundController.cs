using VantasticKick.Core.Audio;
using VantasticKick.Core.Input;
using VantasticKick.UI;
using Zenject;
using System;

namespace VantasticKick.Core
{
    public class GameRoundController
    {
        public Action OnKickStarted;
        public Action OnKickFinished;
        
        [Inject] private TargetController _targetController;
        [Inject] private IGameInput _input;
        [Inject] private KickController _kickController;
        [Inject] private GameRoundModel _gameRoundModel;
        [Inject] private FinishScreenController _finishScreenController;
        [Inject] private IAudioManager _audioManager;

        public void StartRound()
        {
            _gameRoundModel.Clear();
        }

        public void FinishRound()
        {
            _kickController.DeactivateTargeting();
            _kickController.Reset();
            _targetController.RemoveTargets();
            
            var model = new FinishScreenModel(_gameRoundModel);
            _finishScreenController.Open(model);
        }
        
        public void StartKick()
        {
            SetTargets();
            _kickController.Reset();
            _input.OnStartTargeting += StartTargeting;
            _audioManager.PlayClip(AudioClipType.Whistle);
            OnKickStarted?.Invoke();
        }
        
        public void FinishKick()
        {
            _gameRoundModel.CompleteKick();
            OnKickFinished?.Invoke();
            if (_gameRoundModel.Attempts == _gameRoundModel.MaxAttempts)
            {
                FinishRound();
            }
            else
            {
                StartKick();
            }
        }

        public void StartTargeting()
        {
            _kickController.ActivateTargeting();
        }
        
        public void FinishTargeting()
        {
            _input.OnStartTargeting -= StartTargeting;
            _kickController.DeactivateTargeting();
        }
        
        private void SetTargets()
        {
            _targetController.ActivateNextTargetSet();
        }
    }
}
