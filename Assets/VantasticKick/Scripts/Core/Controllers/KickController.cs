using System.Collections;
using UnityEngine;
using VantasticKick.Config;
using VantasticKick.Core.Input;
using Zenject;

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
            _input.OnTarget += OnTarget;
            _input.OnTargetRelease += OnTargetingRelease;
        }

        public void DeactivateTargeting()
        {
            StopAllCoroutines();
            _direction.gameObject.SetActive(false);
            _input.OnTarget -= OnTarget;
            _input.OnTargetRelease -= OnTargetingRelease;
        }

        private void OnTarget(Vector3 value)
        {
            float xAngle = 20 + value.y * 15;
            float yAngle = value.x * 45;
            
            _direction.rotation = Quaternion.Euler(-1*xAngle, yAngle,0);
        }

        private void OnTargetingRelease()
        {
            var velocity = _direction.forward * _config.gameplay.ballVelocity;
            var xScatter = Random.Range(-_config.gameplay.scatterFactor, _config.gameplay.scatterFactor);
            var yScatter = Random.Range(-_config.gameplay.scatterFactor, _config.gameplay.scatterFactor);
            velocity += new Vector3(xScatter, yScatter, 0);
            _ball.velocity = velocity;
            
            _gameRoundController.FinishTargeting();
            StartCoroutine(WaitForFail());
        }

        public void CompleteTarget()
        {
            StopAllCoroutines();
            StartCoroutine(FinishKick());
            gameRoundModel.RegisterShot(true);
        }

        private IEnumerator WaitForFail()
        {
            yield return new WaitForSeconds(3f);
            gameRoundModel.RegisterShot(false);
            _gameRoundController.FinishKick();
        }

        private IEnumerator FinishKick()
        {
            yield return new WaitForSeconds(1f);
            _gameRoundController.FinishKick();
        }
    }
}
