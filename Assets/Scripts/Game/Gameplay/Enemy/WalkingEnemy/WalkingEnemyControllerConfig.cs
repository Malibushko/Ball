using Game.Common.Services.Configs;
using Newtonsoft.Json;

namespace Game.Gameplay.Enemy.WalkingEnemy
{
    public class WalkingEnemyControllerConfig
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        
        [JsonProperty("distance")]
        public float Distance { get; set; }
    }
}