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
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameRound _gameRound;
        
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
            _gameRound.Clear();
            _gamePanel.Open(_gameRound);
        }
    }
}
