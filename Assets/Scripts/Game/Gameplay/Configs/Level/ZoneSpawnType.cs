using System.Runtime.Serialization;

namespace Game.Gameplay.Spawn
{
    public enum ZoneSpawnType
    {
        [EnumMember(Value = "player_spawn_zone")]
        PlayerSpawn,
        [EnumMember(Value = "win_zone")]
        Win
    }
}