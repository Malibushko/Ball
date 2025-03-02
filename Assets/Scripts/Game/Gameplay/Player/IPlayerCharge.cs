using UniRx;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public interface IPlayerCharge
    {
        public IReadOnlyReactiveProperty<Vector3> ChargeStartPosition { get; }
        public IReadOnlyReactiveProperty<Vector3> ChargeEndPosition { get; }
        public IReadOnlyReactiveProperty<bool> IsCharging { get; }
    }
}