using System;
using UnityEngine;

namespace VantasticKick.UI.UiFramework
{
    public abstract class UiView : MonoBehaviour
    {
        private UiModel _model;
        
        public void Init(UiModel model = null)
        {
            _model = model;
        }
        public void Open(Action onComplete)
        {
            OnOpen(onComplete);
        }

        public void Close(Action onComplete)
        {
            OnClose(onComplete);
        }
        
        protected virtual void OnOpen(Action onComplete = null)
        {
            gameObject.SetActive(true);
            onComplete?.Invoke();
        }

        protected virtual void OnClose(Action onComplete = null)
        {
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }
    }
}
