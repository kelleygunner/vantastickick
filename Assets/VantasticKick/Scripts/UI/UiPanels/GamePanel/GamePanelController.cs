using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.UI
{
    public class GamePanelController : UiController
    {
        [Inject] private MainMenuController _mainMenuController;
        
        public GamePanelController(GamePanelView view) : base(view)
        {
            
        }

        public override void OnOpen()
        {
            if (_view is GamePanelView gamePanelView)
            {
                gamePanelView.OnGamePanelHidden += OnGamePanelHidden;
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
