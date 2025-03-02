using Newtonsoft.Json;

namespace Game.Configs.UI
{
    public class UIConfig
    {
        [JsonProperty("loading_screen_config")]
        public LoadingScreenConfig LoadingScreenConfig;
        
        [JsonProperty("game_hub_config")]
        public GameHubUIConfig GameHubConfig;
        
        [JsonProperty("game_play_config")]
        public GamePlayUIConfig GamePlayConfig;
    }
}