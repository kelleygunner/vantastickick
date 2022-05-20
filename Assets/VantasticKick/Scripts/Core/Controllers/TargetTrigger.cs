using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VantasticKick.Core;
using Zenject;

public class TargetTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    
    [Inject] private KickController _kickController;

    public void Open()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.25f);
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, 0.25f);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        _kickController.CompleteTarget();
        Close();
        _effect.Emit(50);
    }
}
