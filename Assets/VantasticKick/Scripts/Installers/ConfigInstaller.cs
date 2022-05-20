using UnityEngine;
using VantasticKick.Config;
using Zenject;

namespace VantasticKick.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameConfig = GameConfig.LoadAt("Config/game_config");
            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle().NonLazy();

            Application.targetFrameRate = 60;
        }
    }
}
