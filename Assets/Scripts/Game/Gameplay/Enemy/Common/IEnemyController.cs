using Core.Infrastructure.Interfaces;

namespace Game.Gameplay.Enemy.Common
{
    public interface IEnemyController : ITransformer, IConfigLoadable, IActivatableService
    {
        
    }
}