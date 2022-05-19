using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VantasticKick.Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace VantasticKick.Core
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private TargetTrigger topLeftTarget;
        [SerializeField] private TargetTrigger topRightTarget;
        [SerializeField] private TargetTrigger bottomLeftTarget;
        [SerializeField] private TargetTrigger bottomRightTarget;

        private List<TargetSet> _targetSets;
        private Circle<TargetSet> _excludeCircle;

        [Inject] private DiContainer _container;

        private void Start()
        {
            //TODO: Create from config
            _targetSets = new List<TargetSet>()
            {
                new (topLeftTarget.gameObject),//Top Left
                new (topRightTarget.gameObject),//Top Right
                new (bottomLeftTarget.gameObject),//Bottom Left
                new (bottomRightTarget.gameObject),//Bottom Right
                new (topLeftTarget.gameObject,topRightTarget.gameObject),//Top ones
                new (bottomLeftTarget.gameObject, bottomRightTarget.gameObject),//Bottom ones
                new (topLeftTarget.gameObject, bottomLeftTarget.gameObject),//Left ones
                new (topRightTarget.gameObject, bottomRightTarget.gameObject),//Right ones
            };

            _excludeCircle = new Circle<TargetSet>(_targetSets.Count / 2);

            _container.Bind<TargetTrigger>().FromInstance(topLeftTarget);
            _container.Bind<TargetTrigger>().FromInstance(topRightTarget);
            _container.Bind<TargetTrigger>().FromInstance(bottomLeftTarget);
            _container.Bind<TargetTrigger>().FromInstance(bottomRightTarget);
        }

        public void ActivateNextTargetSet()
        {
            topLeftTarget.gameObject.SetActive(false);
            topRightTarget.gameObject.SetActive(false);
            bottomLeftTarget.gameObject.SetActive(false);
            bottomRightTarget.gameObject.SetActive(false);

            var elementToPick = _excludeCircle.ExcludeCircleFrom(_targetSets).ToList();
            var randomNumber = Random.Range(0, elementToPick.Count);
            var currentSet = elementToPick[randomNumber];
            currentSet.Activate();
            
            _excludeCircle.Add(currentSet);
        }
    }
    public class TargetSet
    {
        private GameObject[] _targets;
        
        public TargetSet(params GameObject[] targets)
        {
            _targets = targets;
        }

        public void Activate()
        {
            foreach (var target in _targets)
            {
                target.SetActive(true);
            }
        }
    }
}
