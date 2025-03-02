using Game.Gameplay.Player;
using Game.Gameplay.Player.Configs;
using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class LevelConfig
    {        
        [JsonProperty("level_prefab_path")]
        public string LevelPrefabPath { get; set; }

        [JsonProperty("player_config")]
        public PlayerConfig PlayerConfig { get; set; }
        
        [JsonProperty("camera_config")]
        public object CameraConfig { get; set; }
        
        [JsonProperty("enemies")]
        public EnemiesConfig Enemies { get; set; }
        
        [JsonProperty("zones")]
        public LevelZonesConfig LevelZones { get; set; }
    
        [JsonProperty("collectables")]
        public CollectablesConfig Collectables { get; set; }
        
        [JsonProperty("ads")]
        public LevelAdsConfig LevelAds { get; set; }
    }
}