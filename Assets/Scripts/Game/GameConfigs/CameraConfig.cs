using Game.Common.Services.Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.GameConfigs
{
    [SharedConfig]
    public class CameraConfig
    {
        [JsonProperty("position_offset")]
        public Vector3 PositionOffset { get; set; }
        
        [JsonProperty("rotation_offset")]
        public Vector3 RotationOffset { get; set; }
        
        [JsonProperty("speed")]
        public float Speed { get; set; }
    }
}