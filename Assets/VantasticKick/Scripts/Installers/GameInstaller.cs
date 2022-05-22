using UnityEngine;
using VantasticKick.Core;
using VantasticKick.Core.Audio;
using VantasticKick.Core.Input;
using Zenject;

namespace VantasticKick.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private TargetController _targetController;
        [SerializeField] private KickController _kickController;
        [SerializeField] private MobileInput _input;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GoalTrigger goalTrigger;
        [SerializeField] private WorldUiManager _worldUiManager;
        public override void InstallBindings()
        {
            Container.Bind<TargetController>().FromInstance(_targetController).AsSingle().NonLazy();
            Container.Bind<GameRoundController>().FromNew().AsSingle().NonLazy();
            Container.Bind<KickController>().FromInstance(_kickController).AsSingle().NonLazy();
            Container.Bind<IGameInput>().To<MobileInput>().FromInstance(_input).AsSingle().NonLazy();
            Container.Bind<GameRoundModel>().FromNew().AsSingle().NonLazy();
            Container.Bind<IAudioManager>().To<AudioManager>().FromInstance(_audioManager).AsSingle().NonLazy();
            Container.Bind<GoalTrigger>().FromInstance(goalTrigger).AsSingle().NonLazy();
            Container.Bind<WorldUiManager>().FromInstance(_worldUiManager).AsSingle().NonLazy();
        }
    }
}
