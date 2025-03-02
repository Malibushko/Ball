using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class EnemySpawnPoint : SpawnPoint
    {
        protected override string Label => "Enemy";
        protected override Color PointColor => Color.red;
    }
}