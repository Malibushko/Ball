using UniRx;

namespace Game.Gameplay.Enemy
{
    public class EnemyState
    {
        public enum State
        {
            Idle,
            Active,
            Dying,
            Dead
        }

        public ReactiveProperty<State> CurrentState { get; } = new();
    }
}