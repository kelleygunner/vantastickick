using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        [SerializeField] private TargetTrigger centerTarget;
        
        private List<TargetSet> _targetSets;
        private Circle<TargetSet> _excludeCircle;

        [Inject] private DiContainer _container;

        private void Start()
        {
            //TODO: Create from config
            _targetSets = new List<TargetSet>()
            {
                new (topLeftTarget),//Top Left
                new (topRightTarget),//Top Right
                new (bottomLeftTarget),//Bottom Left
                new (bottomRightTarget),//Bottom Right
                new (topLeftTarget,topRightTarget),//Top
                new (bottomLeftTarget, bottomRightTarget),//Bottom
                new (topLeftTarget, bottomLeftTarget),//Left
                new (topRightTarget, bottomRightTarget),//Right
                new (centerTarget),//Center
                new (topLeftTarget,topRightTarget, centerTarget)//Top,Center
            };

            //Circle data structure to store last used sets to exclude it
            _excludeCircle = new Circle<TargetSet>(_targetSets.Count / 2);

            _container.Bind<TargetTrigger>().FromInstance(topLeftTarget);
            _container.Bind<TargetTrigger>().FromInstance(topRightTarget);
            _container.Bind<TargetTrigger>().FromInstance(bottomLeftTarget);
            _container.Bind<TargetTrigger>().FromInstance(bottomRightTarget);
            _container.Bind<TargetTrigger>().FromInstance(centerTarget);
            
            RemoveTargets();
        }

        public void ActivateNextTargetSet()
        {
            RemoveTargets();

            var elementToPick = _excludeCircle.ExcludeCircleFrom(_targetSets).ToList();
            var randomNumber = Random.Range(0, elementToPick.Count);
            var currentSet = elementToPick[randomNumber];
            currentSet.Activate();
            
            _excludeCircle.Add(currentSet);
        }

        public void RemoveTargets()
        {
            topLeftTarget.gameObject.SetActive(false);
            topRightTarget.gameObject.SetActive(false);
            bottomLeftTarget.gameObject.SetActive(false);
            bottomRightTarget.gameObject.SetActive(false);
            centerTarget.gameObject.SetActive(false);
        }
    }
    public class TargetSet
    {
        private TargetTrigger[] _targets;
        
        public TargetSet(params TargetTrigger[] targets)
        {
            _targets = targets;
        }

        public void Activate()
        {
            foreach (var target in _targets)
            {
                target.gameObject.SetActive(true);
                target.Open();
            }
        }
    }
}
