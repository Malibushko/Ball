using UnityEngine;
using Zenject;

namespace Game.Gameplay.Health
{
    public class HealthComponent : MonoBehaviour
    {
        private IHealthAttribute _health;

        [Inject]
        public void Construct(IHealthAttribute health)
        {
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            _health.Health.Value = Mathf.Max(_health.Health.Value - damage, 0);
        }
    }
}