using UnityEngine;
using VantasticKick.Config;
using VantasticKick.Core;
using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.UI
{
    public class MainMenuController : UiController
    {

        [Inject] private GamePanelController _gamePanel;
        [Inject] private GameRoundModel _gameRoundModel;
        [Inject] private GameRoundController _gameRoundController;
        
        public MainMenuController(MainMenuView view) : base(view)
        {
            
        }

        public override void OnOpen()
        {
            var mainMenuView = _view as MainMenuView;
            if (mainMenuView != null)
            {
                mainMenuView.OnPlayClick += OnPlayClick;
            }
        }

        public override void OnClose()
        {
            var mainMenuView = _view as MainMenuView;
            if (mainMenuView != null)
            {
                mainMenuView.OnPlayClick -= OnPlayClick;
            }
        }

        private void OnPlayClick()
        {
            Close();
            _gameRoundController.StartRound();
            _gamePanel.Open(_gameRoundModel);
        }
    }
}
