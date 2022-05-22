using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VantasticKick.Core;
using Zenject;
using Random = UnityEngine.Random;

public class WorldUiManager : MonoBehaviour
{
    [SerializeField] private WorldUiInstance _instancePrfab;
    
    [Inject] private GameRoundController _gameRoundController;
    [Inject] private GameRoundModel _gameRoundModel;

    private float _randomOffsetFactor = 0.5f;
    private void Start()
    {
        _gameRoundController.OnKickStarted += SubscribeKickEvent;
        _gameRoundController.OnKickFinished += UnsubscribeKickEvent;
    }

    private void OnDestroy()
    {
        _gameRoundController.OnKickStarted -= SubscribeKickEvent;
        _gameRoundController.OnKickFinished -= UnsubscribeKickEvent;
    }

    private void SubscribeKickEvent()
    {
        _gameRoundModel.OnGoalScoredAt += OnScoreAt;
        _gameRoundModel.OnHitTargetAt += OnHitTartget;
    }

    private void UnsubscribeKickEvent()
    {
        _gameRoundModel.OnGoalScoredAt -= OnScoreAt;
        _gameRoundModel.OnHitTargetAt -= OnHitTartget;
    }

    private void OnScoreAt(int points, Vector3 position)
    {
        var randomOffset = new Vector3(Random.Range(-_randomOffsetFactor, _randomOffsetFactor),
            Random.Range(-_randomOffsetFactor, _randomOffsetFactor), -1f);
        var worldUiInstance = Instantiate(_instancePrfab, position+Vector3.up+randomOffset, Quaternion.identity);
        worldUiInstance.Init($"+{points}", 2f);
    }

    private void OnHitTartget(int points, Vector3 position)
    {
        var worldUiInstance = Instantiate(_instancePrfab, position+Vector3.up, Quaternion.identity);
        worldUiInstance.Init($"+{points}", 2f);
    }
}
