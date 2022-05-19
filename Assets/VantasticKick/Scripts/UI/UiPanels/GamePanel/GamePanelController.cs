using VantasticKick.Core;
using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.UI
{
    public class GamePanelController : UiController
    {
        [Inject] private MainMenuController _mainMenuController;
        [Inject] private GameRoundController _gameRoundController;
        
        public GamePanelController(GamePanelView view) : base(view)
        {
            
        }

        public override void OnOpen()
        {
            if (_view is GamePanelView gamePanelView)
            {
                gamePanelView.OnGamePanelHidden += OnGamePanelHidden;
                gamePanelView.Countdown.StartCount(3, () =>
                {
                    //Start Round
                    _gameRoundController.StartKick();
                });
            }
        }
        
        public override void OnClose()
        {
            if (_view is GamePanelView gamePanelView)
            {
                gamePanelView.OnGamePanelHidden -= OnGamePanelHidden;
            }
        }

        private void OnGamePanelHidden()
        {
            Close(() =>
            {
                _mainMenuController.Open();
            });
        }
    }
}
