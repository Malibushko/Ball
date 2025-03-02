using System.Collections.Generic;
using Game.Common.Spawn;
using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class EnemiesConfig
    {
        [JsonProperty("spawn_settings")]
        public Dictionary<string, SpawnSetting> SpawnSettings { get; set; }
    }
}