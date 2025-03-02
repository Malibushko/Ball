using Newtonsoft.Json;

namespace Game.GameConfigs
{
    public class GameConfig
    {
        [JsonProperty("ui_config_path")]
        public string UIConfigPath;
        
        [JsonProperty("levels_config_path")]
        public string LevelsConfigPath;
        
        [JsonProperty("strings_config_path")]
        public string StringsConfigPath;
        
        [JsonProperty("memory_config_path")]
        public string MemoryConfigPath;
        
        [JsonProperty("game_hub_config_path")]
        public string GameHubConfigPath;
        
        [JsonProperty("ads_config_path")]
        public string AdsConfig;
    }
}