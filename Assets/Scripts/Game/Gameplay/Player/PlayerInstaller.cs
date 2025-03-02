using Game.Common.Services.Physics;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Health;
using Game.Gameplay.Input;
using Zenject;

namespace Game.Gameplay.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var levelConfig = Container.Resolve<LevelConfig>();
            var playerConfig = levelConfig.PlayerConfig;
            
            Container
                .BindInterfacesAndSelfTo<PhysicsObject>()
                .AsSingle();
#if PLAYER_INPUT_TOUCH || PLAYER_INPUT_MOUSE
            Container
                .BindInterfacesAndSelfTo<PlayerPointerInputService>()
                .AsSingle()
                .WithArguments(playerConfig.PlayerInputConfig.Pointer);
#elif PLAYER_INPUT_KEYBOARD
            Container
                .BindInterfacesAndSelfTo<PlayerKeyboardInputService>()
                .AsSingle()
                .WithArguments(playerConfig.PlayerInputConfig.Keyboard);
#endif
            Container.BindInterfacesAndSelfTo<PlayerController>()
                .AsSingle()
                .WithArguments(playerConfig.PlayerControllerConfig);
            
            Container.Bind<IHealthAttribute>()
                .To<HealthAttribute>()
                .AsSingle()
                .WithArguments(playerConfig.Health);
            
            Container.BindInterfacesAndSelfTo<PlayerHealthController>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<Player>()
                .AsSingle();
        }
    }
}