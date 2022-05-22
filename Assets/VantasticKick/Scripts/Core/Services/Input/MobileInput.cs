using System;
using UnityEngine;

namespace VantasticKick.Core.Input
{
    public class MobileInput : MonoBehaviour, IGameInput
    {
        public Action<Vector3> OnTarget { get; set; }
        public Action OnStartTargeting { get; set; }
        public Action OnTargetRelease { get; set; }

        private Vector3 _startPosition;

        private float _sensity = 15f;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _startPosition = UnityEngine.Input.mousePosition;
                OnStartTargeting?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                var value = _sensity * (UnityEngine.Input.mousePosition - _startPosition)/Screen.width;
                value.x = Mathf.Clamp(value.x, -1f, 1f);
                value.y = Mathf.Clamp(value.y, 0, 1f);
                OnTarget?.Invoke(value);
            }
            
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnTargetRelease?.Invoke();
            }
        }
    }
}
