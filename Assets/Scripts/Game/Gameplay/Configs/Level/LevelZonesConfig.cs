using System.Collections.Generic;
using Game.Common.Spawn;
using Game.Gameplay.Spawn;
using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class LevelZonesConfig
    {
        [JsonProperty("spawn_settings")]
        public Dictionary<ZoneSpawnType, SpawnSetting> SpawnSettings { get; set; }
    }
}