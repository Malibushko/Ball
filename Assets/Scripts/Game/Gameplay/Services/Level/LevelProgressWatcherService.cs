using System.Collections.Generic;

namespace Game.Gameplay.Services.Level
{
    public class LevelProgressWatcherService : ILevelProgressWatcherService
    {
        private ILevelDataService _levelDataService;
        private ILevelService _levelService;
        
        public LevelProgressWatcherService(ILevelDataService levelDataService, ILevelService levelService)
        {
            _levelDataService = levelDataService;
            _levelService = levelService;
        }
        
        public void OnPlayerHealthChanged(int playerHealth)
        {
            _levelDataService.PlayerHealth.Value = playerHealth;
        }

        public void OnResourceCollected(Resource.Resource resource, int amount)
        {
            if (!_levelDataService.Resources.TryAdd(resource, amount))
                _levelDataService.Resources[resource] += amount;
        }

        public void OnWinZoneReached()
        {
            _levelService.Finish(LevelResult.Win);
        }
    }
}