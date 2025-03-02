using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Configs;
using Game.Configs;
using Game.GameConfigs;

namespace Game.Common.Services.Text
{
    public class GameStringsService : IGameStringsService
    {
        private const string NotFoundString = "<undefined>";
        
        private IConfigsService _configsService;
        private string _configPath;
        
        private StringsConfig _stringsConfig = new();
        
        public GameStringsService(IConfigsService configsService, GameConfig gameConfig)
        {
            _configsService = configsService;
            _configPath = gameConfig.StringsConfigPath;
        }
        
        public string GetString(string key)
        {
            return _stringsConfig.GameStrings.GetValueOrDefault(key, NotFoundString);
        }

        public UniTask InitializeAsync()
        {
            _stringsConfig = _configsService.Load<StringsConfig>(_configPath);
            return default;
        }
    }
}