using Newtonsoft.Json;

namespace Game.Configs.UI
{
    public class GamePlayUIConfig
    {
        [JsonProperty("hud_prefab_path")]
        public string HUDPrefabPath;
        [JsonProperty("level_win_prefab_path")]
        public string LevelWinPrefabPath;
        [JsonProperty("level_lose_prefab_path")]
        public string LevelLosePrefabPath;
        [JsonProperty("level_pause_prefab_path")]
        public string LevelPausePrefabPath;
    }
}