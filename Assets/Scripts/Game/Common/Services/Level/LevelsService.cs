using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Analytics;
using Game.Common.Services.Configs;
using Game.GameConfigs;
using Game.Gameplay.Configs.Level;
using Zenject;

namespace Game.Common.Services.Level
{
    public class LevelsService : ILevelsService, IInitializable
    {
        const string LevelIndexPropertyId = "level";
        
        private IConfigsService _configs;
        private IAnalyticsService _analytics;
        private GameConfig _gameConfig;
        
        private List<LevelConfig> _levelConfigs = new();
        private int _currentLevelIndex;

        public LevelsService(IConfigsService configs, IAnalyticsService analytics, GameConfig gameConfig)
        {
            _configs = configs;
            _analytics = analytics;
            _gameConfig = gameConfig;
        }
        
        public UniTask InitializeAsync()
        {
            var levelsConfig = _configs.Load<LevelsConfig>(_gameConfig.LevelsConfigPath);
            
            var levelsConfigDir = Path.GetDirectoryName(_gameConfig.LevelsConfigPath);
            if (levelsConfigDir != null)
            {
                foreach (var levelConfigPath in levelsConfig.LevelConfigsPaths)
                {
                    var configPath = Path.Combine(levelsConfigDir, levelConfigPath);
                    var config = _configs.Load<LevelConfig>(configPath);
                    
                    _levelConfigs.Add(config);
                }
            }

            return default;
        }

        private LevelConfig GetLevelConfig(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= _levelConfigs.Count)
                return null;
            
            return _levelConfigs[levelIndex];
        }

        public LevelConfig GetCurrentLevelConfig()
        {
            return GetLevelConfig(_currentLevelIndex);
        }

        public bool AdvanceLevel()
        {
            int nextLevel = _currentLevelIndex + 1;
            
            if (nextLevel >= _levelConfigs.Count)
                return false;

            _currentLevelIndex = nextLevel;
            
            UpdateAnalytics();
            
            return true;
        }

        public void Reset()
        {
            _currentLevelIndex = 0;
            UpdateAnalytics();
        }
        
        public void Initialize()
        {
            UpdateAnalytics();
        }

        private void UpdateAnalytics()
        {
            _analytics.SetUserProperty(LevelIndexPropertyId, _currentLevelIndex.ToString());
        }
    }
}