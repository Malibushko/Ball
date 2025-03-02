using Game.Gameplay.Input;
using Newtonsoft.Json;

namespace Game.Gameplay.Player.Input
{
    public class PlayerInputConfig
    {
        [JsonProperty("pointer")]
        public PlayerPointerInputConfig Pointer { get; set; }
        
        [JsonProperty("keyboard")]
        public PlayerKeyboardInputConfig Keyboard { get; set; }
    }
}