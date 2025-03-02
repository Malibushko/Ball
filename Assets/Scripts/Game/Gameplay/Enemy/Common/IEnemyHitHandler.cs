using Core.Infrastructure.Interfaces;
using UnityEngine;

namespace Game.Gameplay.Enemy.Common
{
    public interface IEnemyHitHandler : IConfigLoadable
    {
        public bool WasHit(Transform transform, Collision collision);
    }
}