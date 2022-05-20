using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.UI
{
    public class FinishScreenController : UiController
    {
        [Inject] private MainMenuController _mainMenuController;
        public FinishScreenController(FinishScreenView view) : base(view)
        {
            
        }

        public override void OnOpen()
        {
            if (_view is FinishScreenView view)
            {
                view.OnFinishButtonClick += OnFinishButtonClick;
            }
        }

        public override void OnClose()
        {
            if (_view is FinishScreenView view)
            {
                view.OnFinishButtonClick -= OnFinishButtonClick;
            }
        }

        private void OnFinishButtonClick()
        {
            Close();
            _mainMenuController.Open();
        }
    }
}
