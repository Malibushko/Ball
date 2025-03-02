using Game.GameConfigs.Memory;
using Newtonsoft.Json;

namespace Game.Configs
{
    public class MemoryConfig
    {
        [JsonProperty("effects")]
        public EffectsMemoryConfig Effects { get; set; }
    }
}