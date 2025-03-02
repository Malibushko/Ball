using Game.Common.Factories;
using Game.Common.Services.Camera;
using Game.Common.Services.Configs;
using Game.Common.Services.Physics;
using Game.GameConfigs;
using Game.GameHub.States;
using UnityEngine;
using Zenject;
using PrefabInstantiator = Game.Common.Services.Scenes.PrefabInstantiator;

namespace Game.GameHub
{
    public class GameHubInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera _camera;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindPhysics();
            BindPrefabsInstantiator();
            BindStates();
            BindCamera();
        }

        private void BindConfig()
        {
            var gameConfig = Container.Resolve<GameConfig>();
            var configsLoader = Container.Resolve<IConfigsService>();
            
            var config = configsLoader.Load<GameHubConfig>(gameConfig.GameHubConfigPath);

            Container.Bind<GameHubConfig>()
                .FromInstance(config)
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
        
        private void BindCamera()
        {
            Container
                .BindInterfacesAndSelfTo<CameraService>()
                .AsSingle()
                .WithArguments(_camera);
        }

        private void BindPrefabsInstantiator()
        {
            Container
                .BindInterfacesAndSelfTo<PrefabInstantiator>()
                .AsSingle();
        }
        
        private void BindStates()
        {         
            Container.Bind<StateFactory>()
                .AsSingle();
            Container
                .Bind<GameHubLoadingState>()
                .AsSingle();
            Container
                .Bind<GameHubActiveState>()
                .AsSingle();
            Container
                .Bind<GameHubStateMachine>()
                .AsSingle();
        }
    }
}