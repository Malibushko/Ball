using Newtonsoft.Json;

namespace Game.Configs.UI
{
    public class GameHubUIConfig
    {
        [JsonProperty("hud_prefab_path")]
        public string HUDPrefabPath;
    }
}