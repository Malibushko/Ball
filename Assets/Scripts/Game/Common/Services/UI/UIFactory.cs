using System.Collections.Generic;
using Core.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Assets;
using Game.Common.Services.Configs;
using Game.Configs;
using Game.Configs.UI;
using Game.GameConfigs;
using UnityEngine;

namespace Game.Common.Services.UI
{
    public class UIFactory : IInitializableAsyncService
    {
        private readonly IAssetsService _assetsService;
        private readonly IConfigsService _configsService;
        private readonly string _configPath;
        
        private UIConfig _uiConfig;
        
        public UIFactory(IAssetsService assetsService, IConfigsService configs, GameConfig gameConfig)
        {
            _assetsService = assetsService;
            _configsService = configs;
            _configPath = gameConfig.UIConfigPath;
        }

        public UniTask PreloadUI()
        {
            List<UniTask> tasks = new List<UniTask>();
            
            tasks.Add(_assetsService.PreloadAsync<GameObject>(_uiConfig.LoadingScreenConfig.PrefabPath));
            tasks.Add(_assetsService.PreloadAsync<GameObject>(_uiConfig.GameHubConfig.HUDPrefabPath));
            
            return UniTask.WhenAll(tasks);
        }
        
        public GameObject CreateLoadingScreen()
        {
            return _assetsService.Load<GameObject>(_uiConfig.LoadingScreenConfig.PrefabPath);
        }

        public GameObject CreateGamePlayHUD()
        {
            return _assetsService.Load<GameObject>(_uiConfig.GamePlayConfig.HUDPrefabPath);
        }        
        
        public GameObject CreateGameHubHUD()
        {
            return _assetsService.Load<GameObject>(_uiConfig.GameHubConfig.HUDPrefabPath);
        }

        public GameObject CreateLevelWinMenu()
        {
            return _assetsService.Load<GameObject>(_uiConfig.GamePlayConfig.LevelWinPrefabPath);
        }
        
        public GameObject CreateLevelLoseMenu()
        {
            return _assetsService.Load<GameObject>(_uiConfig.GamePlayConfig.LevelLosePrefabPath);
        }
        
        public GameObject CreateLevelPauseMenu()
        {
            return _assetsService.Load<GameObject>(_uiConfig.GamePlayConfig.LevelPausePrefabPath);
        }
        
        public UniTask InitializeAsync()
        {
            _uiConfig = _configsService.Load<UIConfig>(_configPath);
            return default;
        }
    }
}