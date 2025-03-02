using Newtonsoft.Json;

namespace Game.GameHub.Configs
{
    public class PlayerMoveConfig
    {
        [JsonProperty("move_frequency")]
        public float MoveFrequency;
        [JsonProperty("min_speed")]
        public float MinSpeed;
        [JsonProperty("max_speed")]
        public float MaxSpeed;
    }
}