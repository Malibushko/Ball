using Game.Common.Services.Assets;
using Game.Common.Spawn;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Services.Collectable;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class CollectablesSpawner : LevelObjectsSpawner
    {
        private ICollectableFactory _factory;
        
        public CollectablesSpawner(IAssetsService assetsService, ICollectableFactory factory) : base(assetsService)
        {
            _factory = factory;
        }

        protected override GameObject Spawn(GameObject prefab, SpawnPoint point, SpawnSetting spawnSettings)
        {
            var spawnPoint = point as CollectableSpawnPoint;

            if (spawnPoint != null)
                return _factory.Create(prefab, spawnPoint.Resource, spawnPoint.Amount, point.transform.position);

            return null;
        }
    }
}