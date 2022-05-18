using UnityEngine;
using VantasticKick.UI;
using VantasticKick.UI.UiFramework;
using Zenject;

namespace VantasticKick.Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenu;
        [SerializeField] private GamePanelView _gamePanel;
        [SerializeField] private FinishScreenView _finishScreen;
        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromInstance(_mainMenu).AsSingle().NonLazy();
            Container.Bind<GamePanelView>().FromInstance(_gamePanel).AsSingle().NonLazy();
            Container.Bind<FinishScreenView>().FromInstance(_finishScreen).AsSingle().NonLazy();

            Container.Bind<MainMenuController>().FromNew().AsSingle().NonLazy();
            Container.Bind<GamePanelController>().FromNew().AsSingle().NonLazy();
            Container.Bind<FinishScreenModel>().FromNew().AsSingle().NonLazy();
        }

        public override void Start()
        {
            Container.Resolve<MainMenuController>().Open();
        }
    }
}
