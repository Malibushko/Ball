using Game.Common.Services.Physics;
using Game.Configs;
using Game.GameConfigs;
using Zenject;

namespace Game.GameHub.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var hubConfig = Container.Resolve<GameHubConfig>();
            var playerConfig = hubConfig.PlayerConfig;
            
            Container.BindInterfacesAndSelfTo<PhysicsObject>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerRandomMover>()
                .AsSingle()
                .WithArguments(playerConfig.MoveConfig);
        }
    }
}