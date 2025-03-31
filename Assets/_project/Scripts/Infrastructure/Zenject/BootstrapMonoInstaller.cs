using CodeBase.Core;
using CodeBase.Core.Factory;
using CodeBase.Core.Ui;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Servises.Data;
using CodeBase.Servises.Input;
using CodeBase.States;
using Zenject;

namespace CodeBase.Installers
{
    public class BootstrapMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveLoadService();
            BindLocalizationService();
            BindPersistentProgress();
            BindCore();
            BindUi();
            BindInputService();
            BindAssetProvider();
            BindConfigServices();
            BindFactory();
            BindGameStateMachine();
        }

        private void BindConfigServices()
        {
            Container.Bind<IEnemyDataService>().To<EnemyDataService>().AsSingle().NonLazy();
            Container.Resolve<IEnemyDataService>().LoadEnemiesConfigs();
            Container.Bind<ILevelsDataService>().To<LevelsDataService>().AsSingle().NonLazy();
            Container.Resolve<ILevelsDataService>().LoadLevelsConfigs();
        }

        private void BindPersistentProgress()
        {
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle().NonLazy();
        }

        private void BindSaveLoadService() =>
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle().NonLazy();
        
        private void BindLocalizationService() =>
            Container.Bind<IInterfaceLocalizationService>().To<InterfaceLocalizationService>().AsSingle().NonLazy();

        private void BindAssetProvider() =>
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle().NonLazy();

        private void BindCore()
        {
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();
        }

        private void BindUi()
        {
            Container.Bind<UiWindows>().FromNew().AsSingle().NonLazy();
        }

        private void BindInputService()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
#else
        Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
#endif
        }

        private void BindFactory() =>
            Container.Bind<PrefabFactory>().AsSingle().NonLazy();

        private void BindGameStateMachine() =>
            Container.Bind<GameStateMachine>().FromNew().AsSingle().NonLazy();
    }
}