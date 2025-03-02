using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class LevelAdsConfig
    {
        [JsonProperty("health_reward")]
        public int HealthReward { get; set; }
    }
}