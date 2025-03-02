using Game.Gameplay.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Services.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject enemyPrefab, string configPath, Vector3 position, Quaternion rotation)
        {
            var gameObject = _container.InstantiatePrefabForComponent<EnemyView>(enemyPrefab);
            gameObject.LoadFromConfig(configPath);
            
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            
            return gameObject.gameObject;
        }
    }
}