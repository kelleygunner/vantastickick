using System;

namespace VantasticKick.UI.UiFramework
{
    public abstract class UiController
    {
        private UiView _view;

        public UiController(UiView view, UiModel model = null)
        {
            _view = view;
            _view.Init(model);
        }
        
        public void Open(Action onComplete = null)
        {
            _view.Open(onComplete);
        }

        public void Close(Action onComplete = null)
        {
            _view.Close(onComplete);
        }

    }
}
