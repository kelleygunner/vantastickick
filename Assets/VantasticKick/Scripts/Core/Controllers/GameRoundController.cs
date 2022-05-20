using VantasticKick.Core.Input;
using VantasticKick.UI;
using Zenject;

namespace VantasticKick.Core
{
    public class GameRoundController
    {
        [Inject] private TargetController _targetController;
        [Inject] private IGameInput _input;
        [Inject] private KickController _kickController;
        [Inject] private GameRoundModel _gameRoundModel;
        [Inject] private FinishScreenController _finishScreenController;

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
        }
        
        public void FinishKick()
        {
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
