using System;

namespace VantasticKick.UI.UiFramework
{
    public abstract class UiController
    {
        protected UiView _view;

        public UiController(UiView view)
        {
            _view = view;
        }
        
        public void Open(UiModel model = null, Action onComplete = null)
        {
            _view.Init(model);
            _view.Open(onComplete);
        }

        public void Close(Action onComplete = null)
        {
            _view.Close(onComplete);
        }

    }
}
