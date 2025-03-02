using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class LevelZoneSpawnPoint : SpawnPoint
    {
        [SerializeField] private ZoneSpawnType _type;
        protected override Color PointColor => Color.white;
        protected override string Label => $"{_type.ToString()}Zone";
        
        public ZoneSpawnType Type => _type;
    }
}