using System;
using UnityEngine;
using Zenject;

namespace VantasticKick.Core
{
    public class GoalTrigger : MonoBehaviour
    {
        [Inject] private GameRoundModel _model;
        [Inject] private GameRoundController _gameRoundController;

        private bool _isTriggerEnabled;
        
        private void Start()
        {
            _gameRoundController.OnKickStarted += ActivateTrigger;
            _gameRoundController.OnKickFinished += DeactivateTrigger;
        }

        private void OnDestroy()
        {
            _gameRoundController.OnKickStarted -= ActivateTrigger;
            _gameRoundController.OnKickFinished -= DeactivateTrigger;   
        }

        private void ActivateTrigger()
        {
            _isTriggerEnabled = true;
        }

        private void DeactivateTrigger()
        {
            _isTriggerEnabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isTriggerEnabled)
            {
                return;
            }
            _isTriggerEnabled = false;
            _model.ScoreGoal(other.transform.position);
        }
    }
}
