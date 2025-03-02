using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Configs
{
    public class PlatformSpecificAdsConfig
    {
        [JsonProperty("rewarded_placement_id")]
        public string RewardedPlacementId;
        
        [JsonProperty("ads_unit")]
        public string AdsUnit;
        
        [JsonProperty("game_id")]
        public string GameId;
    }
    
    public class AdsConfig
    {
        [JsonProperty("is_test_mode")]
        public bool IsTestMode;
        
        [JsonProperty("max_ads_per_session")]
        public int MaxAdsPerSession;
        
        [JsonProperty("platforms")]
        public Dictionary<string, PlatformSpecificAdsConfig> Platforms;

        public PlatformSpecificAdsConfig GetPlatformConfig(string platform)
        {
            if (Platforms != null && Platforms.TryGetValue(platform, out var config))
            {
                return config;
            }
            
            return Platforms != null && Platforms.TryGetValue("default", out var adsPlatform) ? adsPlatform : null;
        }
    }
}