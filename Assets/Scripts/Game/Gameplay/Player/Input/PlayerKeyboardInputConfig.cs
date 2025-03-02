using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Gameplay.Player.Input
{
    public class PlayerKeyboardInputConfig
    {
        [JsonProperty("left")]
        public KeyCode Left { get; set; }
        
        [JsonProperty("right")]
        public KeyCode Right { get; set; }
        
        [JsonProperty("up")]
        public KeyCode Up { get; set; }
        
        [JsonProperty("down")]
        public KeyCode Down { get; set; }
        
        [JsonProperty("speed")]
        public float Speed;
        
        public IEnumerable<KeyCode> Keys
        {
            get
            {
                yield return Left;
                yield return Right;
                yield return Up;
                yield return Down;
            }
        }
    }
}