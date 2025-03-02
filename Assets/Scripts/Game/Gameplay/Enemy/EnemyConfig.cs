using Game.Common.Services.Configs;
using Newtonsoft.Json;

namespace Game.Gameplay.Enemy
{
    [SharedConfig]
    public class EnemyConfig
    {
        [JsonProperty("damage")]
        public int Damage;
        
        [JsonProperty("controller_config")]
        public string Controller;
        
        [JsonProperty("hit_handler_config")]
        public string HitHandler;
    }
}