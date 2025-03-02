using Game.Common.Services.Assets;
using Game.Common.Spawn;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Services.Enemy;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class EnemySpawner : LevelObjectsSpawner
    {
        private IEnemyFactory _factory;
        
        public EnemySpawner(IAssetsService assetsService, IEnemyFactory enemyFactory) : base(assetsService)
        {
            _factory = enemyFactory;
        }

        protected override GameObject Spawn(GameObject prefab, SpawnPoint point, SpawnSetting spawnSettings)
        {
            return _factory.Create(prefab, spawnSettings.Config, point.transform.position, point.transform.rotation);
        }
    }
}