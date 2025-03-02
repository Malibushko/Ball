using Game.Common.Factories;
using Game.Common.Services.Ads;
using Game.Common.Services.Analytics;
using Game.Common.Services.Assets;
using Game.Common.Services.Configs;
using Game.Common.Services.Effects;
using Game.Common.Services.Level;
using Game.Common.Services.Loading;
using Game.Common.Services.Logging;
using Game.Common.Services.Scenes;
using Game.Common.Services.Text;
using Game.Common.Services.UI;
using Game.Configs;
using Game.Configs.UI;
using Game.GameConfigs;
using Game.GameStates;
using Game.Loading;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using PrefabInstantiator = Game.Common.Services.Scenes.PrefabInstantiator;

namespace Game.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gameRunnerPrefab;
        [SerializeField] private string _gameConfigPath;
        
        private GameConfig _gameConfig;
        
        public override void InstallBindings()
        {
            BindAssetsServices();
            BindConfigsService();
            BindConfigs();
            BindLogger();
            BindAds();
            BindStrings();
            BindSceneLoader();
            BindLevelsService();
            BindStateFactory();
            BindGameStateMachine();
            BindGameStates();
            BindLoadingScreen();
            BindUIFactory();
            BindEffectsFactory();
            BindAnalytics();
            RunGame();
        }

        private void BindAnalytics()
        {
           Container
                .BindInterfacesAndSelfTo<FirebaseAnalyticsService>()
                .AsSingle();
        }

        private void BindAds()
        {
            var configs = Container.Resolve<IConfigsService>();
            var adsConfig = configs.Load<AdsConfig>(_gameConfig.AdsConfig);
            
            Container.BindInterfacesAndSelfTo<AdsService>()
                .AsSingle()
                .WithArguments(adsConfig);
        }

        private void BindLogger()
        {
            Container.BindInterfacesAndSelfTo<ConsoleLoggingService>()
                .AsSingle();
        }

        private void BindEffectsFactory()
        {
            var configs = Container.Resolve<IConfigsService>();
            var memoryConfig = configs.Load<MemoryConfig>(_gameConfig.MemoryConfigPath);

            Container
                .BindInterfacesAndSelfTo<EffectsFactory>()
                .AsSingle()
                .WithArguments(memoryConfig.Effects);
        }

        private void BindStrings()
        {
            Container
                .BindInterfacesAndSelfTo<GameStringsService>()
                .AsSingle();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>()
                .AsSingle();
        }

        private void RunGame()
        {
            Container.InstantiatePrefab(_gameRunnerPrefab);
        }

        private void BindAssetsServices()
        {
            Container
                .BindInterfacesAndSelfTo<PrefabInstantiator>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ResourceAssetsLoader>()
                .AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>()
                .AsSingle();
        }

        private void BindLevelsService()
        {
            Container.Bind<ILevelsService>()
                .To<LevelsService>() 
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
        }

        private void BindConfigsService()
        {
            Container
                .Bind<IConfigsService>()
                .To<JsonConfigsService>()
                .AsSingle();
        }
        
        private void BindConfigs()
        {
            var configs = Container.Resolve<IConfigsService>();

            _gameConfig = configs.Load<GameConfig>(_gameConfigPath);
            Assert.IsNotNull(_gameConfig);

            Container
                .Bind<GameConfig>()
                .FromInstance(_gameConfig)
                .AsSingle();
        }
        
        private void BindGameStateMachine()
        {            
            Container
                .Bind<GameStateMachine>()
                .AsSingle();
        }
        
        private void BindLoadingScreen()
        {
            var configs = Container.Resolve<IConfigsService>();
            var rootConfig = Container.Resolve<GameConfig>();
            var uiConfig = configs.Load<UIConfig>(rootConfig.UIConfigPath);

            Container.BindInterfacesAndSelfTo<TimedLoadingProgressProvider>()
                .AsSingle()
                .WithArguments(uiConfig.LoadingScreenConfig.MinShowTime,
                    uiConfig.LoadingScreenConfig.StatusChangeDelay);
            
            Container
                .BindInterfacesAndSelfTo<LoadingScreen>()
                .AsSingle();
        }
        
        private void BindGameStates()
        {
            Container
                .Bind<GameBootstrapState>()
                .AsSingle();
            Container
                .Bind<GameLoadingState>()
                .AsSingle();
            Container
                .Bind<GameHubState>()
                .AsSingle();
            Container
                .Bind<GamePlayState>()
                .AsSingle();
        }
    }
}