using UniRx;

namespace Game.Gameplay.Health
{
    public interface IHealthAttribute
    {
        public ReactiveProperty<int> Health { get; set; }
    }
}