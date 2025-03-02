using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Gameplay.Configs.Level
{
    public class LevelsConfig
    {
        [JsonProperty("level_configs")]
        public List<string> LevelConfigsPaths;
    }
}