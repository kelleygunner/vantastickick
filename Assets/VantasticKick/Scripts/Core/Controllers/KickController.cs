using System.Collections;
using UnityEngine;
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
        [Inject] private GameRound _gameRound;
        [Inject] private GameRoundController _gameRoundController;
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
        
        public void Activate()
        {
            _direction.gameObject.SetActive(true);
            _input.OnTarget += OnTarget;
            _input.OnTargetRelease += OnTargetingRelease;
        }

        public void Deactivate()
        {
            _direction.gameObject.SetActive(false);
            _input.OnTarget -= OnTarget;
            _input.OnTargetRelease -= OnTargetingRelease;
        }

        private void OnTarget(Vector3 value)
        {
            float xAngle = 20 - value.y * 15;
            float yAngle = value.x * 45;
            
            _direction.rotation = Quaternion.Euler(-1 * xAngle, yAngle,0);
        }

        private void OnTargetingRelease()
        {
            _ball.velocity = _direction.forward * 12f;
            _gameRoundController.FinishKick();
            StartCoroutine(WaitForFail());
        }

        public void CompleteTarget()
        {
            StopAllCoroutines();
            StartCoroutine(FinishKick());
            _gameRound.RegisterShot(true);
        }

        private IEnumerator WaitForFail()
        {
            yield return new WaitForSeconds(3f);
            _gameRound.RegisterShot(false);
            _gameRoundController.ResetKick();
        }

        private IEnumerator FinishKick()
        {
            yield return new WaitForSeconds(1f);
            _gameRoundController.ResetKick();
        }
    }
}
