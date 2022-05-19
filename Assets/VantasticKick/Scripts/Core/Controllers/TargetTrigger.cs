using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VantasticKick.Core;
using Zenject;

public class TargetTrigger : MonoBehaviour
{
    [Inject] private KickController _kickController;
    private void OnCollisionEnter(Collision collision)
    {
        _kickController.CompleteTarget();
    }
}
