using Game.Common.Services.Assets;
using Game.Common.Services.Scenes;
using Game.Common.Spawn;
using Game.Gameplay.Configs.Level;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class LevelZonesSpawner : LevelObjectsSpawner
    {
        private IPrefabInstantiator _prefabInstantiator;
        
        public LevelZonesSpawner(IAssetsService assetsService, IPrefabInstantiator prefabInstantiator) : base(assetsService)
        {
            _prefabInstantiator = prefabInstantiator;
        }

        protected override GameObject Spawn(GameObject prefab, SpawnPoint point, SpawnSetting spawnSettings)
        {
            var gameObject = _prefabInstantiator.Instantiate(spawnSettings.Prefab);
            
            gameObject.transform.position = point.transform.position;
            
            return gameObject;
        }
    }
}