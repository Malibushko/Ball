using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Scenes;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Services.Level;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class LevelBuilder : ILevelBuilder
    {
        private IPrefabInstantiator _prefabInstantiator;
        private CollectablesSpawner _collectablesSpawner;
        private EnemySpawner _enemySpawner;
        private LevelZonesSpawner _levelZonesSpawner;

        public LevelBuilder(
            IPrefabInstantiator prefabInstantiator,
            CollectablesSpawner collectablesSpawner,
            EnemySpawner enemySpawner,
            LevelZonesSpawner levelZonesSpawner)
        {
            _prefabInstantiator = prefabInstantiator;
            _collectablesSpawner = collectablesSpawner;
            _enemySpawner = enemySpawner;
            _levelZonesSpawner = levelZonesSpawner;
        }
        
        public async UniTask<GameObject> BuildLevel(LevelConfig levelConfig)
        {
            var levelGameObject = _prefabInstantiator.Instantiate(levelConfig.LevelPrefabPath);

            if (levelGameObject == null)
                return null;
            
            await SpawnCollectables(levelGameObject, levelConfig.Collectables);
            await SpawnEnemies(levelGameObject, levelConfig.Enemies);
            await SpawnZones(levelGameObject, levelConfig.LevelZones);
            
            return levelGameObject;
        }

        private async Task SpawnZones(GameObject levelGameObject, LevelZonesConfig levelZones)
        {
            var spawnPoints = levelGameObject.GetComponentsInChildren<LevelZoneSpawnPoint>();
            
            foreach (var zoneType in levelZones.SpawnSettings)
            {
                var spawnPointsForType = spawnPoints
                    .Where(x => x.Type == zoneType.Key)
                    .ToList<SpawnPoint>();
                
                await _levelZonesSpawner.Spawn(levelGameObject, zoneType.Value, spawnPointsForType);   
            }
        }

        private async UniTask SpawnCollectables(GameObject levelGameObject, CollectablesConfig collectablesConfig)
        {
            var spawnPoints = levelGameObject.GetComponentsInChildren<CollectableSpawnPoint>();
            
            foreach (var collectablesType in collectablesConfig.SpawnSettings)
            {
                var spawnPointsForType = spawnPoints
                    .Where(x => x.Resource.ResourceName == collectablesType.Key)
                    .ToList<SpawnPoint>();
                
                await _collectablesSpawner.Spawn(levelGameObject, collectablesType.Value, spawnPointsForType);   
            }
        }
        
        private async UniTask SpawnEnemies(GameObject levelGameObject, EnemiesConfig enemiesConfig)
        {
            var spawnPoints = levelGameObject.GetComponentsInChildren<EnemySpawnPoint>();
            int spawnedEnemies = 0;
            
            foreach (var enemyConfig in enemiesConfig.SpawnSettings)
            {
                await _enemySpawner.Spawn(levelGameObject, enemyConfig.Value, spawnPoints.Skip(spawnedEnemies).ToList<SpawnPoint>());
                spawnedEnemies += enemyConfig.Value.SpawnCount;
            }
        }
    }
}