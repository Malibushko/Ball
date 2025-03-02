using Game.Common.Services.Configs;
using Game.Gameplay.Player.Input;
using Newtonsoft.Json;

namespace Game.Gameplay.Player.Configs
{
    [SharedConfig]
    public class PlayerConfig
    {
        [JsonProperty("prefab_path")] 
        public string PrefabPath;
        
        [JsonProperty("controller_config")]
        public PlayerControllerConfig PlayerControllerConfig;

        [JsonProperty("input_config")]
        public PlayerInputConfig PlayerInputConfig { get; set; }

        [JsonProperty("health")]
        public int Health;
    }
}