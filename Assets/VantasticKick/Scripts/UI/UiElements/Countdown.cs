using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace VantasticKick.UI
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        
        public void StartCount(int seconds, Action onComplete)
        {
            StartCoroutine(Count(seconds, onComplete));
        }

        private IEnumerator Count(int seconds, Action onComplete)
        {
            while (seconds > 0)
            {
                _counterText.text = $"{seconds}";
                _counterText.transform.localPosition = Vector3.right * -100;
                _counterText.DOFade(1, 0);
                _counterText.transform.DOLocalMove(Vector3.zero, 0.33f).SetEase(Ease.OutBack);
                _counterText.DOFade(0, 0.4f).SetEase(Ease.InBack);
                yield return new WaitForSeconds(1);
                seconds--;
            }
            onComplete?.Invoke();
        }
    }
}
