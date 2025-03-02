using Newtonsoft.Json;

namespace Game.Gameplay.Player.Configs
{
    public class PlayerControllerConfig
    {
        [JsonProperty("speed")]
        public float Speed;
        
        [JsonProperty("max_interaction_distance")]
        public float MaxInteractionDistance;
    }
}