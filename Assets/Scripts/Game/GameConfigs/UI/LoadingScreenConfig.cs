using Newtonsoft.Json;

namespace Game.Configs.UI
{
    public class LoadingScreenConfig
    {
        [JsonProperty("prefab_path")] 
        public string PrefabPath;
        
        [JsonProperty("min_show_time")] 
        public float MinShowTime;
        
        [JsonProperty("status_change_delay")] 
        public float StatusChangeDelay;
    }
}