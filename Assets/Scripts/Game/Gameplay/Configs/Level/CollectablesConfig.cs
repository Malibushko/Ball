using System.Collections.Generic;
using Game.Common.Spawn;
using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class CollectablesConfig
    {
        [JsonProperty("main_collectable_resource_id")]
        public string MainCollectableResourceId { get; set; }
    
        [JsonProperty("target_collect_amount")]
        public int TargetCollectAmount { get; set; }
        
        [JsonProperty("spawn_settings")]
        public Dictionary<string, SpawnSetting> SpawnSettings { get; set; }
    }
}