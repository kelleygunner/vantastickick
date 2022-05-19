using VantasticKick.Core.Input;
using Zenject;

namespace VantasticKick.Core
{
    public class GameRoundController
    {
        [Inject] private TargetController _targetController;
        [Inject] private IGameInput _input;
        [Inject] private KickController _kickController;
        
        public void StartKick()
        {
            SetTargets();
            _kickController.Reset();
            _input.OnStartTargeting += StartTargeting;
        }

        private void StartTargeting()
        {
            _kickController.Activate();
        }

        public void FinishKick()
        {
            _input.OnStartTargeting -= StartTargeting;
            _kickController.Deactivate();
        }
        
        private void SetTargets()
        {
            _targetController.ActivateNextTargetSet();
        }

        public void ResetKick()
        {
            StartKick();
        }
    }
}
