using System;

namespace VantasticKick.UI.UiFramework
{
    public abstract class UiController
    {
        protected UiView _view;
        protected UiModel _model;

        public UiController(UiView view)
        {
            _view = view;
        }
        
        public void Open(UiModel model = null, Action onComplete = null)
        {
            _view.Init(model);
            _view.Open(onComplete);
            OnOpen();
        }

        public void Close(Action onComplete = null)
        {
            _view.Close(onComplete);
            OnClose();
        }

        public virtual void OnOpen()
        {
            
        }

        public virtual void OnClose()
        {
            
        }
        
    }
}
