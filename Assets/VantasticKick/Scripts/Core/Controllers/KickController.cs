using System;
using System.Collections;
using UnityEngine;
using VantasticKick.Config;
using VantasticKick.Core.Audio;
using VantasticKick.Core.Input;
using Zenject;
using Random = UnityEngine.Random;

namespace VantasticKick.Core
{
    public class KickController : MonoBehaviour
    {
        [SerializeField] private Transform _direction;
        [SerializeField] private Rigidbody _ball;
        [SerializeField] private Transform _ballOriginPosition;

        [Inject] private IGameInput _input;
        [Inject] private GameRoundModel gameRoundModel;
        [Inject] private GameRoundController _gameRoundController;
        [Inject] private GameConfig _config;
        [Inject] private IAudioManager _audioManager;
        void Start()
        {
            _direction.gameObject.SetActive(false);
        }
        
        public void Reset()
        {
            _ball.transform.position = _ballOriginPosition.transform.position;
            _ball.velocity = Vector3.zero;
            _ball.angularVelocity = Vector3.zero;
        }
        
        public void ActivateTargeting()
        {
            _direction.gameObject.SetActive(true);
            _input.OnTarget += OnTargeting;
            _input.OnTargetRelease += OnTargetingRelease;
        }

        public void DeactivateTargeting()
        {
            StopAllCoroutines();
            _direction.gameObject.SetActive(false);
            _input.OnTarget -= OnTargeting;
            _input.OnTargetRelease -= OnTargetingRelease;
        }

        private void OnTargeting(Vector3 valueFromInput)
        {
            _direction.rotation = BallDirector.GetBallDirectionFromInput(valueFromInput);
        }

        private void OnTargetingRelease()
        {
            BallDirector.SetBallVelocity(_ball, _direction.forward, _config.gameplay);
            _gameRoundController.FinishTargeting();
            _audioManager.PlayClip(AudioClipType.Kick);
            
            gameRoundModel.StartKick();
            StartCoroutine(WaitForFail());
        }

        public void HitTarget(Vector3 position)
        {
            StopAllCoroutines();
            StartCoroutine(FinishKick());
            gameRoundModel.AddTarget(position);
        }

        private IEnumerator WaitForFail()
        {
            yield return new WaitForSeconds(3f);
            _gameRoundController.FinishKick();
        }

        private IEnumerator FinishKick()
        {
            yield return new WaitForSeconds(1f);
            _gameRoundController.FinishKick();
        }
    }
}
