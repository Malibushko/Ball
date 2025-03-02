using Game.Common.Services.Configs;
using Newtonsoft.Json;

namespace Game.GameHub.Configs
{
    [SharedConfig]
    public class PlayerConfig
    {
        [JsonProperty("prefab_path")]
        public string PlayerPrefabPath { get; set; }
        
        [JsonProperty("controller")]
        public PlayerMoveConfig MoveConfig { get; set; }
    }
}