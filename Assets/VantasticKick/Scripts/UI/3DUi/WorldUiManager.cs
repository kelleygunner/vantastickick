using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VantasticKick.Core;
using VantasticKick.Utils.ObjectPool;
using Zenject;
using Random = UnityEngine.Random;

public class WorldUiManager : MonoBehaviour
{
    [SerializeField] private WorldUiInstance[] _worldUiInstances;
    private ObjectPool<WorldUiInstance> _pool;

    [Inject] private GameRoundController _gameRoundController;
    [Inject] private GameRoundModel _gameRoundModel;

    private float _randomOffsetFactor = 0.25f;
    private void Start()
    {
        _pool = new ObjectPool<WorldUiInstance>(_worldUiInstances);
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
        _gameRoundModel.OnCombo += OnCombo;
    }

    private void UnsubscribeKickEvent()
    {
        _gameRoundModel.OnGoalScoredAt -= OnScoreAt;
        _gameRoundModel.OnHitTargetAt -= OnHitTartget;
        _gameRoundModel.OnCombo -= OnCombo;
    }
    
    private void OnCombo(int combo)
    {
        if (combo > 1)
        {
            DisplayWorldText($"Combo x{combo}",new Vector3(-0.5f,2f,-1f));   
        }
    }

    private void OnScoreAt(int points, Vector3 position)
    {
        var randomOffset = new Vector3(Random.Range(-_randomOffsetFactor, _randomOffsetFactor),0, -1f);
        DisplayWorldText($"+{points}",position + randomOffset + Vector3.up * 0.25f);
    }

    private void OnHitTartget(int points, Vector3 position)
    {
        var randomOffset = new Vector3(Random.Range(-_randomOffsetFactor, _randomOffsetFactor), 0, -1f);
        
        DisplayWorldText($"+{points}",position + randomOffset + Vector3.up * 1f);
    }

    private void DisplayWorldText(string text, Vector3 position)
    {
        var worldUiInstance = _pool.Pull();
        worldUiInstance.gameObject.SetActive(true);
        worldUiInstance.transform.position = position;
        worldUiInstance.Init(text,(t) =>
        {
            worldUiInstance.gameObject.SetActive(false);
            _pool.Push(t);
        });
    }
}
