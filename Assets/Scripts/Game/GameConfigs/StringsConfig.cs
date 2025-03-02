using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Configs
{
    public class StringsConfig
    {
        [JsonProperty("game_strings")] public Dictionary<string, string> GameStrings = new();
    }
}