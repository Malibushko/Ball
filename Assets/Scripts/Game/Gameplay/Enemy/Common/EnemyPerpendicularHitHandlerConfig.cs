using Game.Common.Services.Configs;
using Newtonsoft.Json;

namespace Game.Gameplay.Enemy.Common
{
    [SharedConfig]
    public class EnemyPerpendicularHitHandlerConfig
    {
        [JsonProperty("hit_angle_threshold")]
        public float HitAngleThreshold { get; set; }
    }
}