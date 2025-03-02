using Core.Input;
using Game.Common.Factories;
using Game.Common.Services.Assets;
using Game.Common.Services.Camera;
using Game.Common.Services.Level;
using Game.Common.Services.Physics;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Player;
using Game.Gameplay.Services.Collectable;
using Game.Gameplay.Services.Enemy;
using Game.Gameplay.Services.Level;
using Game.Gameplay.Spawn;
using Game.Gameplay.States;
using Game.Gameplay.UI;
using UnityEngine;
using Zenject;
using PrefabInstantiator = Game.Common.Services.Scenes.PrefabInstantiator;

namespace Game.Gameplay
{
    public class LevelInstaller : MonoInstaller
    { 
        [Header("Camera settings")]
        [SerializeField] public Camera _mainCamera;  
        
        private LevelConfig _levelConfig;
        
        public override void InstallBindings()
        {
            BindInput();
            BindPhysics();
            BindCamera();
            BindPrefabsInstantiator();
            BindLevelConfig();
            BindLevelStateMachine();
            BindLevelStates();
            BindStateFactory();
            BindLevelService();
            BindPlayer();
            BindCollectables();
            BindEnemyFactory();
            BindUI();
            BindSpawners();
            BindLevelBuilder();
        }

        private void BindUI()
        {
            Container
                .BindInterfacesAndSelfTo<LevelDataViewModel>()
                .AsSingle();
        }
        
        private void BindLevelService()
        {
            Container.BindInterfacesAndSelfTo<LevelDataService>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<LevelService>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<LevelProgressWatcherService>()
                .AsSingle();
        }
        
        private void BindSpawners()
        {
            Container
                .Bind<EnemySpawner>()
                .AsSingle();
            Container
                .Bind<CollectablesSpawner>()
                .AsSingle();
            Container
                .Bind<LevelZonesSpawner>()
                .AsSingle();
        }
        
        private void BindLevelBuilder()
        {
            Container
                .Bind<ILevelBuilder>()
                .To<LevelBuilder>()
                .AsSingle();
        }

        private void BindCollectables()
        {
            Container
                .Bind<Collectable.Collectable>()
                .AsTransient();
            
            Container
                .Bind<ICollectableFactory>()
                .To<CollectableFactory>()
                .AsSingle();
        }
        
        private void BindEnemyFactory()
        {
            Container.Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }
        
        private void BindInput()
        {
            Container
                .BindInterfacesAndSelfTo<PointerInputService>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardInputService>()
                .AsSingle();
        }

        private void BindPhysics()
        {
            Container.Bind<IPhysicsObject>()
                .To<PhysicsObject>()
                .AsTransient();
            
            Container
                .BindInterfacesAndSelfTo<PhysicsService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPrefabsInstantiator()
        {
            Container
                .BindInterfacesAndSelfTo<PrefabInstantiator>()
                .AsSingle();
        }
        
        private void BindLevelConfig()
        {
            var levelsService = Container.Resolve<ILevelsService>();
            _levelConfig = levelsService.GetCurrentLevelConfig();
            
            Container
                .BindInstance(_levelConfig)
                .AsSingle();
        }
        
        private void BindLevelStateMachine()
        {
            Container
                .Bind<LevelStateMachine>()
                .AsSingle();
        }
        
        private void BindLevelStates()
        {
            Container
                .Bind<LevelLoadingState>()
                .AsSingle();
            Container
                .Bind<LevelPlayState>()
                .AsSingle();
            Container
                .Bind<LevelWinState>()
                .AsSingle();
            Container.Bind<LevelLoseState>()
                .AsSingle();
        }
        
        private void BindStateFactory()
        {
            Container.Bind<StateFactory>()
                .AsSingle();
        }
        
        private void BindPlayer()
        {
            if (_levelConfig != null)
            {
                var assetsService = Container.Resolve<IAssetsService>();
                var player =
                    Container.InstantiatePrefabForComponent<PlayerView>(
                        assetsService.Load<GameObject>(_levelConfig.PlayerConfig.PrefabPath));

                Container
                    .Bind<PlayerView>()
                    .FromInstance(player)
                    .AsSingle();
            }
        }

        private void BindCamera()
        {
            Container
                .BindInterfacesAndSelfTo<CameraService>()
                .AsSingle()
                .WithArguments(_mainCamera);
        }
    }
}