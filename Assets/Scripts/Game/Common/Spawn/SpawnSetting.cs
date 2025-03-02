using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game.Common.Spawn
{
    public class SpawnSetting
    {
        [JsonProperty("spawn_count")]
        public int SpawnCount { get; set; }
    
        [JsonProperty("prefab")]
        public string Prefab { get; set; }
        
        [JsonProperty("config")] 
        [CanBeNull] 
        public string Config { get; set; }
    }
}