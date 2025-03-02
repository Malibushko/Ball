using System;
using Game.Gameplay.Health;
using Game.Gameplay.Services.Level;
using UniRx;
using Zenject;

namespace Game.Gameplay.Player
{
    public class PlayerHealthController : IInitializable, IDisposable
    {
        private IHealthAttribute _healthAttribute;
        private ILevelProgressWatcherService _levelProgressWatcherService;
        
        private IDisposable _healthSubscription;
        
        public PlayerHealthController(IHealthAttribute healthAttribute, ILevelProgressWatcherService levelProgressWatcherService)
        {
            _healthAttribute = healthAttribute;
            _levelProgressWatcherService = levelProgressWatcherService;
        }
        
        public void Initialize()
        {
            _healthSubscription = _healthAttribute.Health.Subscribe(OnHealthChanged);
            OnHealthChanged(_healthAttribute.Health.Value);
        }

        private void OnHealthChanged(int health)
        {
            _levelProgressWatcherService.OnPlayerHealthChanged(health);
        }

        public void Dispose()
        {
            _healthSubscription?.Dispose();
        }
    }
}