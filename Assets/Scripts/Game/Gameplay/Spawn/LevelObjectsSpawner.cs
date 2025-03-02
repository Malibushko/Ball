using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common.Extensions;
using Game.Common.Services.Assets;
using Game.Common.Spawn;
using Game.Gameplay.Configs.Level;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public abstract class LevelObjectsSpawner
    {
        private IAssetsService _assetsService;

        public LevelObjectsSpawner(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }
        
        public async UniTask Spawn(GameObject level, SpawnSetting spawnSettings, List<SpawnPoint> spawnPoints)
        {
            var selectedSpawnPoints = spawnPoints.SelectRandom(Mathf.Min(spawnSettings.SpawnCount, spawnPoints.Count));
            var objectPrefab = await _assetsService.LoadAsync<GameObject>(spawnSettings.Prefab);
            
            foreach (var point in selectedSpawnPoints) {
                var gameObject = Spawn(objectPrefab, point, spawnSettings);
                
                if (gameObject)
                    gameObject.transform.SetParent(level.transform);
            }
        }

        protected abstract GameObject Spawn(GameObject prefab, SpawnPoint point, SpawnSetting spawnSettings);
    }
}