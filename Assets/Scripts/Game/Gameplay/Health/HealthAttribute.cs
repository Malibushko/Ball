using UniRx;

namespace Game.Gameplay.Health
{
    public class HealthAttribute : IHealthAttribute
    {
        public ReactiveProperty<int> Health { get; set; } = new();

        public HealthAttribute(int health)
        {
            Health.Value = health;
        }
    }
}