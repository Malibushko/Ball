using Game.Gameplay.Enemy.Common;
using Game.Gameplay.Enemy.WalkingEnemy;
using Zenject;

namespace Game.Gameplay.Enemy
{
    public class WalkingEnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyPerpendicularHitHandler>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<WalkingEnemyController>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyState>()
                .AsSingle();
        }
    }
}