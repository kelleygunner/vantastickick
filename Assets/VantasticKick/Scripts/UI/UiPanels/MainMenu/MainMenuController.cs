using UnityEngine;
using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.UI
{
    public class MainMenuController : UiController
    {

        [Inject] private GamePanelController _gamePanel;
        
        public MainMenuController(MainMenuView view, UiModel model = null) : base(view)
        {
            var mainMenuView = _view as MainMenuView;
            if (mainMenuView != null)
            {
                mainMenuView.OnPlayClick += OnPlayClick;
            }
            Debug.Log("Done");
        }

        ~MainMenuController()
        {
            var mainMenuView = _view as MainMenuView;
            if (mainMenuView != null)
            {
                mainMenuView.OnPlayClick += OnPlayClick;
            }
        }

        private void OnPlayClick()
        {
            Close();
            _gamePanel.Open();
        }
    }
}
