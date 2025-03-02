using Game.GameHub.Configs;
using Newtonsoft.Json;

namespace Game.GameConfigs
{
    public class GameHubConfig
    {
        [JsonProperty("level_prefab_path")]
        public string LevelPrefabPath { get; set; }
        
        [JsonProperty("player")]
        public PlayerConfig PlayerConfig { get; set; }
        
        [JsonProperty("hud_prefab_path")]
        public string HudPrefabPath { get; set; }
        
        [JsonProperty("camera_config")]
        public CameraConfig CameraConfig { get; set; }
    }
}