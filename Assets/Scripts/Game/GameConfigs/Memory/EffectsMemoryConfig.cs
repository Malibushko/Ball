using Newtonsoft.Json;

namespace Game.GameConfigs.Memory
{
    public class EffectsMemoryConfig
    {
        [JsonProperty("default_pool_size")]
        public int DefaultPoolSize { get; set; }
    }
}